using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [Header("Weight Settings")]
    public float currentWeight = 5f;
    public float minWeight = 1f;
    public float maxWeight = 20f;
    
    public float weightTransferPerSecond = 10f;

    private Rigidbody rb;

    private Vector3 originalScale;
    private float originalWeight;
    private float lastChangeTime;

    public Transform growPivot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.mass = currentWeight;
        }
        
        originalScale = transform.localScale;
        originalWeight = currentWeight;
    }
    
    public void SetWeight(float weight)
    {
        currentWeight = weight;
        if (weight <= 0)
        {
            Debug.Log("꺼-억");
            Destroy(gameObject);
            
            return;
        }
        if (rb != null)
        {
            rb.mass = weight;
        }
        
        SetProperScale();
        lastChangeTime = Time.time;
    }

    private void FixedUpdate()
    {
        if(Time.time - lastChangeTime < 1f && rb != null)
        {
            rb.WakeUp();
        }
    }
    public void SetProperScale()
    {
        if(growPivot != null)
        {
            Vector3 pivotOriginalPos = growPivot.transform.position;
            transform.localScale = originalScale * Mathf.Pow(currentWeight / originalWeight, 1f / 3f);
            transform.position += pivotOriginalPos - growPivot.transform.position;
        }
        else
        {
            transform.localScale = originalScale * Mathf.Pow(currentWeight / originalWeight, 1f / 3f);
        }
        
        
    }







}
