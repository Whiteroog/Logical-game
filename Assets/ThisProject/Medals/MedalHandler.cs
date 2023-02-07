using System.Collections;
using System.Collections.Generic;
using ThisProject.Medals.SO;
using ThisProject.Scenes;
using ThisProject.Scenes.Conditions;
using UnityEngine;

namespace ThisProject.Medals
{
	public class MedalHandler : MonoBehaviour
	{
		public List<MedalSO> medals;

		public MedalSO GetMedal(List<LevelConditionSO> levelCondition)
		{
			int levelPlace = 0;

			foreach (var condition in levelCondition)
			{
				levelPlace += condition.IsCompleted() ? 1 : 0;
			}

			return medals[levelPlace];
		}
	}
}