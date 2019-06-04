//Attach this script to a GameObject
//Create a Button (Create>UI>Button) and a Text GameObject (Create>UI>Text)
//Click on the GameObject and attach the Button and Text in the fields in the Inspector

//This script outputs the time since the last level load. It also allows you to load a new Scene by pressing the Button. When this new Scene loads, the time resets and updates.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public Button m_MyButton;
    public Text m_MyText;

    void Awake()
    {

        if (m_MyButton != null)
            //Add a listener to call the LoadSceneButton function when the Button is clicked
            m_MyButton.onClick.AddListener(LoadSceneButton);
    }

    void Update()
    {
        //Output the time since the level loaded to the screen using this label
        m_MyText.text = "Time Since Loaded : " + Time.timeSinceLevelLoad;
    }

    void LoadSceneButton()
    {
        //Press this Button to load another Scene
        //Load the Scene named "Scene2"
        SceneManager.LoadScene("Scene2");
    }
}