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

        public TMP_Text textCon1;
        public Image imageCon1;
        
        public TMP_Text textCon2;
        public Image imageCon2;
        
        public TMP_Text textCon3;
        public Image imageCon3;

        private void Start()
        {
            SetActiveUI(false);
        }

        public void SetActiveUI(bool state)
        {
            pauseMenu.SetActive(state);
            stopwatch.SetActive(!state);
        }

        public void SetupShowingConditions(List<LevelCondition> levelCondition)
        {
            textCon1.text = levelCondition[0].textCondition;
            imageCon1.sprite = levelCondition[0].defaultly;
            
            textCon2.text = levelCondition[1].textCondition;
            imageCon2.sprite = levelCondition[1].defaultly;
            
            textCon3.text = levelCondition[2].textCondition;
            imageCon3.sprite = levelCondition[2].defaultly;
        }

        public void UpdateShowingConditions(List<LevelCondition> levelCondition)
        {
            imageCon1.sprite = levelCondition[0].GetSpriteOfCondition();
            imageCon2.sprite = levelCondition[1].GetSpriteOfCondition();
            imageCon3.sprite = levelCondition[2].GetSpriteOfCondition();
        }
    }
}
