using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleButtonAnimator : MonoBehaviour {

    public Color activatedColor = new Color(0, 1, 0, 1);
    public Color deactivatedColor = new Color(1, 0, 0, 1);
    private Renderer colourChangeTarget;
    private Interactable interactable;
    public Vector3 pressedOffset = new Vector3(0,0,0.1f);
    private Vector3 originalPosition;
    public float transitionTime = 0.5f;
    
    private void Start()
    {
        originalPosition = transform.position;
        interactable = GetComponent<Interactable>();
        colourChangeTarget = GetComponent<Renderer>();
    }

    void Update () {
        if (interactable.isToggle)
        {
            
            colourChangeTarget.material.color = interactable.GetTrigger() ? activatedColor : deactivatedColor;
            if (interactable.GetTrigger())
            {
                if (transform.position - transform.parent.position != originalPosition + transform.rotation * pressedOffset)
                {
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition + transform.rotation * pressedOffset, pressedOffset.magnitude / transitionTime * Time.deltaTime);
                }

            }
            else
            {
                if (transform.position - transform.parent.position != originalPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition, pressedOffset.magnitude / transitionTime * Time.deltaTime);
                }
            }
        }
        else
        {
            if(Time.time - interactable.lastTriggerTime < transitionTime)
            {
                colourChangeTarget.material.color = activatedColor;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition + transform.rotation * pressedOffset, pressedOffset.magnitude / transitionTime * Time.deltaTime);
            }
            else
            {
                colourChangeTarget.material.color = deactivatedColor;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, pressedOffset.magnitude / transitionTime * Time.deltaTime);
            }
        }

	}


}
