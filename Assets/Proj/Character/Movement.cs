using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public CharacterAnimation characterAnimation;

	public Character character;
	public Cursor cursor;
	public RaycastWatcher raycastWatcher;

	public float speed = 2f;

	private Vector3 firstSupposeDirection = Vector3.right;

	private bool isMoving = false;

	private bool IsBoxGrapping() => character.HaveBox();

	private Vector3 GetInputDirection() => new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

	private bool IsTwoKeysDown(Vector3 InputAxisDirection) => InputAxisDirection.x != 0 && InputAxisDirection.y != 0;

	private bool IsSameDirection(Vector3 lastDirect, Vector3 nextDirect) => nextDirect.Equals(lastDirect);

	public void Moving()
	{
		if (isMoving)
			return;

		Vector3 inputDirection = GetInputDirection();

		// если не WASD
		if (inputDirection.magnitude == 0)
			return;

		// не обрабатывать сразу два нажатия
		if (IsTwoKeysDown(inputDirection))
			return;

		// нажато тоже направление
		bool isSameDirection = IsSameDirection(firstSupposeDirection, inputDirection);

		// если тащим коробку прямо -> проверить место за коробкой
		int multipleCheckingDirection = IsBoxGrapping() && isSameDirection ? 2 : 1;

		// учитывание проверки места с коробкой
		bool isPositionObstacle = raycastWatcher.IsPositionObstacle(transform.position + inputDirection * multipleCheckingDirection);

		bool isBoxGrapping = IsBoxGrapping();

		// передвигаемся если нет препятствия и наж. то же направление
		if (!isPositionObstacle && IsSameDirection(firstSupposeDirection, inputDirection))
		{
			isMoving = true;
			StartCoroutine(SmoothedMove(transform.position + inputDirection));

			characterAnimation.SetAnimationDirect(inputDirection);
		}
		else
		{
			characterAnimation.SetAnimationDirect(inputDirection);
			characterAnimation.SetToIdle();

			if (!isPositionObstacle || !isBoxGrapping)
				firstSupposeDirection = inputDirection; // просто направление
		}
		
		// курсор перемещаем если нет припятствий или не тащим
		if (!isPositionObstacle || !isBoxGrapping)
			cursor.SetCursor(inputDirection);
	}

	private IEnumerator SmoothedMove(Vector3 endPosition)
	{
		Vector3 startPosition = transform.position;

		for (float timeElapsed = 0; timeElapsed < 1.0f; timeElapsed += Time.deltaTime * speed)
		{
			float lerpStep = timeElapsed / 1.0f;
			transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
			yield return null;
		}

		transform.position = endPosition;

		isMoving = false;
		characterAnimation.SetToIdle();
	}
}