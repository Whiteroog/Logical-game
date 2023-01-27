using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Cursor cursor;
	public RaycastWatcher raycastWatcher;
	public Movement movement;

	[HideInInspector]
	public GameObject box;

	public UnityEvent boxPutDown;

	public bool HaveBox() => box != null;


	void Update()
	{
		if (Input.anyKeyDown)
		{
			movement.Moving();

			if (Input.GetKeyDown(KeyCode.E))
			{
				TakeBox();
			}
		}
	}

	private void TakeBox()
	{
		if (HaveBox())
		{
			// to world
			box.transform.SetParent(null, true);
			box = null;

			boxPutDown.Invoke();
		}
		else
		{
			if (raycastWatcher.IsTargetBox(cursor.transform.position, out var findedBox))
			{               
				// to character
				box = findedBox;
				findedBox.transform.SetParent(cursor.transform);
			}
		}
	}
}
