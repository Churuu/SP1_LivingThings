﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static AudioManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    [SerializeField]
    private List<AudioClip> musicClips = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> ambienceClips = new List<AudioClip>();

    [SerializeField]
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip pauseMenuMusic;

    [SerializeField]
    private AudioClip winMusic;
    [SerializeField]
    private AudioClip loseMusic;

    [SerializeField]
    private string nextSong = "NextSong";

    [SerializeField]
    private string previousSong = "PreviousSong";

    private int musicIndex = 0;

    private GameObject stemsManager;

    private bool pause = false;
    private bool winLoseStingerPlaying = false;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        stemsManager = FindObjectOfType<StemsManager>().gameObject;
    }

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);

        
    }

    void Update()
    {
        if (winLoseStingerPlaying)
        {
            if (musicSource.clip.name == winMusic.name || musicSource.clip.name == loseMusic.name)
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.loop = true;
                    winLoseStingerPlaying = false;
                }
            }
        }
        else if (!winLoseStingerPlaying)
        {
            if (!pause)
            {
                musicSource.clip = mainMenuMusic;
                musicChange();
            }

        }





    }

    private void musicChange()
    {


        if (!pause)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                musicSource.Pause();
            }
            if (FindObjectOfType<VideoStreamer>() != null)
            {
                if (FindObjectOfType<VideoStreamer>().IsVideoPlaying())
                {
                    PauseBool(true);
                }
                else if (!FindObjectOfType<VideoStreamer>().IsVideoPlaying())
                {
                    PauseBool(false);
                }
            }
        }
        else if (pause)
        {
            musicSource.UnPause();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 ||
            SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (FindObjectOfType<VideoStreamer>() != null)
            {
                if (!FindObjectOfType<VideoStreamer>().IsVideoPlaying())
                {
                    stemsManager.GetComponent<StemsManager>().OnMenuPause();
                    if (musicSource.clip.name != mainMenuMusic.name)
                    {
                        musicSource.clip = mainMenuMusic;
                        if (!musicSource.isPlaying)
                        {
                            musicSource.Play();
                        }


                    }
                    else if (musicSource.clip.name == mainMenuMusic.name)
                    {
                        if (!musicSource.isPlaying)
                        {
                            musicSource.Play();
                        }
                    }
                }

            }
            else
            {
                stemsManager.GetComponent<StemsManager>().OnMenuPause();
                if (musicSource.clip.name != mainMenuMusic.name)
                {
                    musicSource.clip = mainMenuMusic;
                    if (!musicSource.isPlaying)
                    {
                        musicSource.Play();
                    }


                }
                else if (musicSource.clip.name == mainMenuMusic.name)
                {
                    if (!musicSource.isPlaying)
                    {
                        musicSource.Play();
                    }
                }
            }

        }
    }

    public void PauseBool(bool pBool)
    {
        if (pBool)
        {
            stemsManager.GetComponent<StemsManager>().OnMenuPause();
            pause = true;
            musicSource.clip = pauseMenuMusic;
            if (FindObjectOfType<VideoStreamer>() != null)
            {
                if (!FindObjectOfType<VideoStreamer>().IsVideoPlaying())
                {
                    musicSource.Play();
                }
                else if (FindObjectOfType<VideoStreamer>().IsVideoPlaying())
                {
                    musicSource.Pause();
                }
            }
            else if (FindObjectOfType<VideoStreamer>() == null)
            {
                musicSource.Play();
            }

        }
        if (!pBool)
        {
            stemsManager.GetComponent<StemsManager>().OnMenuUnPause();
            pause = false;
            musicSource.Pause();
        }
    }

    public void OnWinStinger()
    {
        winLoseStingerPlaying = true;
        stemsManager.GetComponent<StemsManager>().OnMenuPause();
        musicSource.clip = winMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    public void OnLoseStinger()
    {
        winLoseStingerPlaying = true;
        stemsManager.GetComponent<StemsManager>().OnMenuPause();
        musicSource.clip = loseMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    public void SongRequest()
    {
        musicSource.clip = musicClips[musicIndex];

        musicSource.Play();
    }

    public void SongRequest(int musicI)
    {
        musicIndex = musicI;
        musicSource.clip = musicClips[musicIndex];

        musicSource.Play();
    }

    public void SwitchMusic()
    {
        if (Input.GetButtonDown(nextSong))
        {
            musicIndex++;
            SongRequest();
        }
        else if (Input.GetButtonDown(previousSong))
        {
            musicIndex--;
            SongRequest();
        }
    }

}