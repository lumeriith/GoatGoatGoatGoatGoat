using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectSpawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public int maxObjects = 1;
    public new ParticleSystem particleSystem;
    private AudioSource audioSource;
    public List<GameObject> objects = new List<GameObject>();
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Spawn()
    {
        audioSource.Play();
        particleSystem.Play();
        if (objects.Count < maxObjects)
        {
            objects.Add(Instantiate(objectToSpawn, transform.position, transform.rotation));
        }
        else
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
            objects.Add(Instantiate(objectToSpawn, transform.position, transform.rotation));
        }
    }
 	
 
}
