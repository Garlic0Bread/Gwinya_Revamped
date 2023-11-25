using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_Ctrl : MonoBehaviour
{
    [SerializeField] private float changeTime;
    public AudioSource Intro;
    public AudioSource MainMenu;
    public AudioSource Death;

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
            Intro.Play();
        }

        else if (this.gameObject.CompareTag("MainMenuCanvas"))
        {
            MainMenu.Play();
        }

        else if (this.gameObject.CompareTag("GameManager"))
        {
            MainMenu.Play();
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel(int sceneNumber)
    {
        StartCoroutine(WaitBeforeLoading(sceneNumber));
    }


    IEnumerator WaitBeforeLoading(int levelNumber)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelNumber);
    }

    public void Placeholder()
    {
        SceneManager.LoadScene(5);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
