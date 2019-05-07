using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodIndicator : MonoBehaviour {
    public GameObject foodImage;
    public TextMeshProUGUI detail;
    public Image ring;
    private Goat goat;
    public Food lastEatTarget;

    Camera cam;

    // Use this for initialization
    void Start () {
        goat = Goat.instance;
        cam = goat.GetComponentInChildren<Camera>();
    }
	
    public void Flash()
    {
        if (gameObject.activeInHierarchy) {
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
        Food target;

        if(goat.eatingObject != null)
        {
            target = goat.eatingObject;
        }
        else if( goat.eatTarget != null)
        {
            target = goat.eatTarget;
        }
        else
        {
            target = null;
        }
        
		if(target != null)
        {

            if(lastEatTarget == null || lastEatTarget != target)
            {
                Flash();
                lastEatTarget = target;
            }

            foodImage.SetActive(true);
            ring.enabled = true;
            //Vector3 pos = cam.WorldToScreenPoint(goat.eatTarget.transform.position);
            //gameObject.transform.position = pos;
            detail.enabled = true;
            detail.text = Units.readableText(target.minWeight) + "/" +
                Units.readableText(target.currentWeight) + "/" +
                Units.readableText(target.maxWeight);
            ring.fillAmount = (target.currentWeight- target.minWeight) / (target.maxWeight- target.minWeight);
        }
        else
        {
            lastEatTarget = null;
            foodImage.SetActive(false);
            detail.enabled = false;
            ring.enabled = false;
        }
	}
}
