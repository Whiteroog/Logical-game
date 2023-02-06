using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisProject.Scenes
{
    public enum Sign
    {
        Great,
        Less,
        Equal
    }

    public enum StateCompletedTask
	{
        Processes,
        Completed,
        Failed
	}

    static class Conditions
    {
        public static StateCompletedTask GetResultOperation(Sign sign, float leftValue, float rightValue)
            => sign switch
            {
                Sign.Great => leftValue > rightValue ? StateCompletedTask.Completed : StateCompletedTask.Failed,
                Sign.Less => leftValue < rightValue ? StateCompletedTask.Completed : StateCompletedTask.Failed,
                Sign.Equal => leftValue == rightValue ? StateCompletedTask.Completed : StateCompletedTask.Failed,
                _ => throw new Exception("No such sign")
            };
    }

    [CreateAssetMenu(fileName = "L1Con", menuName = "Level/Condition")]
    public class LevelCondition : ScriptableObject
    {
        public delegate float ValueLevel();
        
        public Sprite defaultly;
        public Sprite ifTrue;
        public Sprite ifFalse;
        public string textCondition;
        public float targetValue;
        public Sign sign;

        public int score;

        private StateCompletedTask stateCompletedTask = StateCompletedTask.Processes;

        public ValueLevel getValueLevel = () => 0.0f;

        public void SetDefaultCondition()
        {
            stateCompletedTask = StateCompletedTask.Processes;
        }
        
        public void UpdateCondition()
        {
            stateCompletedTask = Conditions.GetResultOperation(sign, getValueLevel(), targetValue);
        }

        public Sprite GetSpriteOfCondition() => stateCompletedTask switch
        {
            StateCompletedTask.Processes => defaultly,
            StateCompletedTask.Completed => ifTrue,
            StateCompletedTask.Failed => ifFalse,
            _ => throw new Exception("No such state")
        };

        public bool IsCompleted() => stateCompletedTask == StateCompletedTask.Completed;
    }
}