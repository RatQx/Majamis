using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject mainScript;
    public int missionID = 1;
    void OnTriggerEnter(Collider other)
    {
        mainScript.gameObject.GetComponent<M001>().MissionManager(missionID);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
