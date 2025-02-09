using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _VC_Racing._Scripts.ControllerRelated
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public float timeRemaining = 30f;
        private bool timerIsRunning = true;

        private UIController _uiController;
        
        public Camera mainCamera;
        private Transform _platformsParent;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            _uiController = UIController.instance;
            _platformsParent = GameObject.Find("Platform").transform;
            mainCamera = Camera.main;
            SetToBottomLeft();
            int screenHeight = Screen.height;
            int screenWidth = Screen.width;
        }
        void SetToBottomLeft()
        {
            Vector3 bottomLeftViewport = new Vector3(0, 0, mainCamera.nearClipPlane);
            Vector3 worldPosition = mainCamera.ViewportToWorldPoint(bottomLeftViewport);
            
            worldPosition.z = transform.position.z;
            _platformsParent.position = worldPosition;
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
            
        }

        private void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    _uiController.UpdateTimerText(timeRemaining);
                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    GameFail();
                }
            }
        }

        void GameFail()
        {
            MainController.instance.SetActionType(GameState.LevelFail);
        }
        public void On_RestartButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void On_NextButtonPressed()
        {
            if (PlayerPrefs.GetInt("level", 1) >= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCountInBuildSettings - 1));
                PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 1) + 1));
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 1) + 1));
            }

            PlayerPrefs.SetInt("levelnumber", PlayerPrefs.GetInt("levelnumber", 1) + 1);
        }
    }   
}
