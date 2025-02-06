using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace  _VC_Racing._Scripts.ControllerRelated
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;

        public GameObject HUD, completePanel, failPanel;
        public Image giantHealthBar;
        public GameObject giantHealthBarParent;
        public TextMeshProUGUI levelNumText;
        public GameObject crowdCounterBubble;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            int currentLevel = PlayerPrefs.GetInt("levelnumber", 1);
            levelNumText.text = "Lv."+currentLevel.ToString();
            //GAScript.Instance.LevelStart(SceneManager.GetActiveScene().buildIndex.ToString());
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
                //GAScript.Instance.LevelCompleted(SceneManager.GetActiveScene().buildIndex.ToString());
            }
            if (newState == GameState.LevelFail)
            {
                HUD.SetActive(false);
                failPanel.SetActive(true);
                //GAScript.Instance.LevelFail(SceneManager.GetActiveScene().buildIndex.ToString());
            }
        }
    }   
}
