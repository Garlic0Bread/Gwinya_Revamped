using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_Ctrl : MonoBehaviour
{
    [SerializeField] private float changeTime;

    private void FixedUpdate()
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
