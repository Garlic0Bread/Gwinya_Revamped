using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameAuManager : MonoBehaviour
{
    public string sceneName;

    public AudioSource const_AmbianceSource;
    public AudioClip const_WindClip;

    public AudioSource random_AmbianceSource;
    public AudioClip[] random_AmbianceClips;


    void PlayConstClip()
    {
        
        const_AmbianceSource.Play();
        Debug.Log("Playing: " + const_AmbianceSource.clip.name);
    }
    void PlayRandomClip()
    {
        if (random_AmbianceClips.Length > 0)
        {
            int randomIndex = Random.Range(0, random_AmbianceClips.Length);
            random_AmbianceSource.clip = random_AmbianceClips[randomIndex];
            random_AmbianceSource.Play();

            Debug.Log("Playing: " + random_AmbianceSource.clip.name);
        }
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
    public void Start()
    {
        PlayRandomClip();
        PlayConstClip();
    }
}
