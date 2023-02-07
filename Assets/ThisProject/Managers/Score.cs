using System.Collections.Generic;
using ThisProject.Scenes;
using ThisProject.Scenes.Conditions;

namespace ThisProject.Managers
{
	public static class Score
	{
		public static int GetTotalScore(List<LevelConditionSO> levelCondition)
		{
			int totalScore = 0;

			foreach(var condition in levelCondition)
			{
				totalScore += condition.IsCompleted() ? condition.score : 0;
			}

			return totalScore;
		}
	}
}
