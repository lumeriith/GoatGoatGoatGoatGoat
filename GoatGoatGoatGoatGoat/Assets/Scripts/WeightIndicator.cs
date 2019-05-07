using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeightIndicator : MonoBehaviour {
    public GameObject cubeImage;
    public TextMeshProUGUI detail;


    private Rigidbody lastTarget;



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
        if (goat.interactionTarget != null && goat.interactionTarget.layer == LayerMask.NameToLayer("Movable"))
        {
            if(lastTarget == null || lastTarget.gameObject != goat.interactionTarget)
            {
                lastTarget = goat.interactionTarget.GetComponent<Rigidbody>();
                Flash();
            }

            cubeImage.SetActive(true);
            detail.enabled = true;
            if(lastTarget.mass > 0.001f)
            {
                detail.text = Units.readableText(lastTarget.mass);
            }
            else
            {
                detail.text = Units.readableText(goat.grabbedObjectOriginalMass);
            }
            
        }
        else
        {
            cubeImage.SetActive(false);
            detail.enabled = false;
            lastTarget = null;
        }
	}
}
