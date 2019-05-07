using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Win : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(Mathf.Sin(Time.time*1.8f), Mathf.Sin(Time.time),  Mathf.Sin(Time.time*2.2f)) * 500 * Time.deltaTime * Mathf.Sin(Time.time * 1.5f) * Mathf.Sin(Time.time * 1.3f));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Goat.instance.gameObject) {

            GameManager.instance.Win();
            
        }
    }
}
