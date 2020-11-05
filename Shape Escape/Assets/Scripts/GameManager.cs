using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel;
    public int levelReached;
    public int[] levelScore;
    public int[] levelAttempt;
    public float[] levelTime;

    public int nextlevel;

    public bool returnFromLevel = false;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        //SaveLevelData();// remove
        LoadLevelData();
    }

    public void LoadLevelData()
    {
        // this function is a modified version of the tutorial video by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido 
        LevelData data = SaveSystem.LoadLevel();

        if (data == null)
            return;

        levelReached = data.levelReached;
        levelScore = data.levelScore;
        levelAttempt = data.levelAttempt;
        levelTime = data.levelTime;
    }

    public void SaveLevelData()
    {
        SaveSystem.SaveLevel(this);
    }

    public void ExitLevel()
    {
        SaveLevelData();

        returnFromLevel = true;
        FadeToLevel(0);
    }

    public void StartNextLevel()
    {
        FadeToLevel(nextlevel);
    }

    public void FadeToLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(int level)
    {
        ScreenFader.instance.FadeOut();

        yield return new WaitForSeconds(0.25f);

        if (level > 0) // is not on the main menu
            currentLevel = level - 1; // offset as levels start from scene 1 but level arrays start at 0;

        nextlevel = level + 1; // for loading sequential next level

        SceneManager.LoadScene(level);
    }

    public void LevelWon(int score, float finalTime)
    {
        // save better score
        if (levelScore[currentLevel] < score)
            levelScore[currentLevel] = score;

        // set level best time
        if (levelTime[currentLevel] == 0f)
            levelTime[currentLevel] = finalTime;
        if (finalTime < levelTime[currentLevel])
            levelTime[currentLevel] = finalTime;

        // unlock next level
        if (currentLevel == levelReached)
            levelReached++;

        SaveLevelData();

        GameUIController.instance.ShowWinMenu(score, finalTime);
    }
}
