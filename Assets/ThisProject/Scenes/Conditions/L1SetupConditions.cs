using System.Collections.Generic;
using ThisProject.Character;
using ThisProject.GameObjects.Stopwatch;
using ThisProject.Managers;
using UnityEngine;

namespace ThisProject.Scenes.Conditions
{
    public class L1SetupConditions : MonoBehaviour
    {
        public List<LevelConditionSO> conditions;

        public GameManager fromGameManager;
        public Stopwatch fromStopwatch;
        public Movement fromMovement;

        private void Start()
        {
            conditions[0].getValueLevel = fromGameManager.GetCheckIsBoxesOnPositions;
            conditions[1].getValueLevel = fromStopwatch.GetSeconds;
            conditions[2].getValueLevel = fromMovement.GetStepsMoved;
        }
    }
}