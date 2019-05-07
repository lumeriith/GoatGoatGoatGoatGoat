using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlwaysLookAtCamera : MonoBehaviour {
    private Camera cam;
    

    private void Start()
    {
        cam = Camera.main;
    }
 	
    private void Update()
    {
        transform.rotation = cam.transform.rotation;
        
    }
 
}
