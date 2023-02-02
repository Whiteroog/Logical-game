using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class RaycastWatcher
{
	public static bool IsTargetBox(Vector3 position, LayerMask targetBoxLayer, out GameObject box)
	{
		box = Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, targetBoxLayer).collider?.gameObject;
		return box;
	}

	public static bool IsPositionObstacle(Vector3 position, LayerMask obstacleLayers)
	{
		return Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, obstacleLayers);
	}
}
