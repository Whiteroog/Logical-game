using System;
using System.Collections.Generic;
using ThisProject.GameObjects.Stopwatch;
using ThisProject.Scenes;
using ThisProject.Scenes.Conditions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThisProject.Managers
{
	public class GameManager : MonoBehaviour
	{
		public List<GameObject> boxes;
		public List<GameObject> points;

		public UIManager uiManager;
		public Stopwatch stopwatch;
		
		public List<LevelConditionSO> levelConditions;

		private bool _isActiveMenu = false;
		private bool _isSceneCompleted = false;

		private void Start()
		{
			SetDefaultStateConditions();
			uiManager.SetupDataMenu(levelConditions);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !_isSceneCompleted)
			{
				_isActiveMenu = !_isActiveMenu;
				SetActiveMenu(_isActiveMenu);
			}
		}

		private void UpdateStateConditions()
		{
			foreach (var condition in levelConditions)
			{
				condition.UpdateCondition();
			}
		}

		private void SetDefaultStateConditions()
		{
			foreach (var condition in levelConditions)
			{
				condition.SetDefaultCondition();
			}
		}
		
		public void RestartLevel()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void CheckEndGame()
		{
			_isSceneCompleted = IsBoxesOnPositions();

			if (!_isSceneCompleted)
				return;

			UpdateStateConditions();
			uiManager.UpdateDataMenu(levelConditions);

			_isActiveMenu = true;
			SetActiveMenu(_isActiveMenu);
		}
		
		private void SetActiveMenu(bool state)
		{
			uiManager.SetActiveUI(state);
			stopwatch.ActiveStopwatch = !state;
		}

		// 1.0f - true | 0.0f - false
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
	}
}
