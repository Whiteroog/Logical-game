using System;
using System.Collections.Generic;
using ThisProject.OtherGameObject;
using ThisProject.Scenes;
using UnityEngine;

namespace ThisProject.Managers
{
	public class GameManager : MonoBehaviour
	{
		public List<LevelCondition> levelCondition;
		
		public List<GameObject> boxes;
		public List<GameObject> points;

		public UIManager uiManager;
		public Stopwatch stopwatch;

		private bool _isActiveMenu = false;
		private bool _isSceneCompleted = false;

		private void Start()
		{
			SetDefaultStateConditions();
			uiManager.SetupShowingConditions(levelCondition);

			// boxesValue = new List<GameObject>(GameObject.FindGameObjectsWithTag("TargetObject"));
			// pointKey = new List<GameObject>(GameObject.FindGameObjectsWithTag("TargetPoint"));
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !_isSceneCompleted)
			{
				_isActiveMenu = !_isActiveMenu;
				SetActiveMenu(_isActiveMenu);
			}
		}

		public void CheckEndGame()
		{
			_isSceneCompleted = IsBoxesOnPositions();

			if (!_isSceneCompleted)
				return;

			UpdateStateConditions();
			uiManager.UpdateShowingConditions(levelCondition);

			_isActiveMenu = true;
			SetActiveMenu(_isActiveMenu);
		}

		public float GetCheckIsBoxesOnPositions()
			=> Convert.ToSingle(IsBoxesOnPositions());
		
		private bool IsBoxesOnPositions()
		{
			if (points.Count == 0)
				return false;
		
			for (int i = 0; i < points.Count; i++)
			{
				if(points[i].transform.position != boxes[i].transform.position)
				{
					return false;
				}
			}
			return true;
		}

		private void SetActiveMenu(bool state)
		{
			uiManager.SetActiveUI(state);
			stopwatch.ActiveStopwatch = !state;
		}

		private void UpdateStateConditions()
		{
			foreach (var condition in levelCondition)
			{
				condition.UpdateCondition();
			}
		}

		private void SetDefaultStateConditions()
		{
			foreach (var condition in levelCondition)
			{
				condition.SetDefaultCondition();
			}
		}
	}
}
