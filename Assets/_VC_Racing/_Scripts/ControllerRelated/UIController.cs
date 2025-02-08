using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace  _VC_Racing._Scripts.ControllerRelated
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;

        public GameObject HUD, completePanel, failPanel;
        public TextMeshProUGUI speedText;

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
    }   
}
