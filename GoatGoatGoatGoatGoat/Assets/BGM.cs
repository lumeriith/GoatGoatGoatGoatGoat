using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGM : MonoBehaviour {
    private void Awake()
    {
        if (FindObjectsOfType<BGM>().Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);

    }


}
