using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorBoxPosition : MonoBehaviour
{
	private Image _indicator;

	private void Awake()
	{
		_indicator = GetComponent<Image>();
	}

	public void SwitchIndicator(bool isTarget)
	{
		_indicator.color = isTarget ? Color.green : Color.red;
	}
}
