using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayMusicList : MonoBehaviour
{
    public AudioClip[] myMusic; // declare this as Object array
    public AudioSource audio;
    //public float length;

    void Awake()
    {
        /*audio.clip = myMusic[0] as AudioClip;
        length = audio.clip.length;
        audio.Play();*/
        
        
        playRandomMusic();

    }

    // Update is called once per frame
    void Update()
    {
        //length -= Time.deltaTime;

        if (!audio.isPlaying)
        {
            playRandomMusic();
        }
        
    }

    public void playRandomMusic()
    {
        audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
        audio.Play();
        //length = audio.clip.length;
        /*foreach (AudioClip clip in myMusic)
        {
            if (clip.LoadAudioData())
                Debug.Log("Audio has been successfully loaded");
            else
            {
                Debug.Log("Sound effect loading failed");
                return;
            }
        }*/
    }

    public void LoadAudio()
    {
        foreach (AudioClip clip in myMusic)
        {
            if (clip.LoadAudioData())
            {
                //Debug.Log("Audio has been successfully loaded");
            }
            else
            {
                Debug.Log("Sound effect loading failed");
                return;
            }
        }
    }
}
