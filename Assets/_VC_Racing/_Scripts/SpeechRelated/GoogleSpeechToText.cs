using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class GoogleSpeechToText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI responseText;
    private const string apiUrl = "https://speech.googleapis.com/v1p1beta1/speech:recognize?key=AIzaSyDSAsL3N67fThFr9pGK604ov2UFrsX6vAw";
    public void SendAudioFile(string filePath)
    {
        StartCoroutine(SendRequest(filePath));
    }

    private IEnumerator SendRequest(string filePath)
    {
        byte[] audioData = System.IO.File.ReadAllBytes(filePath);

        string jsonRequest = "{\"config\": {\"encoding\": \"LINEAR16\", \"languageCode\": \"en-US\"}," +
                             "\"audio\": {\"content\": \"" + System.Convert.ToBase64String(audioData) + "\"}}";

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, jsonRequest))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                responseText.text = request.downloadHandler.text;
                Debug.Log("Response: " + request.downloadHandler.text);
                ProcessResponse(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    private void ProcessResponse(string jsonResponse)
    {
        if (jsonResponse.Contains("start"))
        {
            Debug.Log("Voice Command: Start Car");
            FindObjectOfType<CarController>().StartCar();
        }
        else if (jsonResponse.Contains("stop"))
        {
            Debug.Log("Voice Command: Stop Car");
            FindObjectOfType<CarController>().StopCar();
        }
        else
        {
            Debug.Log("Unknown Command");
        }
    }
}