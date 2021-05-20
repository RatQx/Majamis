using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnterExitCar : MonoBehaviour
{
    private GameObject player;
    public GameObject playerCamera;
    public GameObject minimapCamera;
    private GameObject car;
    public GameObject camera;
    public bool nearCar;
    public bool inCar = false;
    public Radio globalRadio;
    void Start()
    {
        car = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //FOR ELLEN
        if (nearCar && Input.GetKeyDown(KeyCode.F) && !inCar)      //Enter car
        {
            player.SetActive(false);
            car.GetComponent<CarController>().enabled = true;
            inCar = true;
            camera.GetComponent<OrbitCamera>().focus = car.transform.Find("CameraFocus");
            playerCamera.gameObject.SetActive(false);
            camera.SetActive(true);
            minimapCamera.SetActive(true);
            StartCoroutine(RadioOn());
        }
        else if (inCar && Input.GetKeyDown(KeyCode.F))            //Exit car
        {
            player.SetActive(true);
            car.GetComponent<CarController>().enabled = false;
            inCar = false;
            //camera.focus = player.transform.Find("CameraFocus");
            camera.SetActive(false);
            minimapCamera.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            nearCar = false;
            player.transform.position = car.transform.position + new Vector3(3, 1, 0);
            player.transform.rotation = car.transform.rotation;
            RadioOff();
        }

        //FOR MJ
        /*if (nearCar && Input.GetKeyDown(KeyCode.F) && !inCar)      //Enter car
        {
            player.SetActive(false);
            car.GetComponent<CarController>().enabled = true;
            inCar = true;
            camera.GetComponent<OrbitCamera>().focus = car.transform.Find("CameraFocus");
            //playerCamera.gameObject.SetActive(false);
            //camera.SetActive(true);
            StartCoroutine(RadioOn());
        }
        else if (inCar && Input.GetKeyDown(KeyCode.F))            //Exit car
        {
            player.SetActive(true);
            car.GetComponent<CarController>().enabled = false;
            inCar = false;
            //camera.focus = player.transform.Find("CameraFocus");
            //camera.SetActive(false);
            //playerCamera.gameObject.SetActive(true);
            nearCar = false;
            camera.GetComponent<OrbitCamera>().focus = player.transform.Find("CameraFocus");
            player.transform.position = car.transform.position + new Vector3(3, 1, 0);
            player.transform.rotation = car.transform.rotation;
            RadioOff();
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            nearCar = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nearCar = false;
        }
    }

    IEnumerator RadioOn()
    {
        yield return new WaitForSeconds(1);
        globalRadio.PlayRandomStation();
        globalRadio.enabled = true;
    }
    void RadioOff()
    {
        globalRadio.TurnOffRadio();
        globalRadio.enabled = false;
    }
}
