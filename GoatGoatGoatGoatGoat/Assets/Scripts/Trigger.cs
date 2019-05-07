using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Trigger : MonoBehaviour {

    public int activationCount=0;
    public bool activateOnlyOnce = false;
    [Header("Conditions")]
    public bool ifGoatIsGrabbing = false;
    public UnityEvent action;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Goat.instance.gameObject)
        {
            if (!activateOnlyOnce || activationCount == 0)

            {
                if(!ifGoatIsGrabbing || Goat.instance.grabbingObject != null)
                {
                    activationCount++;
                    action.Invoke();
                }

            }
        }
    }
}
