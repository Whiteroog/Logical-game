using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPositionBoxesOnPoints : MonoBehaviour
{
	public List<GameObject> boxesValue;
	public List<GameObject> pointKey;

	public bool CheckBoxesOnPositions()
	{
		for (int i = 0; i < pointKey.Count; i++)
		{
			if(pointKey[i].transform.position != boxesValue[i].transform.position)
			{
				return false;
			}
		}
		return true;
	}
}
