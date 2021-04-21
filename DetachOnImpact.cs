using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum Type
{
    Detach,
    Particle,
    OpenAndDetach
}
public class DetachOnImpact : MonoBehaviour
{
    public Rigidbody Car;

    [Tooltip("Force required to detach object")]
    public float health = 1000f;
    private float h;

    public Type breakingType;
    public bool triggeredCollision = false;
    public bool broken = false;

    [SerializeField, Range(0, 10)]
    public int[] layers;

    private float timer = 1f;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public float positionTimer = 10f;
    private HingeJoint hinge;
    private bool stopTimer = false;

    private void Start()
    {
        h = health;
        originalPosition = gameObject.transform.localPosition;
        originalRotation = gameObject.transform.localRotation;
        if(breakingType == Type.OpenAndDetach)
        {
            //hinge = gameObject.GetComponent<HingeJoint>();
            //Destroy(gameObject.GetComponent<HingeJoint>());
        }
    }
    void Update()
    {
        if (broken && !stopTimer)
            positionTimer -= Time.deltaTime;
        if(positionTimer <= 0)
        {
            //gameObject.SetActive(false);

            stopTimer = true;
            if (breakingType == Type.Detach)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Collider>().isTrigger = true;
                gameObject.GetComponent<Collider>().enabled = false;
                
                Destroy(gameObject.GetComponent<Rigidbody>());
            }
            else if (breakingType == Type.Particle)
            {   
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            gameObject.transform.localPosition = originalPosition;
            gameObject.transform.localRotation = originalRotation;

            positionTimer = 10f;
        }


        if(triggeredCollision)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
            triggeredCollision = false;

        if (Input.GetKeyDown(KeyCode.P) && positionTimer == 10f)
        {
            health = h;
            broken = false;
            stopTimer = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            if(breakingType == Type.Detach)
            {
                gameObject.GetComponent<Collider>().enabled = true;
            }
            else if (breakingType == Type.Particle)
            {

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.layer + "   " + LayersToCollide.value); 
        foreach (var layer in layers)
        {
            if(other.gameObject.layer == layer)
            {
                //print(other.gameObject.layer + "   " + gameObject);
                triggeredCollision = true;
            }
            
        }
            
    }



    public void CheckImpact(float force)
    {
        if(!broken)
        {
            
            if (triggeredCollision)
            {
                print(gameObject);
                health -= force;
                triggeredCollision = false;
            }

            if (health <= 0)
            {
                
                if (breakingType == Type.Detach)
                {
                    broken = true;
                    Detach();
                }
                else if (breakingType == Type.Particle)
                {
                    broken = true;
                    Particle();
                }
                else
                    Open();
                    
            }
                
        }
        
    }

    private void Detach()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void Particle()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    private void Open()
    {
        print("OOOOPEEEEEN");
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        addHinge();
        
        broken = true;
    }

    private void addHinge()
    {
        while(gameObject.GetComponent<HingeJoint>() == null)
        {
            print("adding joint!!!!!");
            gameObject.AddComponent<HingeJoint>();
            //gameObject.AddComponent<HingeJoint>();
            //Destroy(gameObject.GetComponent<HingeJoint>());
        }


        //gameObject.GetComponent<HingeJoint>(). = hinge;
        gameObject.GetComponent<HingeJoint>().connectedBody = Car;
        gameObject.GetComponent<HingeJoint>().enableCollision = true;
        gameObject.GetComponent<HingeJoint>().enablePreprocessing = true;
        gameObject.GetComponent<HingeJoint>().breakForce = 5;

        print(gameObject + "    " + gameObject.GetComponent<HingeJoint>().connectedBody);

        while(gameObject.GetComponent<HingeJoint>().connectedBody != Car)
        {
            gameObject.GetComponent<HingeJoint>().connectedBody = Car;
            gameObject.GetComponent<HingeJoint>().enableCollision = true;
            gameObject.GetComponent<HingeJoint>().enablePreprocessing = true;
            gameObject.GetComponent<HingeJoint>().breakForce = 5;
            print("ooooo");
        }
        print(gameObject + "    " + gameObject.GetComponent<HingeJoint>().connectedBody);
    }
}
