using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelText : MonoBehaviour {

    float t
    {
        get
        {
            return Time.time - birthTime;
        }
    }
    float birthTime;
	// Use this for initialization
	void Start () {
        transform.position += transform.right * -8f;
        birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta;
        if (t < 2)
        {
            delta = transform.right * (5-t*2);
        }
        else
        {
            delta = transform.right;
        }
        
        transform.position += delta * Time.deltaTime;
	}
}
