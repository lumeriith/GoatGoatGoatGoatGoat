using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour {
    public GameObject[] titles;
    private int selectedTitleIndex;
    public InputField levelInputField;
    public GameObject nonExistentLevelWarning;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        selectedTitleIndex = Random.Range(0, titles.Length);
        for (int i = 0; i < titles.Length; i++)
        {
            if (i == selectedTitleIndex)
            {
                titles[i].SetActive(true);
            }
            else
            {
                titles[i].SetActive(false);
            }
        }
	}
	

    public void StartGame()
    {
        SceneManager.LoadScene("01");
    }

    public void LoadLevel()
    {
        if (levelInputField.gameObject.activeSelf)
        {

                WarnNonExistentLevel();
                SceneManager.LoadScene(levelInputField.text);

        }
        else
        {
            levelInputField.gameObject.SetActive(true);
        }
    }

    private void WarnNonExistentLevel()
    {
        StopCoroutine("FlashNonExistentLevelWarning");
        StartCoroutine("FlashNonExistentLevelWarning");
    }

    IEnumerator FlashNonExistentLevelWarning()
    {
        yield return new WaitForSeconds(.5f);
        nonExistentLevelWarning.SetActive(true);
        yield return new WaitForSeconds(1f);
        nonExistentLevelWarning.SetActive(false);
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
