using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Cursor cursor;
	public Movement movement;

	[HideInInspector]
	public GameObject box;

	public UnityEvent boxPutDown;
	
	public LayerMask targetBoxLayer;

	public bool IsDraggingBox() => box != null;
	
	private Vector3 GetInputDirection() => new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

	void Update()
	{
		if (Input.anyKeyDown)
		{
			movement.Moving(GetInputDirection(), out Vector3 cursorDirection, IsDraggingBox());

			cursor.SetCursor(cursorDirection);

			if (Input.GetKeyDown(KeyCode.E))
			{
				ActionToBox();
			}
		}
	}

	private void ActionToBox()
	{
		if (IsDraggingBox())
		{
			// to world
			box.transform.SetParent(null, true);
			box = null;

			boxPutDown.Invoke();
		}
		else
		{
			if (RaycastWatcher.IsTargetBox(cursor.transform.position, targetBoxLayer, out var foundBox))
			{               
				// to character
				box = foundBox;
				foundBox.transform.SetParent(cursor.transform);
			}
		}
	}
}
