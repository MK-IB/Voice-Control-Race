using System;
using System.Collections;
using _VC_Racing._Scripts.ControllerRelated;
using UnityEngine;

namespace _VC_Racing._Scripts.GameplayRelated
{
    public class HelpElement : MonoBehaviour
    {
        [SerializeField] private GameObject startHelp, continueHelp, timerHelp;

        private void Start()
        {
            #if UNITY_EDITOR
            Invoke(nameof(StartRace), 2);
            #endif
        }

        void StartRace()
        {
            MainController.instance.SetActionType(GameState.RaceStarted);
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
            if (newState == GameState.RaceStarted)
                StartCoroutine(ShowHelp());
        }

        IEnumerator ShowHelp()
        {
            startHelp.SetActive(false);
            yield return new WaitForSeconds(3.5f);
            continueHelp.SetActive(true);
            
            yield return new WaitForSeconds(7);
            timerHelp.SetActive(true);
            continueHelp.SetActive(false);
            
            yield return new WaitForSeconds(10);
            timerHelp.SetActive(false);
        }
    }
}
