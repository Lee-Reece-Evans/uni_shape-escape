using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject[] level;
    public Sprite blankStar;
    public Sprite fullStar;

    void Start()
    {
        SetLevelInfo();
    }

    private void SetLevelInfo()
    {
        // loop through level buttons
        for (int i = 0; i < level.Length; i++)
        {
            //unlock levels
            if (i <= GameManager.instance.levelReached)
                level[i].transform.Find("Locked").gameObject.SetActive(false);

            // if the level is unlocked...
            if (!level[i].transform.Find("Locked").gameObject.activeSelf)
            {
                // set levels stars
                for (int j = 0; j < GameManager.instance.levelScore[i]; j++)
                {
                    Image star = level[i].transform.Find("Stars").gameObject.transform.GetChild(j).GetComponent<Image>();
                    star.sprite = fullStar;
                }

                // set level number text
                level[i].transform.Find("Button").gameObject.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                // set level best time text
                level[i].transform.Find("Button").gameObject.transform.GetChild(1).GetComponent<Text>().text = "BEST TIME: " +
                                                  Mathf.Floor(GameManager.instance.levelTime[i] / 60).ToString("00:") + (GameManager.instance.levelTime[i] % 60).ToString("00");
                // set level attempts
                level[i].transform.Find("Button").gameObject.transform.GetChild(2).GetComponent<Text>().text = "ATTEMPTS: " + GameManager.instance.levelAttempt[i];
            }
        }
    }

    public void StartLevel(int level)
    {
        SFXManager.instance.PlayLevelSelectedSFX();
        GameManager.instance.FadeToLevel(level);
    }
}
