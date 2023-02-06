using System.Collections.Generic;
using ThisProject.Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThisProject.Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject stopwatch;

        public TMP_Text scoreText;
        public TMP_Text medalText;

        public MedalHandle medalHandle;

        public List<GameObject> uiConditions;

        private void Start()
        {
            SetActiveUI(false);
        }

        public void SetActiveUI(bool state)
        {
            pauseMenu.SetActive(state);
            stopwatch.SetActive(!state);
        }

        public void SetupDataMenu(List<LevelCondition> levelCondition)
        {
            scoreText.text = "0";

            medalText.text = "";

            for (int i = 0; i < levelCondition.Count; i++)
			{
                uiConditions[i].GetComponentInChildren<TMP_Text>().text = levelCondition[i].textCondition;
                uiConditions[i].GetComponentInChildren<Image>().sprite = levelCondition[i].defaultly;
            }
        }

        public void UpdateDataMenu(List<LevelCondition> levelCondition)
        {
            scoreText.text = Score.GetTotalScore(levelCondition).ToString();

            Medal medal = medalHandle.GetMedalForCondition(levelCondition);

            medalText.text = medal.placeText;
            medalText.color = medal.colorPlace;

            for (int i = 0; i < levelCondition.Count; i++)
            {
                uiConditions[i].GetComponentInChildren<Image>().sprite = levelCondition[i].GetSpriteOfCondition();
            }
        }
    }
}
