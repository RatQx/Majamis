using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class M001 : MonoBehaviour
{
    

    //----------TEKSTAS ANT EKRANO
    public GameObject Tekstas;


    //----------TELEFONO SKAMBUTIS
    public string fadeInAnim;
    public string fadeOutAnim;
    public GameObject phone;
    public GameObject LauresSkambutisPirmas;
    public GameObject LaureSneka;
    public GameObject laikstesMarker;
    public Animator panelAnimator;

    //----------MISSION START MARKERS
    public GameObject miniMaploc;
    public GameObject missionStartPointloc;

    //----------LAIKSTES MARKERS/COLLIDER
    public Collider laikstesCollider;

    //----------PUSKELNIU MARKERS/COLLIDER
    public GameObject puskelniuMarker;
    public Collider puskelniuCollider;

    //----------LAIKSTES GRIZIMO MARKERS/COLLIDER
    public GameObject laikstesGrizimoMarker;
    public Collider laikstesGrizimoCollider;

    //----------RESPECT SCREEN:
    public GameObject MisijaPabaigta;
    public GameObject missionCompBackGround;
    public GameObject gavai;
    public GameObject kaGavai;
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MissionBegin());
    }
    public void MissionManager(int missionID)
    {
        switch (missionID)
        {
            case 1:
                StartCoroutine(LaikstesEtapas());
                break;
            case 2:
                StartCoroutine(PuskelniuEtapas());
                break;
            case 3:
                StartCoroutine(LaikstesGrizimoEtapas());
                break;

        }
    }
    IEnumerator MissionBegin()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        LauresSkambutisPirmas.SetActive(true);
        miniMaploc.SetActive(false);
        panelAnimator.Play(fadeInAnim);
        yield return new WaitForSeconds(4);
        LauresSkambutisPirmas.SetActive(false);
        LaureSneka.SetActive(true);
        Tekstas.SetActive(true);
        Tekstas.GetComponent<Text>().text = "MD: Alio, kas čia zvanija?";
        yield return new WaitForSeconds(4);
        Tekstas.GetComponent<Text>().text = "Laurė: Sveikas, vietiniai mane vadina Laure.";
        yield return new WaitForSeconds(4);
        Tekstas.GetComponent<Text>().text = "Laurė: Gavau tavo numerį, girdėjau mašinos reikia?";
        yield return new WaitForSeconds(4);
        Tekstas.GetComponent<Text>().text = "MD: Gal ir reik gal ir ne o ką?";
        yield return new WaitForSeconds(4);
        Tekstas.GetComponent<Text>().text = "Laurė: Ateik prie laikštės garažo, paderinsim.";
        yield return new WaitForSeconds(4);
        Tekstas.SetActive(false);
        panelAnimator.Play(fadeOutAnim);
        LaureSneka.SetActive(false);
        missionStartPointloc.gameObject.GetComponent<MeshRenderer>().enabled = false;
        laikstesMarker.SetActive(true);
        
    }
    IEnumerator LaikstesEtapas()
    {
        laikstesCollider.gameObject.GetComponent<BoxCollider>().enabled = false;
        miniMaploc.SetActive(false);
        Tekstas.SetActive(true);
        Tekstas.GetComponent<Text>().text = "Laurė: Nu diela tokia";
        yield return new WaitForSeconds(1);
        Tekstas.GetComponent<Text>().text = "Laurė: Man reikia kad nuvažiuotum su šitu lavonu";
        yield return new WaitForSeconds(4);
        Tekstas.GetComponent<Text>().text = "Laurė: Ir paimtum iš puskelnių cigarų";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Imk dešimt eurų";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Grįši, gausi savo mašiną";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "MD: Davažiuos tas grabas iki tų puskelnių?";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Tas grabas važinėjo į rusyną tai manau pora kilų atlaikys";
        yield return new WaitForSeconds(5);
        Tekstas.SetActive(false);
        puskelniuMarker.SetActive(true);
        laikstesMarker.SetActive(false);
    }
    IEnumerator PuskelniuEtapas()
    {
        puskelniuCollider.gameObject.GetComponent<BoxCollider>().enabled = false;
        Tekstas.SetActive(true);
        Tekstas.GetComponent<Text>().text = "MD: Labą dieną, reikia cigarų";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "Kasinikė: Kokių reikia?";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "MD: Žinot duokit malboro auksinį";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Kasinikė: 4 eurai 20 centų";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "MD: Oho branginykai didieji gal akcijukę kokią";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Kasinikė: Nori pigiau rūkyk tą są savo enzių";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "MD: Gerai teta tinka ta kaina nesinervuokit";
        yield return new WaitForSeconds(3);
        Tekstas.SetActive(false);
        yield return new WaitForSeconds(2);
        LauresSkambutisPirmas.SetActive(true);
        panelAnimator.Play(fadeInAnim);
        yield return new WaitForSeconds(4);
        LauresSkambutisPirmas.SetActive(false);
        LaureSneka.SetActive(true);
        Tekstas.SetActive(true);
        Tekstas.GetComponent<Text>().text = "MD: Nu nupirkau tau marlborkės auksinį";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "MD: Kur privežt?";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "Laurė: Numesk ten pat kur susitikom lauksiu";
        yield return new WaitForSeconds(4);
        Tekstas.SetActive(false);;
        panelAnimator.Play(fadeOutAnim);
        LaureSneka.SetActive(false);
        puskelniuMarker.SetActive(false);
        laikstesGrizimoMarker.SetActive(true);
    }
    IEnumerator LaikstesGrizimoEtapas()
    {
        laikstesGrizimoCollider.gameObject.GetComponent<BoxCollider>().enabled = false;
        Tekstas.SetActive(true);
        Tekstas.GetComponent<Text>().text = "MD: Nu va tau malborkė auksas";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "Laurė: Nu dėkavoju o va čia tau";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "Laurė: Pradžia gyvenimo - slyva trilitris dyzelis";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Viskas kaip priklauso mpackai čipuota iki 170";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Saleros nedaug ima";
        yield return new WaitForSeconds(2);
        Tekstas.GetComponent<Text>().text = "Laurė: Priekis biškį daužtas bet tai nieko";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Su tokia galėsi pusė miesto guldyt";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Ir pigiai važinėdamas šaibų pasitaupysi";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "MD: Nu dėkoju senelyzai jei ką susiskambinam";
        yield return new WaitForSeconds(3);
        Tekstas.GetComponent<Text>().text = "Laurė: Davai";
        yield return new WaitForSeconds(3);
        Tekstas.SetActive(false);
        laikstesGrizimoMarker.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(RespectScreen());
    }
    IEnumerator RespectScreen()
    {
        MisijaPabaigta.SetActive(true);
        yield return new WaitForSeconds(1);
        missionCompBackGround.SetActive(true);
        yield return new WaitForSeconds(1);
        gavai.SetActive(true);
        yield return new WaitForSeconds(1);
        kaGavai.SetActive(true);
        yield return new WaitForSeconds(10);
        MisijaPabaigta.SetActive(false);
        missionCompBackGround.SetActive(false);
        gavai.SetActive(false);
        kaGavai.SetActive(false);
    }
}
