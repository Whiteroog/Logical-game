using System.Collections;
using System.Collections.Generic;
using ThisProject.Scenes;
using UnityEngine;

public static class Score
{
	public static int GetTotalScore(List<LevelCondition> levelCondition)
	{
		int totalScore = 0;

		foreach(var condition in levelCondition)
		{
			totalScore += condition.IsCompleted() ? condition.score : 0;
		}

		return totalScore;
	}
}
