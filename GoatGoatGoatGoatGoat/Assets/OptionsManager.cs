using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour {
    public Text currentSensitivity;
    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel > 0)
        {
            PlayerPrefs.SetFloat("Sensitivity", Mathf.Clamp(PlayerPrefs.GetFloat("Sensitivity", 1f) + .1f, 0.5f, 5f));
        } else if (scrollWheel < 0)
        {
            PlayerPrefs.SetFloat("Sensitivity", Mathf.Clamp(PlayerPrefs.GetFloat("Sensitivity", 1f) - .1f,0.5f,5f));
        }


        currentSensitivity.text = PlayerPrefs.GetFloat("Sensitivity", 1.5f).ToString("0.0");
        Goat.instance.goatFPSController.mouseSensitivity = Vector3.one * PlayerPrefs.GetFloat("Sensitivity", 1.5f);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }
    }

   


}
