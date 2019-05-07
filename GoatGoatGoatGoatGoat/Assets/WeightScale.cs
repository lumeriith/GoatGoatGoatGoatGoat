using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightScale : MonoBehaviour {
    public float weight;
    public bool turnInvisibleOnPlaying;

    private void Start()
    {
        if (turnInvisibleOnPlaying)
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        weight = 0;
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.lossyScale/2, transform.rotation);

        foreach(Collider c in colliders)
        {
            Goat g = c.GetComponent<Goat>();
            if(g != null && g.goatFPSController.isGrounded)
            {
                weight += g.currentWeight;
                if(g.grabbingObject != null)
                {
                    weight += g.grabbingObject.mass;
                }
                continue;
            }

            Rigidbody rb = c.GetComponent<Rigidbody>();
            if(rb != null && !rb.isKinematic && rb.useGravity)
            {
                weight += rb.mass;
                continue;
            }
            


        }
    }
}
