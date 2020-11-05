using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;

    public Animator anim;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public void FadeIn()
    {
        anim.Play("FadeIn", 0, 0);
    }

    public void FadeOut()
    {
        anim.Play("FadeOut", 0, 0);
    }
}
