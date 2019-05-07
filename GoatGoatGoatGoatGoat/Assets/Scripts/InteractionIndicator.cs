using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionIndicator : MonoBehaviour {
    Goat goat;
    Camera cam;
    public Image indicator;
	// Use this for initialization
	void Start () {
        goat = Goat.instance;
        cam = goat.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if(goat.interactionTarget != null && goat.grabbingObject == null && !GameManager.instance.disableInteraction)
        {
            indicator.enabled = true;
            Vector3 pos = cam.WorldToScreenPoint(goat.interactionTarget.transform.position);
            gameObject.transform.position = pos;
            
            float size = (1 - (cam.transform.position - goat.interactionTarget.transform.position).magnitude / goat.interactDistance / goat.size) * 2 + 1f;
            size *= 0.7f;
            size = Mathf.Min(Mathf.Max(size, 0.5f), 1.5f);
            indicator.transform.localScale = new Vector3(size, size, size);
            if (goat.cantInteract)
            {
                indicator.color = new Color(.5f, .5f, .5f, .5f);
            }
            else
            {
                indicator.color = new Color(1, 1, 1, .85f);
            }
        }
        
        else
        {
            indicator.enabled = false;
        }
    }
}
