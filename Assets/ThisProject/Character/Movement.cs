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
	
		public float speed = 2.0f;
		public float rotateSpeed = 2.0f;
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
				_characterAnimation?.SetDirectAnimation(inputDirection);
				MoveToDirection(inputDirection);
			}
			else
			{
				if (boxIsNotOnObstacle)
				{
					_lastInputDirection = inputDirection;
					RotateToDirection(inputDirection);
				}
			}
		}

		private void MoveToDirection(Vector3 direction)
		{
			_stepsMoved ++;
			isPlayingAnimationMovement.Invoke(true);
			StartCoroutine(SmoothedMove(transform.position + direction));
		}

		private void RotateToDirection(Vector3 direction)
		{
			isPlayingAnimationMovement.Invoke(true);
			StartCoroutine(SmoothedRotation(direction));
		}
	
		private IEnumerator SmoothedMove(Vector3 endPosition)
		{
			Vector3 startPosition = transform.position;

			Vector3 direction = endPosition - startPosition;

			for (float lerpStep = 0; lerpStep < 1.0f; lerpStep += Time.deltaTime * speed)
			{
				transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
				yield return null;
			}

			transform.position = endPosition;
			_characterAnimation?.SetIdleAnimation(direction.normalized);
			isPlayingAnimationMovement.Invoke(false);
		}

		private IEnumerator SmoothedRotation(Vector3 inputDirection)
		{
			Vector3 currentDirection = _cursor.localPosition;

			float roundK = 1.0f;

			for (float lerpStep = 0; lerpStep < 1.0f; lerpStep += Time.deltaTime * rotateSpeed)
			{
				float powerRounding = Time.deltaTime * rotateSpeed;
				roundK += lerpStep < 0.5f ? powerRounding : -powerRounding;

				Vector3 lerpCalc = Vector3.Lerp(currentDirection, inputDirection, lerpStep);
				_cursor.localPosition = lerpCalc * roundK;

				_characterAnimation?.SetIdleAnimation(lerpCalc);

				yield return null;
			}

			_cursor.localPosition = inputDirection;
			isPlayingAnimationMovement.Invoke(false);
		}
	}
}