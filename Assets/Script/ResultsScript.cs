using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ResultsScript : MonoBehaviour{

    public Text timeElapsed;
    public Text incorrectView;
    public Text incorrectSpeaks;

    public int sceneNumber;

    void Start(){

        DataStore dataControl = GameObject.FindObjectOfType<DataStore>();
         List<Report_template> results = dataControl.getResults();

        foreach (Report_template result in results){
            if (result.Scene_num == sceneNumber)
            {
                timeElapsed.text = result.Timespend.ToString();
                incorrectView.text = result.Incorrect_staring.ToString();
                incorrectSpeaks.text = result.Incorrect_voice.ToString();
                break;
            }
        }
     }

    void Update()
    {
      
    }
}
