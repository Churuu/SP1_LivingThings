﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StemsManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainStemAudioSource;
    [SerializeField] private AudioSource sealStemAudioSource;

    [SerializeField] private AudioSource otterStemAudioSource;

    [SerializeField] private AudioSource frogStemAudioSource;

    [SerializeField]
    private List<AudioClip> music11 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music12 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music13 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music21 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music22 = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> music31 = new List<AudioClip>();

    private List<AudioClip> thisLevelStems = new List<AudioClip>();

    private List<AudioSource> AudioSources = new List<AudioSource>();


    private float mainStemVolume;
    private float sealStemVolume;
    private float otterStemVolume;
    private float frogStemVolume;

    private float savedMapIndex = 4;

    [SerializeField]
    private int stemCount = 1;

    public enum StemFocus
    {
        Otter, Seal, Frog, Stairs
    }
    [SerializeField]
    StemFocus stemFocus;

    private bool stairStemBool = false;


    MenuSystem menuSystem;
    // Use this for initialization
    void Start ()
    {
        mainStemVolume = mainStemAudioSource.volume;
        sealStemVolume = sealStemAudioSource.volume;
        otterStemVolume = otterStemAudioSource.volume;
        frogStemVolume = frogStemAudioSource.volume;
        menuSystem = FindObjectOfType<MenuSystem>();
        mainStemAudioSource = GetComponent<AudioSource>();

        ToOtter();

        savedMapIndex = SceneManager.GetActiveScene().buildIndex;

        AudioSources.Add(mainStemAudioSource);
        AudioSources.Add(sealStemAudioSource);
        AudioSources.Add(otterStemAudioSource);
        AudioSources.Add(frogStemAudioSource);

        UpdateStems();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        print("Saved Index: " + savedMapIndex);
        print("Build Index: " + SceneManager.GetActiveScene().buildIndex);
        if (savedMapIndex != SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex > 2)
        {
            UpdateStems();
            savedMapIndex = SceneManager.GetActiveScene().buildIndex;
        }
        for (int i = 0; i < AudioSources.Count; i++)
        {
            print(AudioSources[i].name + " " + AudioSources[i].isPlaying);
        }
    }

    public void UpdateStems()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 3:
                thisLevelStems = music11;
                stairStemBool = false;
                break;
            case 4:
                thisLevelStems = music12;
                stairStemBool = false;
                break;
            case 5:
                thisLevelStems = music13;
                stairStemBool = false;
                break;
            case 8:
                thisLevelStems = music21;
                stairStemBool = true;
                break;
            case 9:
                thisLevelStems = music22;
                stairStemBool = true;
                break;
            case 10:
                thisLevelStems = music31;
                stairStemBool = false;
                break;
        }
        mainStemAudioSource.clip = thisLevelStems[0];
        sealStemAudioSource.clip = thisLevelStems[1];
        otterStemAudioSource.clip = thisLevelStems[2];
        frogStemAudioSource.clip = thisLevelStems[3];

        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].clip = thisLevelStems[i];
        }
        RestartStems();
        ToOtter();
    }

    public void RestartStems()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            OnMenuUnPause();
            AudioSources[i].Play();
        }
    }

    public void OnMenuPause()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].Pause();
        }
    }

    public void OnMenuUnPause()
    {
        for (int i = 0; i < AudioSources.Count; i++)
        {
            AudioSources[i].UnPause();
        }
    }

    public void ToSeal()
    {
        sealStemAudioSource.volume = sealStemVolume;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = 0;
        stemFocus = StemFocus.Seal;
    }
    public void ToOtter()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = otterStemVolume;
        frogStemAudioSource.volume = 0;
        stemFocus = StemFocus.Otter;
    }
    public void ToFrog()
    {
        sealStemAudioSource.volume = 0;
        otterStemAudioSource.volume = 0;
        frogStemAudioSource.volume = frogStemVolume;
        stemFocus = StemFocus.Frog;
    }

    public void UpdateStemStairs()
    {
        stemFocus = StemFocus.Stairs;
        switch (stemCount)
        {
            case 1:
                mainStemAudioSource.volume = mainStemVolume;
                sealStemAudioSource.volume = 0;
                otterStemAudioSource.volume = 0;
                frogStemAudioSource.volume = 0;
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public void StemCountIncrease()
    {
        ++stemCount;
    }
}
