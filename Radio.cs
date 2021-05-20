using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public GameObject[] stations; //all radio stations with music
    public AudioSource globalRadio;
    public int station = 1;
    public float volume = 1;

    public bool enabled = false;
    public Text text;
    private bool btOn = false;
    private void Start()
    {
        globalRadio.clip.LoadAudioData();
        TurnOffRadio();
        //ActivateStation();
        text.CrossFadeAlpha(0.0f, 0.01f, false);
        for(int i=1; i<stations.Length-1; i++)
        {
            stations[i].GetComponent<PlayMusicList>().LoadAudio();
        }
    }
    void Update()
    {
        //if(station > 1)
            //print(stations[station].name + "   " + stations[station].GetComponent<AudioSource>().volume + "   " + stations[station].GetComponent<AudioSource>().clip.name);
        if(enabled)
        {
            bool changed = false;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                station++;
                if (station > stations.Length - 1)
                    station = -1;

                changed = true;

                /*TurnOffRadio();
                if (station == -1)
                {
                    text.text = "Radio Off";
                    StartCoroutine(FadeText());
                    btOn = false;
                }
                else
                {
                    text.text = stations[station].name;
                    StartCoroutine(FadeText());
                    if (station == 0)
                    {
                        btOn = true;
                        StartCoroutine(TurnBluetooth());
                    }
                    else
                    {
                        btOn = false;
                        ActivateStation();//stations[station].GetComponent<AudioSource>().mute = false;
                    }
                        
                }*/
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                station--;
                if (station < -1)
                    station = stations.Length - 1;

                changed = true;

                /*TurnOffRadio();
                if (station == -1)
                {
                    text.text = "Radio Off";
                    StartCoroutine(FadeText());
                }
                else
                {
                    text.text = stations[station].name;
                    StartCoroutine(FadeText());
                    if (station == 0)
                    {
                        StartCoroutine(TurnBluetooth());
                    }
                    else
                        ActivateStation();//stations[station].GetComponent<AudioSource>().mute = false;
                }*/
            }

            if(changed)
            {
                TurnOffRadio();
                if (station == -1)
                {
                    text.text = "Radio Off";
                    StartCoroutine(FadeText());
                    btOn = false;
                    globalRadio.volume = 0;
                }
                else
                {
                    text.text = stations[station].name;
                    StartCoroutine(FadeText());
                    if (station == 0)
                    {
                        btOn = true;
                        StartCoroutine(TurnBluetooth());
                        
                    }
                    else
                    {
                        btOn = false;
                        ActivateStation();//stations[station].GetComponent<AudioSource>().mute = false;
                    }

                }
            }
        }
    }

    public void TurnOffRadio()
    {
        for(int i=0; i<stations.Length; i++)
        {
            //stations[i].GetComponent<AudioSource>().mute = true;
            stations[i].GetComponent<AudioSource>().volume = 0;
        }
    }

    void ActivateStation()
    {
        //globalRadio.mute = true;
        //stations[station].GetComponent<AudioSource>().mute = false;
        globalRadio.volume = 0;
        stations[station].GetComponent<AudioSource>().volume = 1;
    }

    IEnumerator TurnBluetooth()
    {
        //globalRadio.mute = false;
        globalRadio.volume = 1;
        globalRadio.Play();
        yield return new WaitForSeconds(8);
        if (btOn)
            ActivateStation();
    }

    public void PlayRandomStation()
    {
        station = Random.Range(-1, stations.Length);
        TurnOffRadio();
        if (station != -1)
        {
            text.text = stations[station].name;
            ActivateStation();
        }    
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        text.CrossFadeAlpha(1.0f, 0.2f, false);
        yield return new WaitForSeconds(3f);
        text.CrossFadeAlpha(0.0f, 0.2f, false);
    }
    
}
/*
[System.Serializable]
public class Station
{
    public Object[] music;

}
*/
