using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_Ctrl : MonoBehaviour
{
    [SerializeField] private float changeTime;
    public AudioSource Sounds;
    public AudioClip introSound;
    public AudioClip mainMenuClip;

    private void Update()
    {
        if (this.gameObject.CompareTag("IntroCanvas"))
        {
            
            changeTime -= Time.deltaTime;
            if (changeTime <= 0)
            {
                mainMenu();
            }
        }
    }
    private void Start()
    {
        if (this.gameObject.CompareTag("IntroCanvas"))
        {
            Sounds.clip = introSound;
            Sounds.Play();
        }

        else if (this.gameObject.CompareTag("MainMenuCanvas"))
        {
            Sounds.clip = mainMenuClip;
            Sounds.Play();
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }
    public void Placeholder()
    {
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
