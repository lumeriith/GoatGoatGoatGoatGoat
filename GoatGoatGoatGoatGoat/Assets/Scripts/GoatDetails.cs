using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoatDetails : MonoBehaviour {
    public TextMeshProUGUI currentWeight;
    public TextMeshProUGUI minimumWeight;
    public TextMeshProUGUI maximumWeight;
    public Image currentImage;

    private Goat goat;

	// Use this for initialization
	void Start () {
        goat = Goat.instance;	
	}

    public void Flash()
    {
        if (gameObject.activeInHierarchy)
        {
            StopCoroutine("Flasher");
            StartCoroutine("Flasher");
        }

    }

    IEnumerator Flasher()
    {
        for (float size = 1.3f; size >= 1f; size -= Time.deltaTime * 3)
        {
            gameObject.transform.localScale = new Vector3(size, size, size);
            yield return null;
        }
        gameObject.transform.localScale = new Vector3(1, 1, 1);

    }
    // Update is called once per frame
    void Update () {
        currentWeight.text = Units.readableText(goat.currentWeight);
        minimumWeight.text = Units.readableText(goat.minWeight);
        maximumWeight.text = Units.readableText(goat.maxWeight);
        if(goat.currentWeight >= goat.maxWeight)
        {
            currentImage.fillAmount = 1f;
        }
        else
        {
            currentImage.fillAmount = (goat.currentWeight - goat.minWeight) / (goat.maxWeight - goat.minWeight);
        }
        
	}
}
