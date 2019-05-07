using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gib : MonoBehaviour {

    // Use this for initialization
    public Transform destination;
    Vector3 rotationAxis;
    float rotationSpeed;
    Vector3 initialVector;
    public float initialVelocity = 5f;
    public float speedModifier = 4.5f;
    public float rampUpModifier = 3f;
    float t = 0f;

    private void Start()
    {
        rotationAxis = Random.insideUnitSphere;
        rotationSpeed = Random.Range(200f, 400f);
        initialVector = Random.insideUnitSphere * initialVelocity;
        
    }

    private void Update()
    {
        if(destination == null) {
            return;
        }
        t += rampUpModifier * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        transform.position += Vector3.Lerp(initialVector, destination.position - transform.position, t) * Time.deltaTime * t * speedModifier;
        if((transform.position-destination.position).magnitude < .2f)
        {
            Destroy(gameObject);
        }
    }
}
