using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public IndicatorBoxPosition indicator;
	public CheckPositionBoxesOnPoints check;

	private void Start()
	{
		indicator.SwitchIndicator(false);
	}

	public void CheckEndGame()
	{
		indicator.SwitchIndicator(check.CheckBoxesOnPositions());
	}
}
