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
    public float typingSpeed;
    private TextMeshProUGUI dialogue;
    private Coroutine dialogueCoroutine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void GameOver()
    {
        StartCoroutine(LoadYourAsyncScene("TitleScreen"));
        backgroundImage.SetActive(true);
        StartCoroutine(FadeBackgroundImage(new Color(1, 1, 1, 1), 2f));
        titleText.SetActive(true);
        titleText.GetComponent<TextMeshProUGUI>().SetText("Game Over");
        backButton.SetActive(true);
    }

    public void WinGame()
    {
        StartCoroutine(LoadYourAsyncScene("TitleScreen"));
        backgroundImage.SetActive(true);
        StartCoroutine(FadeBackgroundImage(new Color(1, 1, 1, 1), 2f));
        titleText.SetActive(true);
        titleText.GetComponent<TextMeshProUGUI>().SetText("You Win");
        backButton.SetActive(true);
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

    public void StartDialog(string text)
    {
        dialogueBox.SetActive(true);
        dialogueText.SetActive(true);
        dialogueCoroutine = StartCoroutine(TypeText(text));
    }

    public void HideDialog()
    {
        dialogueBox.SetActive(false);
        dialogueText.SetActive(false);
        StopCoroutine(dialogueCoroutine);
    }

    IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        foreach (char c in text.ToCharArray())
        {
            dialogue.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
