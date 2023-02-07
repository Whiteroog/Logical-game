using System.Collections.Generic;
using ThisProject.Medals;
using ThisProject.Medals.SO;
using ThisProject.Scenes;
using ThisProject.Scenes.Conditions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ThisProject.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject stopwatch;

        public TMP_Text scoreText;
        public TMP_Text medalText;

        public MedalHandler medalHandler;

        public Transform conditionsGameObject;
        private List<Transform> _listConditions;

        private void Start()
        {
            _listConditions = new List<Transform>(conditionsGameObject.childCount);

            for (int i = 0; i < conditionsGameObject.childCount; i++)
            {
                _listConditions.Add(conditionsGameObject.GetChild(i));
            }
            
            SetActiveUI(false);
        }

        public void SetActiveUI(bool state)
        {
            pauseMenu.SetActive(state);
            stopwatch.SetActive(!state);
        }

        public void SetupDataMenu(List<LevelConditionSO> levelCondition)
        {
            scoreText.text = "0";
            medalText.text = "";

            for (int i = 0; i < levelCondition.Count; i++)
			{
                _listConditions[i].GetComponentInChildren<TMP_Text>().text = levelCondition[i].textCondition;
                _listConditions[i].GetComponentInChildren<Image>().sprite = levelCondition[i].starterSprite;
            }
        }

        public void UpdateDataMenu(List<LevelConditionSO> levelCondition)
        {
            scoreText.text = Score.GetTotalScore(levelCondition).ToString();

            MedalSO medal = medalHandler.GetMedal(levelCondition);

            medalText.text = medal.placeText;
            medalText.color = medal.colorPlace;

            for (int i = 0; i < levelCondition.Count; i++)
            {
                _listConditions[i].GetComponentInChildren<Image>().sprite = levelCondition[i].GetSpriteOfCondition();
            }
        }
    }
}
