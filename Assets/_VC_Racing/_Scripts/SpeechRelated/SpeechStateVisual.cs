using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechStateVisual : MonoBehaviour
{
    [SerializeField] private Image iconBg;
    [SerializeField] private TextMeshProUGUI recStatusText;
    
    [SerializeField] private Color listeningColor1 = Color.green;
    [SerializeField] private Color listeningColor2 = Color.blue;
    [SerializeField] private Color idleColor = Color.red;
    [SerializeField] private float lerpSpeed = 2f;

    private bool _isListening;
    private float lerpTime;
        
    public void UpdateRecStatus(bool state)
    {
        _isListening = state;
        recStatusText.text = _isListening ? "Listening..." : "Idle";
    }

    private void Update()
    {
        if (_isListening)
        {
            lerpTime += Time.deltaTime;
            iconBg.color = Color.Lerp(listeningColor1, listeningColor2, Mathf.PingPong(lerpTime, 1f));
        }
        else iconBg.color = idleColor;
    }
}
