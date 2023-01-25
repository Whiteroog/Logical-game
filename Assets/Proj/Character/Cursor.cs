using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
	public void SetCursor(Vector3 direction)
	{
		if (direction.magnitude == 0) return;

		transform.localPosition = direction;
	}

}
