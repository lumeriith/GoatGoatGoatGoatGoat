using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistancedPlatform : MonoBehaviour {

    private GameObject platform;
    private LineRenderer lineRenderer;

    public float distance;
    public bool smoothDamp = true;
    public float smoothTime = 0.5f;
    private Vector3 cv;

	void Start () {
        platform = transform.Find("Platform").gameObject;
        lineRenderer = GetComponent<LineRenderer>();
	}

    public void SetDistance(float dist)
    {
        distance = dist;
    }
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if(platform == null) { platform = transform.Find("Platform").gameObject; }
            if(lineRenderer == null) { lineRenderer = GetComponent<LineRenderer>(); }
            platform.transform.position = transform.position + transform.up * -1 * distance;
            UpdateLineRenderer();
        }

    }


    void UpdateLineRenderer()
    {
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, platform.transform.position);
    }




    void Update () {
        if (Application.isPlaying)
        {
            platform.transform.position = Vector3.SmoothDamp(platform.transform.position, transform.position + transform.up * -1 * distance, ref cv, smoothTime);
            UpdateLineRenderer();
        }

    }

}
