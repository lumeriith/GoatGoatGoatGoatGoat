using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterGoat : MonoBehaviour {
    private float birthTime;
    private Vector3 velocity;
    private float angle;
    public float maxSpeed = 10f;
    public float maxAngle = 75f;
    public float lifeTime = 1f;
    public float gravity = 5f;
	// Use this for initialization
	void Start () {
        birthTime = Time.unscaledTime;
        velocity = (Vector3)Random.insideUnitCircle * maxSpeed + new Vector3(maxSpeed,-maxSpeed/4);
        angle = Random.value * maxAngle * 2 - maxAngle;
    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.unscaledTime - birthTime;
        if (t > lifeTime)
        {
            Destroy(gameObject);
        }
        gameObject.transform.position += velocity * Time.unscaledDeltaTime;
        gameObject.transform.Rotate(new Vector3(0, 0, angle * Time.unscaledDeltaTime));
        velocity.y -= gravity * Time.unscaledDeltaTime;
	}
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
