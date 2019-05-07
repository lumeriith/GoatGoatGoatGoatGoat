using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level06 : MonoBehaviour {

    public GameObject noGoBack;
    public Light emergencyLight;
    public Image black;

    public void OpenFloor()
    {
        StartCoroutine(OpenFloorRoutine());
    }


	IEnumerator OpenFloorRoutine()
    {
        noGoBack.SetActive(true);
        for (float t = 0; t < 5f; t += Time.deltaTime)
        {
            Goat.instance.ApplyCameraShake(.1f);
            emergencyLight.intensity = Mathf.Sin(Time.time * 5) / 2 + .75f;
            yield return null;
        }
        emergencyLight.enabled = false;
        yield return new WaitForSeconds(2f);
        GameObject.Find("The Floor").SetActive(false);
        while(Goat.instance.transform.position.y > -20)
        {
            yield return new WaitForSeconds(.5f);
        }
        for(float t = 0; t < 3f; t += Time.deltaTime)
        {
            black.color = new Color(0, 0, 0, t / 3);
            yield return null;
        }

        GameManager.instance.StartNextLevel();
    }
}
