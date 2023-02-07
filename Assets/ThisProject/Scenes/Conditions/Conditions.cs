using System;
using UnityEngine;

namespace ThisProject.Scenes.Conditions
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
}