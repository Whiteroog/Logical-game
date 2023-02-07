using System;
using System.Collections;
using System.Collections.Generic;
using ThisProject.Scenes.Conditions;
using UnityEngine;

[CreateAssetMenu(fileName = "L1Con", menuName = "Level/Condition")]
public class LevelConditionSO : ScriptableObject
{
    public delegate float ValueLevel();
        
    public Sprite starterSprite;
    public Sprite trueSprite;
    public Sprite falseSprite;
    public string textCondition;
    public float targetValue;
    public Sign sign;

    public int score;

    private StateCompletedTask _stateCompletedTask = StateCompletedTask.Processes;

    public ValueLevel getValueLevel = () => 0.0f;

    public void SetDefaultCondition()
    {
        _stateCompletedTask = StateCompletedTask.Processes;
    }
        
    public void UpdateCondition()
    {
        _stateCompletedTask = Conditions.GetResultOperation(sign, getValueLevel(), targetValue);
    }

    public Sprite GetSpriteOfCondition() => _stateCompletedTask switch
    {
        StateCompletedTask.Processes => starterSprite,
        StateCompletedTask.Completed => trueSprite,
        StateCompletedTask.Failed => falseSprite,
        _ => throw new Exception("No such state")
    };

    public bool IsCompleted() => _stateCompletedTask == StateCompletedTask.Completed;
}
