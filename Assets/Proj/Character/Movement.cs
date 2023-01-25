using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Character character;
	public Cursor cursor;
	public RaycastWatcher raycastWatcher;

	private Vector3 firstDirection = Vector3.right;

	public void MovementKeyDown()
	{
		Vector3 direction = new Vector3(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);

		if (IsTwoKeysDown(direction))
		{
			return;
		}

		bool isSameDirection = IsSameDirection(firstDirection, direction);
		int multipleSizeCharacter = IsBoxGrapping() && isSameDirection ? 2 : 1;

		Vector3 newPositionAboutBox = direction * multipleSizeCharacter;

		bool isPositionObstacle = raycastWatcher.IsPositionObstacle(transform.position + newPositionAboutBox);
		bool isBoxGrapping = IsBoxGrapping();

		if (!isPositionObstacle && IsSameDirection(firstDirection, direction))
		{
			transform.Translate(direction);
		}
		else
		{
			if (!isBoxGrapping || !isPositionObstacle)
				firstDirection = direction;
		}
		if (!isBoxGrapping || !isPositionObstacle)
			cursor.SetCursor(direction);
	}

	private bool IsSameDirection(Vector3 firstDirect, Vector3 targetDirect)
	{
		return firstDirect.Equals(targetDirect);
	}

	private bool IsTwoKeysDown(Vector3 clickDirection)
	{
		return clickDirection.x != 0 && clickDirection.y != 0;
	}



	private bool IsBoxGrapping()
	{
		return character.HaveBox();
	}
}