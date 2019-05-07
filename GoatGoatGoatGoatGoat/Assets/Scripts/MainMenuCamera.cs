using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {
    public float rotateSpeed = 5f;
    private Camera cam;
	// Use this for initialization
	void Start () {
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 euler = transform.rotation.eulerAngles;
        euler.x = 20 + 10 * Mathf.Sin(Time.time);
        euler.y += rotateSpeed * Time.deltaTime;
        euler.z = Mathf.Sin(Time.time*.7f) * 5;
        transform.rotation = Quaternion.Euler(euler);
        // cam.fieldOfView = 55 + (Time.time % .5f) * 10 / .5f;
    }
}
