using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace ThisProject.Character
{
	public class Movement : MonoBehaviour
	{
		[CanBeNull] private CharacterAnimation _characterAnimation;
	
		public float speed = 2f;
		public LayerMask obstacleLayers;

		private Vector3 _lastInputDirection = Vector3.right;
		private bool _isPlayAnimationMoving = false;

		private int _stepsMoved = 0;

		private void Start()
		{
			_characterAnimation = GetComponent<CharacterAnimation>();
		}

		public float GetStepsMoved() => _stepsMoved;

		private bool IsTwoKeysDown(Vector3 inputAxisDirection) => inputAxisDirection.x != 0 && inputAxisDirection.y != 0;

		private bool IsSameDirection(Vector3 nextDirect) => nextDirect.Equals(_lastInputDirection);

		public void Moving(Vector3 inputDirection, out Vector3 cursorDirection, bool isDraggingBox = false)
		{
			cursorDirection = _lastInputDirection;
		
			if (_isPlayAnimationMoving)
				return;
		
			if (inputDirection.magnitude == 0)
				return;
		
			if (IsTwoKeysDown(inputDirection))
				return;
		
			bool isSameDirection = IsSameDirection(inputDirection);
		
			int multipleCheckingDirection = isDraggingBox && isSameDirection ? 2 : 1;
		
			bool isPositionObstacle = RaycastWatcher.IsPositionObstacle(transform.position + inputDirection * multipleCheckingDirection, obstacleLayers);
		
			if (!isPositionObstacle && IsSameDirection(inputDirection))
			{
				_stepsMoved ++;
				MoveToDirection(inputDirection);

				_characterAnimation?.SetDirectAnimation(inputDirection);
			}
			else
			{
				_characterAnimation?.SetIdleAnimation(inputDirection);

				if (!isPositionObstacle || !isDraggingBox)
					_lastInputDirection = inputDirection;
			}
			if (!isPositionObstacle || !isDraggingBox)
				cursorDirection = inputDirection;
		}

		private void MoveToDirection(Vector3 direction)
		{
			_isPlayAnimationMoving = true;
			StartCoroutine(SmoothedMove(transform.position + direction));
		}
	
		private IEnumerator SmoothedMove(Vector3 endPosition)
		{
			Vector3 startPosition = transform.position;

			Vector3 direction = endPosition - startPosition;

			for (float timeElapsed = 0; timeElapsed < 1.0f; timeElapsed += Time.deltaTime * speed)
			{
				float lerpStep = timeElapsed / 1.0f;
				transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
				yield return null;
			}

			transform.position = endPosition;

			_isPlayAnimationMoving = false;
			_characterAnimation?.SetIdleAnimation(direction.normalized);
		}
	}
}