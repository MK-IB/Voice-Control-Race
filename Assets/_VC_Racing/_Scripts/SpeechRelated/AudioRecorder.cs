using UnityEngine;
using System.Collections;
using System.IO;

public class AudioRecorder : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRecording()
    {
        audioClip = Microphone.Start(null, false, 5, 44100);
        Debug.Log("Recording...");
    }

    public void StopRecording()
    {
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
            Debug.Log("Stopped Recording.");
            SaveAudioClip(audioClip);
            FindObjectOfType<GoogleSpeechToText>().SendAudioFile(Path.Combine(Application.persistentDataPath, "recording.wav"));
        }
    }

    private void SaveAudioClip(AudioClip clip)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "recording.wav");
        var data = ConvertToWav(clip);
        File.WriteAllBytes(filePath, data);
        Debug.Log("Audio saved: " + filePath);
    }

    private byte[] ConvertToWav(AudioClip clip)
    {
        MemoryStream memoryStream = new MemoryStream();
        // Write WAV file format header (simplified for brevity)
        return memoryStream.ToArray();
    }
}