using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour {
    public bool isToggle = false;
    // Use this for initialization
    public bool isTriggered = false;
    public float lastTriggerTime;
    public bool isPermanent = false;
    public UnityEvent onClick;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public bool GetTrigger()
    {
        if (isTriggered)
        {
            isTriggered = isToggle;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Trigger()
    {
        if(isPermanent && isTriggered) { return; }
        onClick.Invoke();
        audioSource.Play();
        lastTriggerTime = Time.time;
        if (isToggle)
        {
            
            isTriggered = !isTriggered;
            if (isTriggered)
            {
                onActivate.Invoke();
                
            }
            else
            {
                onDeactivate.Invoke();
            }
        }
        else
        {
            isTriggered = true;
        }
       
    }
}
