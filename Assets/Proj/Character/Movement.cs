using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public CharacterAnimation characterAnimation;

	public float speed = 2f;
	public LayerMask obstacleLayers;

	private Vector3 _lastInputDirection = Vector3.right;

	private bool _isPlayAnimationMoving = false;

	private bool IsTwoKeysDown(Vector3 inputAxisDirection) => inputAxisDirection.x != 0 && inputAxisDirection.y != 0;

	private bool IsSameDirection(Vector3 nextDirect) => nextDirect.Equals(_lastInputDirection);

	public void Moving(Vector3 inputDirection, out Vector3 cursorDirection, bool isDraggingBox = false)
	{
		cursorDirection = _lastInputDirection;
		
		// animation is playing
		if (_isPlayAnimationMoving)
			return;

		// ���� �� WASD
		if (inputDirection.magnitude == 0)
			return;

		// �� ������������ ����� ��� �������
		if (IsTwoKeysDown(inputDirection))
			return;

		// ������ ���� �����������
		bool isSameDirection = IsSameDirection(inputDirection);

		// ���� ����� ������� ����� -> ��������� ����� �� ��������
		int multipleCheckingDirection = isDraggingBox && isSameDirection ? 2 : 1;

		// ���������� �������� ����� � ��������
		bool isPositionObstacle = RaycastWatcher.IsPositionObstacle(transform.position + inputDirection * multipleCheckingDirection, obstacleLayers);

		// ������������� ���� ��� ����������� � ���. �� �� �����������
		if (!isPositionObstacle && IsSameDirection(inputDirection))
		{
			MoveToDirection(inputDirection);

			characterAnimation.SetDirectAnimation(inputDirection);
		}
		else
		{
			characterAnimation.SetDirectAnimation(inputDirection);
			characterAnimation.SetIdleAnimation();

			if (!isPositionObstacle || !isDraggingBox)
				_lastInputDirection = inputDirection; // ������ �����������
		}
		if (!isPositionObstacle || !isDraggingBox)
			cursorDirection = inputDirection;
	}

	private void MoveToDirection(Vector3 direction)
	{
		_isPlayAnimationMoving = true;
		StartCoroutine(SmoothedMove(transform.position + direction));
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

		_isPlayAnimationMoving = false;
		characterAnimation.SetIdleAnimation();
	}
}