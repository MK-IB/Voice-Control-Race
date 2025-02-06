using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private float delay;
    private bool isPermissionRequested = false;

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            InitializeSpeechRecognition();
        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
            isPermissionRequested = true;
        }
    }

    private void InitializeSpeechRecognition()
    {
        // Your speech recognition initialization logic here
        Debug.Log("Microphone permission granted. Initializing...");
        LoadScene();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && isPermissionRequested && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        Debug.Log("Reloading scene to recover from black screen.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
