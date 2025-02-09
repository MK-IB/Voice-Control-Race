using System;
using _VC_Racing._Scripts.ControllerRelated;
using UnityEngine;

namespace _JohnyTrigger._Scripts.ControllerRelated
{
    public class SoundsController : MonoBehaviour
    {
        public static SoundsController instance;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource audioSourceBg;

        public AudioClip buttonClick; 
        

        private void Awake()
        {
            instance = this;
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
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
            audioSourceBg.enabled = newState  != GameState.LevelComplete || newState != GameState.LevelFail;
        }
    }
}
