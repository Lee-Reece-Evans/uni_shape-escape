using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    //import sound effects 
    [Header("MenuSounds")]
    public AudioClip menuButton;
    public AudioClip levelSelected;
    public AudioClip resumeButton;
    public AudioClip dragDropMenu;

    [Header("GameSounds")]
    public AudioClip playerSplit;
    public AudioClip playerSplitGoalMet;
    public AudioClip playerGoalMet;
    public AudioClip playerHitEnemy;
    public AudioClip playerHitMovingEnemy;
    public AudioClip levelWon;

    [Header("Music")]
    public AudioClip[] MusicTracks;

    //import level Music
    public AudioSource MusicPlayer;
    public AudioSource SoundPlayer;

    private void Awake()
    {
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

    private void Start()
    {
        PlayMusic(0);
    }

    //menu sounds 
    public void PlayMenuButtonSFX()
    {
        SoundPlayer.PlayOneShot(menuButton);
    }

    public void PlayLevelSelectedSFX()
    {
        SoundPlayer.PlayOneShot(levelSelected);
    }

    public void PlayResumeButtonSFX()
    {
        SoundPlayer.PlayOneShot(resumeButton);
    }

    public void PlayDragDropMenuSFX()
    {
        SoundPlayer.PlayOneShot(dragDropMenu);
    }

    //game sounds 
    public void PlayPlayerSplitSFX()
    {
        SoundPlayer.PlayOneShot(playerSplit);
    }

    public void PlayPlayerSplitGoalMetSFX()
    {
        SoundPlayer.PlayOneShot(playerSplitGoalMet, 0.5f);
    }

    public void PlayPlayerGoalMetSFX()
    {
        SoundPlayer.PlayOneShot(playerGoalMet);
    }

    public void PlayPlayerHitEnemySFX()
    {
        SoundPlayer.PlayOneShot(playerHitEnemy);
    }

    public void PlayPlayerHitMovingEnemySFX()
    {
        SoundPlayer.PlayOneShot(playerHitMovingEnemy);
    }

    public void PlayLevelWonSFX()
    {
        SoundPlayer.PlayOneShot(levelWon);
    }

    //music 
    public void PlayMusic(int trackNumber)
    {
        MusicPlayer.clip = MusicTracks[trackNumber];
        MusicPlayer.Play();
    }

}
