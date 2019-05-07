using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    public static HUDManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<HUDManager>();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private static HUDManager _instance;

    [Header("Note: Thesse are automatically set")]
    public GoatDetails goatDetails;
    public InteractionIndicator interactionIndicator;
    public FoodIndicator foodIndicator;
    public Image crosshair;
    public Image weightWarning;
    public GameObject deadIndicator;
    public WeightIndicator weightIndicator;
	// Use this for initialization
	void Start () {
        crosshair = transform.parent.Find("Crosshair").GetComponent<Image>();
        weightWarning = transform.parent.Find("Weight Warning").GetComponent<Image>();
        goatDetails = transform.parent.GetComponentInChildren<GoatDetails>();
        interactionIndicator = transform.parent.GetComponentInChildren<InteractionIndicator>();
        foodIndicator = transform.parent.GetComponentInChildren<FoodIndicator>();
        deadIndicator = transform.parent.Find("Dead Indicator").gameObject;
        weightIndicator = transform.parent.Find("Weight Indicator").GetComponent<WeightIndicator>();
        deadIndicator.SetActive(false);
        weightWarning.color = new Color(0, 0, 0, 0);
    }
    private void Update()
    {
        goatDetails.transform.parent.gameObject.SetActive(!GameManager.instance.hideGoatUI);
        crosshair.gameObject.SetActive(!GameManager.instance.hideCrosshair);
        foodIndicator.gameObject.SetActive(!GameManager.instance.hideFoodIndicator);
        interactionIndicator.gameObject.SetActive(!GameManager.instance.hideInteractionIndicator);

    }
    public void WarnWeight()
    {
        StopCoroutine("WeightWarner");
        StartCoroutine("WeightWarner");
    }

    IEnumerator WeightWarner()
    {
        float size;
        for(float t = 0; t <= 1f; t += Time.deltaTime)
        {
            size = Mathf.Max(1f, 1.4f-t*3f);
            weightWarning.gameObject.transform.localScale = new Vector3(size, size, size);
            weightWarning.color = new Color(Mathf.Max(0,1-t*6), 0, 0, Mathf.Min(0.7f, 2- t*2));
            yield return null;
        }
        weightWarning.color = new Color(0, 0, 0, 0);

    }

    public void DeadFlash()
    {
        if (!gameObject.activeInHierarchy) { return; }
        StopCoroutine("DeadFlasher");
        StartCoroutine("DeadFlasher");
    }


    IEnumerator DeadFlasher()
    {
        for(float t=0;t<=0.6f; t += Time.deltaTime)
        {
            deadIndicator.transform.rotation = Quaternion.Euler( new Vector3(0, 0, Mathf.Sin(Time.time * 40) * 30) * (.6f - t));
            yield return null;
        }
        deadIndicator.transform.rotation = Quaternion.identity;
    }



    
}
