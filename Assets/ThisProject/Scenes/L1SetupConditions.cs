using System;
using System.Collections.Generic;
using ThisProject.Character;
using ThisProject.Managers;
using ThisProject.OtherGameObject;
using UnityEngine;
using UnityEngine.Events;

namespace ThisProject.Scenes
{
    public class L1SetupConditions : MonoBehaviour
    {
        public List<LevelCondition> conditions;

        public GameManager gameManager;
        public Stopwatch stopwatch;
        public Movement movement;

        private void Start()
        {
            conditions[0].getValueLevel = gameManager.GetCheckIsBoxesOnPositions;
            conditions[1].getValueLevel = stopwatch.GetSeconds;
            conditions[2].getValueLevel = movement.GetStepsMoved;
        }
    }
}