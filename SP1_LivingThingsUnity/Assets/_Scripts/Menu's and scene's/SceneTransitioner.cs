﻿//Skapad av Robin Nechovski 07-02-2019

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneTransitioner : MonoBehaviour
{
    public RawImage fadeImage;
    public float fadeSpeed;

    string sceneToLoad;
    static SceneTransitioner scene;
    float target = 0;
    VideoStreamer videoStreamer;

    float alpha
    {
        get
        {
            return fadeImage.color.a;
        }
        set
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, value);
        }

    }

    void Start()
    {
        DontDestroyOnLoad(this);

        if (scene == null)
            scene = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        Fade(target);
    }

    public void LoadScene(string name)
    {
        sceneToLoad = name;
        Fade(1f);
        InvokeRepeating("SwitchScene", 0, Time.deltaTime);
    }

    void SwitchScene()
    {
        Fade(1f);
        if (alpha >= .99f)
        {
            SceneManager.LoadScene(sceneToLoad);
            Fade(0f);
            CancelInvoke();
        }
    }

    public void Fade(float target)
    {
        this.target = target;
        alpha = Mathf.Lerp(alpha, target, fadeSpeed * Time.deltaTime);
    }
}

