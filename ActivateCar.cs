using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCar : MonoBehaviour
{
    public CarController[] cars;
    int activated = 0;
    public OrbitCamera camera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            activated++;
            if (activated >= cars.Length)
                activated = 0;

            for(int i=0; i<cars.Length; i++)
            {
                cars[i].enabled = false;
            }
            cars[activated].enabled = true;

            var cameraFocus = cars[activated].transform.Find("CameraFocus");
            if (cameraFocus)
            {
                camera.focus = cameraFocus;
            }
        }
    }
}
