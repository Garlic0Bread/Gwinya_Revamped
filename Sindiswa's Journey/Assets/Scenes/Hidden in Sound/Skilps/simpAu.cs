using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class simpAu : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioSource menuSource;
    public AudioClip firstMenuClip;
    public AudioClip secondMenuClip;
    public AudioClip introClip;

    public string sceneName;

    void Start()
    {
        PlayRandomTheme();
        PlayIntroClip();
    }

    void PlayIntroClip()
    {
        menuSource.clip = introClip;
        menuSource.Play();
        Invoke("PlayRandomTheme", introClip.length); // Switch to the theme song after the intro clip has finished playing
        Debug.Log("Playing" + introClip + "introLength: "+ introClip.length);
    }

    public void PlayRandomTheme()
    {
        float introDuration = introClip.length;
        menuSource = GetComponent<AudioSource>();
        AudioClip selectedClip = Random.Range(0, 2) == 0 ? firstMenuClip : secondMenuClip;

        menuSource.clip = selectedClip;
        menuSource.Play();
        Debug.Log("Playing" + selectedClip);

        
    }
    public void ChangeScene()
    {
        if (Input.GetKey(KeyCode.E))
        {

            SceneManager.LoadScene(sceneName);
        }

    }

    public void Update()
    {
        ChangeScene();
    }
}
