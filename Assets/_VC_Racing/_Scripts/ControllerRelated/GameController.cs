using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _VC_Racing._Scripts.ControllerRelated
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

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
