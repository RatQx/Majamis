using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForce : MonoBehaviour
{
    [Tooltip("Object with trigger colliders to detect collision")]
    public GameObject[] colliderTriggers;
    [Tooltip("Force multiplier. Multiplied force will be subtracted from collider health")]
    public float multiplier = 0.001F;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        float collisionForce = (collision.impulse.magnitude / Time.fixedDeltaTime) * multiplier;
        foreach (var trigger in colliderTriggers)
        {
            trigger.GetComponent<DetachOnImpact>().CheckImpact(collisionForce);

        }


        /*damage -= collisionForce;
        gameObject.transform.localRotation = rotation;

        if (damage <= 0)
        {
            foreach (var body in detachObjects)
            {
                body.GetComponent<Rigidbody>().isKinematic = false;
                body.GetComponent<Collider>().enabled = true;
                body.GetComponent<Rigidbody>().useGravity = true;

            }
        }
        else
        {

        }*/
    }
}
