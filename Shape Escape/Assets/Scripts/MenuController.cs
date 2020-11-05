using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;

    public static MenuController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (GameManager.instance.returnFromLevel)
        {
            levelMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(mainMenu.activeSelf && !optionsMenu.activeSelf)
                ExitApplication();
            else if (optionsMenu.activeSelf)
                HideOptionsMenu();
            else if (levelMenu.activeSelf)
                TransitiontToMenu("MainMenu");
        }
    }

    public void TransitiontToMenu(string menu)
    {
        ScreenFader.instance.FadeIn();

        if (menu == "MainMenu")
        {
            ShowMainMenu();
            HideLevelMenu();
        }
        if (menu == "LevelMenu")
        {
            ShowLevelMenu();
            HideMainMenu();
        }
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void ShowOptionsMenu()
    {
        
        optionsMenu.SetActive(true);
    }

    public void ShowLevelMenu()
    {
        levelMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void HideOptionsMenu()
    {
        SFXManager.instance.PlayMenuButtonSFX();
        optionsMenu.SetActive(false);
    }

    public void HideLevelMenu()
    {
        SFXManager.instance.PlayMenuButtonSFX();
        levelMenu.SetActive(false);
    }

    public void ExitApplication()
    {
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        ScreenFader.instance.FadeOut();

        yield return new WaitForSeconds(0.25f);
        Application.Quit();
    }
}
