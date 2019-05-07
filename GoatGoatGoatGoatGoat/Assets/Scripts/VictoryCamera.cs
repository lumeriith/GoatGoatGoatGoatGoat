using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCamera : MonoBehaviour {
    private Camera cam;
    // Use this for initialization
    void Start()
    {
        
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.LookAt(Goat.instance.transform);
        cam.transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time * 4)*15), Space.Self);
    }
}
