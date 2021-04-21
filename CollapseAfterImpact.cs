using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseAfterImpact : MonoBehaviour
{
    [Tooltip("Force multiplier. Multiplied force will be subtracted from collider health")]
    public float multiplier = 0.001F;
    [Tooltip("Mass of the object")]
    public float mass = 10f;
    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        float collisionForce = (collision.impulse.magnitude / Time.fixedDeltaTime) * multiplier;
        if(collisionForce >= 100)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().mass = mass;
        }
    }
    
}
