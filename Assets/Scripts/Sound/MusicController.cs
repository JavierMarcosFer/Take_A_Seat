using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disable warning CS0618
// 'WWW' is obsolete'
#pragma warning disable CS0618

public class MusicController : MonoBehaviour
{
    
    [HideInInspector] public AudioSource musicAudioSource;
    [HideInInspector] public AudioClip musicTrack;
    [HideInInspector] public string musicPath;

    private void Awake()
    {
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicPath = "file://" + Application.streamingAssetsPath + "/Sounds/Music/";
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }
    
   public void PlayMusic()
   {
        musicAudioSource.loop = true;
        LoadMusic("InterrogationMusic.wav");
   }

   private IEnumerator LoadMusic(string filename)
   {
       WWW request = GetaudioFromFile(musicPath, filename);
       yield return request;

       musicTrack = request.GetAudioClip();
       musicTrack.name = filename;

        PlayMusicTrack();
   }

   private void PlayMusicTrack()
   {
       musicAudioSource.Stop();
       musicAudioSource.clip = musicTrack;
       musicAudioSource.Play();
   }

    private WWW GetaudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
