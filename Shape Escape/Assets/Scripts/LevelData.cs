using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    // following code is a modified version of the tutorial video by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido 
    public int levelReached;
    public int[] levelScore;
    public int[] levelAttempt;
    public float[] levelTime;

    public LevelData (GameManager gm)
    {
        levelReached = gm.levelReached;
        levelScore = gm.levelScore;
        levelAttempt = gm.levelAttempt;
        levelTime = gm.levelTime;
    }
}


