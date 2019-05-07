using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arrow : MonoBehaviour {
    private Image arrow;
    private Color arrowColour;
    private Vector2 originalSizeDelta;
    private RectTransform rt;
    private float lifetime = 4f;

	void Start () {
        arrow = GetComponent<Image>();
        arrowColour = arrow.color;
        arrow.enabled = false;
        rt = GetComponent<RectTransform>();
        originalSizeDelta = rt.sizeDelta;

	}


    public void Indicate(float time)
    {
        lifetime = time;
        StopCoroutine("Indicator");
        StartCoroutine("Indicator");
    }
    IEnumerator Indicator()
    {
        Color color = arrowColour;
        arrow.enabled = true;
        for (float t = 0; t < lifetime; t += Time.deltaTime)
        {
            rt.sizeDelta = originalSizeDelta * (1 + Mathf.Sin(Time.time*9.34f) * 0.2f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
