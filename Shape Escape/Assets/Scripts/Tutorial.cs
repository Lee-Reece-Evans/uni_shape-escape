using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject swipeText;
    public GameObject splitText;
    public GameObject mergeText;
    public GameObject triangleWall;
    public GameObject circleWall;

    private bool stage1Complete;
    private bool stage2Complete;

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.goals == 1 && !stage1Complete)
        {
            triangleWall.SetActive(false);
            swipeText.SetActive(true);
            stage1Complete = true;
        }
        if (LevelManager.instance.goals == 2 && !stage2Complete)
        {
            circleWall.SetActive(false);
            splitText.SetActive(true);
            mergeText.SetActive(true);
            stage2Complete = true;
        }
    }
}
