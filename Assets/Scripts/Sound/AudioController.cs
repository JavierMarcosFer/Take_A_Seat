using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disable warning CS0618
// 'WWW' is obsolete'
#pragma warning disable CS0618

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [HideInInspector] public AudioSource voiceAudioSource;
    [HideInInspector] public AudioClip voiceLine;
    [HideInInspector] public string voicePath;

    private void Awake()
    {
        instance = this;
        voiceAudioSource = gameObject.AddComponent<AudioSource>();
        voicePath = "file://" + Application.streamingAssetsPath + "/Sounds/VoiceClips/";
    }

    public void PlayVoiceLine(string filename)
    {
        StartCoroutine(LoadVoiceLine(filename));
    }

    private IEnumerator LoadVoiceLine(string filename)
    {
        WWW request = GetaudioFromFile(voicePath, filename);
        yield return request;

        voiceLine = request.GetAudioClip();
        voiceLine.name = filename;

        PlayVoiceLine();
    }
    
    private void PlayVoiceLine()
    {
        voiceAudioSource.Stop();
        voiceAudioSource.clip = voiceLine;
        voiceAudioSource.Play();
    }

    private WWW GetaudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
