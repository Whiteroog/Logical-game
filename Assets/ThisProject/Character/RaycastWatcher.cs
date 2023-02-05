using UnityEngine;

namespace ThisProject.Character
{
	public static class RaycastWatcher
	{
		public static bool TryTakeTargetBox(Vector3 position, LayerMask targetBoxLayer, out Transform box)
		{
			box = Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, targetBoxLayer).transform;
			return box != null;
		}

		public static bool IsPositionObstacle(Vector3 position, LayerMask obstacleLayers)
		{
			return Physics2D.Raycast(position, Vector3.zero, Mathf.Infinity, obstacleLayers).collider != null;
		}
	}
}
