using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject canvas;
    public GameObject events;

    public GameObject startButton;
    public GameObject creditsButton;
    public GameObject instructionButton;
    public GameObject backButton;

    public GameObject titleText;
    public GameObject instructionText;
    public GameObject creditsText;

    public GameObject backgroundImage;

    public GameObject dialogueBox;
    public GameObject dialogueText;
    private TextMeshProUGUI dialogue;
    private Coroutine dialogueCoroutine;

    public GameObject healthBar;
    private Slider healthSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
        } else
        {
            Destroy(canvas);
            Destroy(events);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogue = dialogueText.GetComponent<TextMeshProUGUI>();
        healthSlider = healthBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        checkEnemyCount();
    }

    public void ShowInstructions()
    {
        HideMainMenuButtons();
        instructionText.SetActive(true);
        backButton.SetActive(true);
    }

    public void ShowCredits()
    {
        HideMainMenuButtons();
        creditsText.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        creditsText.SetActive(false);
        instructionText.SetActive(false);
        backButton.SetActive(false);
        titleText.GetComponent<TextMeshProUGUI>().SetText("Light and Darkness");
        ShowMainMenuButtons();
    }

    public void StartGame()
    {
        HideMainMenuButtons();
        backButton.SetActive(false);
        titleText.SetActive(false);
        StartCoroutine(LoadYourAsyncScene("LevelOne"));
        StartCoroutine(FadeBackgroundImage(new Color(1, 1, 1, 0), 2f));
        backgroundImage.SetActive(false);
        healthBar.SetActive(true);
    }

    public void GameOver()
    {
        healthBar.SetActive(false);
        StartCoroutine(LoadYourAsyncScene("TitleScreen"));
        backgroundImage.SetActive(true);
        StartCoroutine(FadeBackgroundImage(new Color(1, 1, 1, 1), 2f));
        titleText.SetActive(true);
        titleText.GetComponent<TextMeshProUGUI>().SetText("Game Over");
        backButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void WinGame()
    {
        healthBar.SetActive(false);
        StartCoroutine(LoadYourAsyncScene("TitleScreen"));
        backgroundImage.SetActive(true);
        StartCoroutine(FadeBackgroundImage(new Color(1, 1, 1, 1), 2f));
        titleText.SetActive(true);
        titleText.GetComponent<TextMeshProUGUI>().SetText("You Win");
        backButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideMainMenuButtons()
    {
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        instructionButton.SetActive(false);
    }

    private void ShowMainMenuButtons()
    {
        startButton.SetActive(true);
        creditsButton.SetActive(true);
        instructionButton.SetActive(true);
    }

    public void SwitchScenes(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator FadeBackgroundImage(Color endValue, float duration)
    {
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        Debug.Log("Loading " + scene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void StartDialog(string text, float typingSpeed)
    {
        dialogueBox.SetActive(true);
        dialogueText.SetActive(true);
        dialogueCoroutine = StartCoroutine(TypeText(text, typingSpeed));
    }

    public void HideDialog()
    {
        dialogueBox.SetActive(false);
        dialogueText.SetActive(false);
        StopCoroutine(dialogueCoroutine);
    }

    IEnumerator TypeText(string text, float typingSpeed)
    {
        dialogue.text = "";
        foreach (char c in text.ToCharArray())
        {
            dialogue.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void adjustHealth(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    public void checkEnemyCount()
    {
        Scene current = SceneManager.GetActiveScene();
        if ((current.name.Equals("LevelOne")) || (current.name.Equals("LevelTwo")) || (current.name.Equals("LevelThree"))){
            if (GameObject.Find("Enemies").transform.childCount == 0)
                
            {
                if (current.name.Equals("LevelOne"))
                {
                    StartCoroutine(LoadYourAsyncScene("LevelTwo"));
                }
                if (current.name.Equals("LevelTwo"))
                {
                    StartCoroutine(LoadYourAsyncScene("LevelThree"));
                }
                if (current.name.Equals("LevelThree"))
                {
                    //LoadYourAsyncScene("LevelTwo");
                    //Win Scene here
                }
            }
        }
    }

}
