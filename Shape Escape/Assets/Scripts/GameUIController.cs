using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

// Analytics YouTube material Brackeys https://www.youtube.com/watch?v=iCEQdP1TdwM How to use Unity Analytics 27/11/2019. 

public class GameUIController : MonoBehaviour
{
    public static GameUIController instance;

    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject nextLevelBtn;
    public GameObject fireworks;

    public Image[] stars;
    public Sprite fullStar;

    public Text timerTxt;
    public Text finalTimeTxt;
    public Text attemptTxt;

    public Text WinText;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    private void Start()
    {
        StartCoroutine("ChangeTimeColor");
    }

    private void Update()
    {
        // set timer text
        float time = LevelManager.instance.timer;
        timerTxt.text = Mathf.Floor(time / 60).ToString("00:") + (time % 60).ToString("00");

        // use back button on phone
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
                HidePauseMenu();
            else if (!pauseMenu.activeSelf)
                ShowPauseMenu();
        }
    }

    IEnumerator ChangeTimeColor()
    {
        yield return new WaitForSeconds(LevelManager.instance.threeStarTime);
        timerTxt.color = new Color32(192, 192, 192, 200);
        yield return new WaitForSeconds(LevelManager.instance.twoStarTime - LevelManager.instance.threeStarTime);
        timerTxt.color = new Color32(205, 127, 50, 200);
    }

    public void ShowPauseMenu()
    {
        SFXManager.instance.PlayMenuButtonSFX();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        SFXManager.instance.PlayResumeButtonSFX();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ShowWinMenu(int score, float finalTime)
    {
        SFXManager.instance.PlayLevelWonSFX();
        fireworks.SetActive(true);

        if (score == 1)
        {
            WinText.text = "GOOD!";
            //WinText.color = new Color32(205, 127, 50, 255);
        }
        else if (score == 2)
        {
            WinText.text = "GREAT!";
            //WinText.color = new Color32(192, 192, 192, 255);
        }

        // set star rating
        for (int i = 0; i < score; i++)
        {
            stars[i].sprite = fullStar;
        }

        // set stat text
        finalTimeTxt.text = "TIME: " + Mathf.Floor(finalTime / 60).ToString("00:") + (finalTime % 60).ToString("00");
        attemptTxt.text = "ATTEMPT: " + GameManager.instance.levelAttempt[GameManager.instance.currentLevel];

        // no next level available
        if (GameManager.instance.nextlevel > GameManager.instance.levelScore.Length)
        {
            nextLevelBtn.GetComponent<Button>().interactable = false;
        }

        winMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SFXManager.instance.PlayLevelSelectedSFX();
        GameManager.instance.FadeToLevel(GameManager.instance.currentLevel + 1);
        Analytics.CustomEvent("RestartLevel");
    }

    public void StartNextLevel()
    {
        SFXManager.instance.PlayLevelSelectedSFX();
        GameManager.instance.StartNextLevel();
        Analytics.CustomEvent("StartNextLevel");
    }

    public void ExitLevel()
    {
        SFXManager.instance.PlayMenuButtonSFX();
        SFXManager.instance.PlayMusic(0);
        GameManager.instance.ExitLevel();
    }
}
