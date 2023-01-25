using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Cursor cursor;
	public RaycastWatcher raycastWatcher;
	public Movement movement;

	public GameObject box;
	public bool HaveBox() => box != null;


	void Update()
	{
		if (Input.anyKeyDown)
		{
			movement.MovementKeyDown();

			if (Input.GetKeyDown(KeyCode.E))
			{
				TakeKeyDown();
			}
		}
	}

	private void TakeKeyDown()
	{
		if (!raycastWatcher.IsTargetBox(cursor.transform.position, out var findedBox))
			return;

		if (HaveBox())
		{
			box.transform.SetParent(null, true);
			box = null;
		}
		else
		{
			box = findedBox;
			findedBox.transform.SetParent(cursor.transform);
		}
	}
}
