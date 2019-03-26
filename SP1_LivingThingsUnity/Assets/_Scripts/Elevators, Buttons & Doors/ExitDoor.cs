﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{

    bool seal = false, frog = false, otter = false;

    public string sceneToLoad;
    public bool playCutscene;

    void OnTriggerEnter2D(Collider2D col)
    {
        var other = col.gameObject;
        if (other.CompareTag("Otter"))
            otter = true;
        else if (other.CompareTag("Seal"))
            seal = true;
        else if (other.CompareTag("Frog"))
            frog = true;

        if (otter && seal && frog && playCutscene)
            FindObjectOfType<VideoStreamer>().PrepareVideo();
        else if (otter && seal && frog)
        {
            if (SceneManager.GetActiveScene().buildIndex != 6)
            {
                FindObjectOfType<AudioManager>().OnWinStinger();
                Invoke("LoadSceneTimer", FindObjectOfType<AudioManager>().GetComponent<AudioSource>().clip.length);
            }
            else
                FindObjectOfType<SceneTransitioner>().LoadScene(sceneToLoad);


        }

            
    }

    private void LoadSceneTimer()
    {
        FindObjectOfType<SceneTransitioner>().LoadScene(sceneToLoad);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var other = col.gameObject;
        if (other.CompareTag("Otter"))
            otter = false;
        else if (other.CompareTag("Seal"))
            seal = false;
        else if (other.CompareTag("Frog"))
            frog = false;
    }
}
