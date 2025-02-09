using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace  _VC_Racing._Scripts.ControllerRelated
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;

        public GameObject HUD, completePanel, failPanel;
        public TextMeshProUGUI speedText, timerText;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            
        }
        void OnEnable()
        {
            MainController.GameStateChanged += GameManager_GameStateChanged;
        }

        void OnDisable()
        {
            MainController.GameStateChanged -= GameManager_GameStateChanged;
        }

        void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.LevelComplete)
            {
                HUD.SetActive(false);
                completePanel.SetActive(true);
            }
            if (newState == GameState.LevelFail)
            {
                HUD.SetActive(false);
                failPanel.SetActive(true);
            }
        }

        public void UpdateSpeedUi(float val)
        {
            int speed = (int)val * 10;
            speedText.text = speed + " KMH";
        }

        public void UpdateTimerText(float timeToDisplay)
        {
            timeToDisplay = Mathf.Max(0, timeToDisplay);
            
            int minutes = Mathf.FloorToInt(timeToDisplay / 60);
            int seconds = Mathf.FloorToInt(timeToDisplay % 60);
            if (timeToDisplay <= 20f)
            {
                timerText.color = Color.red;
            }
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }   
}
