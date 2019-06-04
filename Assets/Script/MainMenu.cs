using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;

    public void Demo()
    {
        SceneManager.LoadScene(1);
    }
    public void StartTest()
    {
        SceneManager.LoadScene(2);
    }

    public void EndDemo()
    {
        SceneManager.LoadScene(0);
    }


    public void OnHover(){
        source.PlayOneShot(hover);


    }

    public void OnClick(){
        source.PlayOneShot(click);

    }




}
