using System.Collections;
using System.Collections.Generic;
using ThisProject.Scenes;
using UnityEngine;

public class MedalHandle : MonoBehaviour
{
	public List<Medal> medals;

	public Medal GetMedalForCondition(List<LevelCondition> levelCondition)
	{
		int levelPlace = -1;

		foreach (var condition in levelCondition)
		{
			levelPlace += condition.IsCompleted() ? 1 : 0;
		}

		return medals[levelPlace];
	}
}

[CreateAssetMenu(fileName = "Medal-", menuName = "Level/Medal")]
public class Medal : ScriptableObject
{
	public string placeText = "Gold";
	public Color colorPlace = Color.yellow;
}