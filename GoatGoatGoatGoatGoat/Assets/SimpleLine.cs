using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SimpleLine : MonoBehaviour {
    public GameObject end;
    private LineRenderer lineRenderer;

	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, end.transform.position);
    }
}
