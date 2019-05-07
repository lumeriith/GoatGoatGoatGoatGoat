using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("Game Manager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private static GameManager _instance;

    public bool gamePaused = false;
    [Header("Winning the game")]
    public bool isVictorious = false;
    private bool wasVictorious = false;
    public string nextLevelName = "";

    [Header("Note: These are automatically set")]

    public GameObject pauseScreen;
    public TextMeshProUGUI pausedText;
    public EasterCredit easterCredit;
    public Image background;

    [Header("Disable Controls")]
    public bool disableIngameControls = false;
    public bool disableAiming = false;
    public bool disableEating = false;
    public bool disableGiving = false;
    public bool disableInteraction = false;
    public bool disableMovement = false;

    [Header("Hide UI Elements")]
    public bool hideGoatUI = false;
    public bool hideCrosshair = false;
    public bool hideFoodIndicator = false;
    public bool hideInteractionIndicator = false;

    [Header("Pre-configurations")]
    public List<GameObject> preplacedObjects = new List<GameObject>();
    public GameObject victoryCamera;
    public GameObject victoryScreen;
    public GameObject messageObject;
    public GameObject victoriousFirework;
    public GameObject deadExplosion;
    public PostProcessingProfile dedProfile;
    private float pauseTime = 0;

    private void Awake()

    {
        foreach(GameObject obj in preplacedObjects)
        {
            if(GameObject.Find("/" + obj.name) == null)
            {
                Instantiate(obj).name = obj.name;
            }

        }
        pauseScreen = GameObject.Find("/Pause Screen");
        pausedText = GameObject.Find("/Pause Screen/Paused Text").GetComponent<TextMeshProUGUI>();
        easterCredit = GameObject.Find("/Pause Screen/Easter Credit").GetComponent<EasterCredit>();
        background = GameObject.Find("/Pause Screen/Background").GetComponent<Image>();
        GameObject.Find("/Pause Screen/Resume Button").GetComponent<Button>().onClick.AddListener(ResumeGame);
        GameObject.Find("/Pause Screen/Main Menu Button").GetComponent<Button>().onClick.AddListener(BackToMainMenu);
        pauseScreen.SetActive(false);
        
    }
    private void Start()
    {
        Goat.instance.goatFPSController.mouseSensitivity = Vector3.one * PlayerPrefs.GetFloat("Sensitivity", 1.5f);
    }

    private void FixedUpdate()
    {
        if (isVictorious)
        {
            if (Goat.instance.goatFPSController.isGrounded)
            {
                Goat.instance.transform.Rotate(new Vector3(0f, 200f, 0f) * Time.deltaTime);
            }
            else
            {
                Goat.instance.transform.Rotate(new Vector3(0f, 1600f, 0f) * Time.deltaTime);
            }
            

            
        }
    }

    private void Update()
    {

        if (Goat.instance.isDead)
        {
            if (!HUDManager.instance.deadIndicator.activeSelf)
            {
                HUDManager.instance.deadIndicator.SetActive(true);
                HUDManager.instance.DeadFlash();
                Goat.instance.cam.GetComponent<PostProcessingBehaviour>().profile = dedProfile;
                Goat.instance.Ungrab();
                Instantiate(deadExplosion, Goat.instance.transform.position, Goat.instance.transform.rotation);
            }
            disableMovement = true;

            disableEating = true;
            disableGiving = true;
            disableInteraction = true;
            hideGoatUI = true;
            hideCrosshair = true;
            hideFoodIndicator = true;
            hideInteractionIndicator = true;
        }

        if (isVictorious) {
            if (!wasVictorious)
            {
                Goat.instance.goatBleater.Bleat();
                wasVictorious = true;
                disableAiming = true;
                Goat.instance.cam.SetActive(false);
                Instantiate(victoryCamera, Goat.instance.transform.position + Goat.instance.transform.forward * -2f * Goat.instance.size, Goat.instance.transform.rotation).name = victoryCamera.name;
                HUDManager.instance.gameObject.transform.parent.gameObject.SetActive(false);
                GameObject vic = Instantiate(victoryScreen);
                Instantiate(victoriousFirework, Goat.instance.transform.position, Goat.instance.transform.rotation);
                vic.transform.Find("Retry").GetComponent<Button>().onClick.AddListener(Restart);
                vic.transform.Find("Next Level").GetComponent<Button>().onClick.AddListener(StartNextLevel);
                vic.transform.Find("Main Menu").GetComponent<Button>().onClick.AddListener(BackToMainMenu);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (gamePaused)
            {
                if (Time.unscaledTime - pauseTime > 180f)
                {
                    pausedText.text = "The Goat Is Sleeping";
                }
            }


        }
        

    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        disableIngameControls = true;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        pausedText.text = "Game Paused";
        pauseTime = Time.unscaledTime;
        easterCredit.popAmount = 0;
        StopCoroutine("BackgroundSemiFadeIn");
        StartCoroutine("BackgroundSemiFadeIn");

        StopCoroutine("PauseTextAnimator");
        StartCoroutine("PauseTextAnimator");
       
    }

    IEnumerator PauseTextAnimator()
    {
        for (float t = 0; t < .5; t += Time.unscaledDeltaTime*2)
        {
            pausedText.characterSpacing = 70f + t*2f*30f;
            pausedText.color = new Color(1,1, 1, .5f+ t);

            yield return null;
        }
        pausedText.characterSpacing = 100;
        pausedText.color = Color.white;
    }

    IEnumerator BackgroundSemiFadeIn()
    {
        for (float a = 0.5f; a < 0.8f; a += 1f * Time.unscaledDeltaTime*1.5f)
        {
            background.color = new Color(0, 0, 0, a);
            
            yield return null;
        }
        background.color = new Color(0, 0, 0, 0.8f);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        disableIngameControls = false;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
    }

    public void BackToMainMenu()
    {
        
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
    public void StartNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void Win()
    {
        isVictorious = true;
    }

    public void Message(string text)
    {
        Message msg = Instantiate(messageObject, HUDManager.instance.transform.parent).GetComponent<Message>();
        if(text.Length < 20)
        {
            msg.lifetime = 3f;
        } else
        {
            msg.lifetime = 6f;
        }
        
        
        msg.GetComponent<Text>().text = text.Replace("\\n", "\n");
    }
    public void Message(string text, float lifetime)
    {
        Message msg = Instantiate(messageObject, HUDManager.instance.transform.parent).GetComponent<Message>();
        msg.lifetime = lifetime;
        msg.GetComponent<Text>().text = text.Replace("\\n","\n");
    }


}
