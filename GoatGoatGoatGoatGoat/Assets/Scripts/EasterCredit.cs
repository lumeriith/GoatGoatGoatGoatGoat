using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EasterCredit : MonoBehaviour {
    public GameObject easterGoat;
    private TextMeshProUGUI creditText;
    private Image goatImage;
	// Use this for initialization
	void Start () {
        goatImage = GetComponentInChildren<Image>();
        creditText = GetComponentInChildren<TextMeshProUGUI>();
	}
    public float popAmount;
	// Update is called once per frame
	void Update () {
        popAmount -= Time.unscaledDeltaTime*2;
        if (popAmount < 0) { popAmount = 0; }
        goatImage.color = new Color(1, 1, 1, .1f + (popAmount / 10) * 0.9f);
        creditText.color = goatImage.color;
	}

    public void Pop()
    {
        popAmount += 1.3f;
        if (popAmount > 10) {
            popAmount = 10f;
            Instantiate(easterGoat, goatImage.transform);
        }

    }
}
