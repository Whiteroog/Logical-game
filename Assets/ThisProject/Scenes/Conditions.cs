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

    static class Conditions
    {
        public static bool GetResultOperation(Sign sign, float leftValue, float rightValue)
            => sign switch
            {
                Sign.Great => leftValue > rightValue,
                Sign.Less => leftValue < rightValue,
                Sign.Equal => leftValue == rightValue,
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

        public bool isCompleted = false;

        public ValueLevel getValueLevel = () => 0.0f;

        public void SetDefaultCondition()
        {
            isCompleted = false;
        }
        
        public void UpdateCondition()
        {
            isCompleted = Conditions.GetResultOperation(sign, getValueLevel(), targetValue);
        }

        public Sprite GetSpriteOfCondition() => isCompleted ? ifTrue : ifFalse;
    }
}