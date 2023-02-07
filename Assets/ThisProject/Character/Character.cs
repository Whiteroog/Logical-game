using UnityEngine;
using UnityEngine.Events;

namespace ThisProject.Character
{
	public class Character : MonoBehaviour
	{
		private Transform _cursor;
		private Transform _box;
	
		private Movement _movement;

		public LayerMask targetBoxLayer;
		public UnityEvent boxPutDown;

		public bool _isPlayingAnimation = false;

		public void SetIsPlayingAnimation(bool state) => _isPlayingAnimation = state;

		public bool IsDraggingBox() => _box != null;
	
		private Vector3 GetInputDirection() => new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		private void Start()
		{
			_cursor = transform.GetChild(0);
			_movement = GetComponent<Movement>();
			_movement.isPlayingAnimationMovement += SetIsPlayingAnimation;
		}

		void Update()
		{
			if (Input.anyKeyDown && !_isPlayingAnimation)
			{
				_movement.Moving(GetInputDirection(), IsDraggingBox());

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
				_box.SetParent(null);
				_box = null;

				boxPutDown.Invoke();
			}
			else
			{
				if (RaycastWatcher.TryTakeTargetBox(_cursor.position, targetBoxLayer, out Transform foundBox))
				{               
					// to character
					_box = foundBox;
					_box.SetParent(_cursor);
				}
			}
		}
	}
}
