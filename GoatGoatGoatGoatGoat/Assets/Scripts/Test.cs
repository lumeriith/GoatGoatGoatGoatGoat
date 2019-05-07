using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    // Use this for initialization
    float startTime;
    public float magnitude;
    public GameObject gib;

	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(((Time.time - startTime) % 2f) < 1)
        {
            transform.Translate(0, magnitude*Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, -magnitude * Time.deltaTime, 0);
        }
        

    }
}
