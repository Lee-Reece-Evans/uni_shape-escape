using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float twoStarTime;
    public float threeStarTime;

    public int score;
    public float timer;

    public int totalGoals;
    public int goals = 0;

    public int musicTrack = 1;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SFXManager.instance.MusicPlayer.clip.name != SFXManager.instance.MusicTracks[musicTrack].name)
            SFXManager.instance.PlayMusic(musicTrack);

        IncreaseAttempt();
        goals = 0;
        totalGoals = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    public void GoalMet()
    {
        goals++;
        // level won
        if (goals == totalGoals)
        {
            float finalTime = timer;

            if (finalTime <= threeStarTime)
                score = 3;
            else if (finalTime <= twoStarTime)
                score = 2;
            else
                score = 1;

            GameManager.instance.LevelWon(score, finalTime);
        }
    }

    public void IncreaseAttempt()
    {
        GameManager.instance.levelAttempt[GameManager.instance.currentLevel]++;
        GameManager.instance.SaveLevelData();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
