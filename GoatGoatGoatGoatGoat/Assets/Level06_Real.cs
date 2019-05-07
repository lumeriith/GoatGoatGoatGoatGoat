using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level06_Real : MonoBehaviour {
    public Image black;

    void Start () {
        StartCoroutine(StartMapRoutine());
        black.color = new Color(0, 0, 0, 1);
	}
	

	IEnumerator StartMapRoutine()
    {
        for(float t=0; t<1f; t += Time.deltaTime)
        {
            black.color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
        Destroy(black.transform.parent.gameObject);
    }
}
