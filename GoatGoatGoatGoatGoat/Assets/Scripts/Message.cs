using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Message : MonoBehaviour {

    // Use this for initialization
    Vector2 showPos;
    Text text;
    float t;
    RectTransform rt;
    public float lifetime = 2;
	void Start () {
        rt = GetComponent<RectTransform>();
        showPos = rt.anchoredPosition;
        text = GetComponent<Text>();
        Destroy(gameObject, lifetime + 1);
        text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        text.color = new Color(1, 1, 1, Mathf.Min(1f, t*2, lifetime*2+1-t*2));
        if(t < .5)
        {
            rt.anchoredPosition += new Vector2(0f, 30f) * Time.deltaTime;
        }
        text.enabled = true;
	}
}
