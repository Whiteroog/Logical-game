using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RaycastWatcher : MonoBehaviour
{
	public LayerMask targetBoxLayer;
	public LayerMask obstacleLayers;

	public bool IsPositionObstacle(Vector3 position)
	{
		return Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, obstacleLayers);
	}

	public bool IsTargetBox(Vector3 position, out GameObject box)
	{
		box = Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, targetBoxLayer).collider?.gameObject;
		return box;
	}
}
