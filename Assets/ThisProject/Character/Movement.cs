using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace ThisProject.Character
{
	public class Movement : MonoBehaviour
	{
		public Action<bool> isPlayingAnimationMovement;
		
		[CanBeNull] private CharacterAnimation _characterAnimation;
	
		public float speed = 2f;
		public LayerMask obstacleLayers;

		private Vector3 _lastInputDirection = Vector3.right;

		private int _stepsMoved = 0;
		
		private Transform _cursor;

		private void Start()
		{
			_cursor = transform.GetChild(0);
			_characterAnimation = GetComponent<CharacterAnimation>();
		}

		public float GetStepsMoved() => _stepsMoved;

		private bool IsTwoKeysDown(Vector3 inputAxisDirection) => inputAxisDirection.x != 0 && inputAxisDirection.y != 0;

		private bool IsSameDirection(Vector3 nextDirect) => nextDirect.Equals(_lastInputDirection);

		private bool IsTurnOppositeDirectionWithBoxes(Vector3 nextDirect, bool isDraggingBox) => IsSameDirection(nextDirect * (-1)) && isDraggingBox;

		public void Moving(Vector3 inputDirection, bool isDraggingBox = false)
		{
			if (inputDirection.magnitude == 0)
				return;
		
			if (IsTwoKeysDown(inputDirection))
				return;

			if (IsTurnOppositeDirectionWithBoxes(inputDirection, isDraggingBox))
				return;
		
			bool isSameDirection = IsSameDirection(inputDirection);
		
			int multipleCheckingDirection = isDraggingBox && isSameDirection ? 2 : 1;
			bool isPositionObstacle = RaycastWatcher.IsPositionObstacle(transform.position + inputDirection * multipleCheckingDirection, obstacleLayers);
			
			bool boxIsNotOnObstacle = !isPositionObstacle || !isDraggingBox;

			if (!isPositionObstacle && IsSameDirection(inputDirection))
			{
				MoveToDirection(inputDirection);
				_characterAnimation?.SetDirectAnimation(inputDirection);
			}
			else
			{
				if (boxIsNotOnObstacle)
				{
					_cursor.localPosition = inputDirection;
					_lastInputDirection = inputDirection;
					_characterAnimation?.SetIdleAnimation(inputDirection);
				}
			}
		}

		private void MoveToDirection(Vector3 direction)
		{
			_stepsMoved ++;
			isPlayingAnimationMovement.Invoke(true);
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
			_characterAnimation?.SetIdleAnimation(direction.normalized);
			isPlayingAnimationMovement.Invoke(false);
		}
	}
}