using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
	public bool isOpen;
	private Vector3 originalPosition;
	public Vector3 openOffset = new Vector3(3f,0f,0f);
	public float smoothTime = .5f;
    private Vector3 cv;
	public void Open()
	{
		isOpen = true;
	}

	public void Close()
	{
		isOpen = false;
	}

	public void Toggle()
	{
		isOpen = !isOpen;
	}

	private void Awake()
	{
		originalPosition = transform.position;
	}

	private void Update()
	{
		if (isOpen)
		{
			transform.position = Vector3.SmoothDamp(transform.position, originalPosition + transform.rotation * openOffset, ref cv, smoothTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, originalPosition, ref cv, smoothTime);
        }
	}
	
	
}
