using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class BluetoothPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayMusicList player;
    public WWW www;
    public AudioSource source;
    public AudioClip[] userMusic;
    public string path;
    int musicCount;
    int found = 0;
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        player = gameObject.GetComponent<PlayMusicList>();
        path = "file://" + Application.streamingAssetsPath + "/Music/";


        var infoMP3 = new DirectoryInfo(Application.streamingAssetsPath + "/Music/");
        var fileInfoMP3 = infoMP3.GetFiles("*.mp3");

        var infoWAV = new DirectoryInfo(Application.streamingAssetsPath + "/Music/");
        var fileInfoWAV = infoWAV.GetFiles("*.wav");

        musicCount = fileInfoMP3.Length + fileInfoWAV.Length;
        userMusic = new AudioClip[musicCount];

        foreach (var file in fileInfoMP3)
        {
            print(file.Name);
            StartCoroutine(LoadAudio(file.Name));
        }
        foreach (var file in fileInfoWAV)
        {
            print(file.Name);
            StartCoroutine(LoadAudio(file.Name));
        }

        gameObject.GetComponent<PlayMusicList>().myMusic = userMusic;
        
        
    }

    private IEnumerator LoadAudio(string name)
    {
        WWW request = GetAudioFromFile(path, name);
        yield return request;
        AudioClip fileClip = request.GetAudioClip();
        fileClip.name = request.GetAudioClip().name;
        userMusic[found++] = fileClip;
        //source.clip = clip;
        //source.Play();
    }

    private WWW GetAudioFromFile(string path, string name)
    {
        WWW request = new WWW(path + name);
        return request;
        
    }

   
}
