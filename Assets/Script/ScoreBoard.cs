using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour {

    public Text con;
    public GameObject scoreEntry;

	// Use this for initialization
	void Start () {

        DataStore dataControl = GameObject.FindObjectOfType<DataStore>();
        List<Report_template> results = dataControl.getResults();

        float total_TimeSpend = 0;
        int total_Incorrect_staring = 0;
        int total_Incorrect_voice = 0;
        string testName = "";

        foreach(Report_template result in results){
            
            if(result.Scene_num==1){
                testName = "Demo";;
            }else if (result.Scene_num == 3){
                testName = "Test 1";
            }else if (result.Scene_num == 5){
                testName = "Test 2";
            }else if (result.Scene_num == 7){
                testName = "Test 3";
            }

            addToScoreBoard(testName, result);

            total_TimeSpend += result.Timespend;
            total_Incorrect_staring += result.Incorrect_staring;
            total_Incorrect_voice += result.Incorrect_voice;

            if (total_TimeSpend / 3 > 40 && total_TimeSpend / 3 <= 45)
                con.text = "Please try again,results are not conclusive!";
            else if (total_TimeSpend / 3 < 40)
                con.text = "You do not have a concussion!";
            else
                con.text = "You have a concussion!";
            
        }

      //  addToScoreBoard("Totals", total_TimeSpend, total_Incorrect_staring, total_Incorrect_voice);
        addToScoreBoard("Average", total_TimeSpend.Equals(0.0) ? 0.0f: total_TimeSpend/3, total_Incorrect_staring==0 ? 0 : (total_Incorrect_staring/3), total_Incorrect_voice==0 ? 0 : (total_Incorrect_voice/3));

	}


    void addToScoreBoard(string testName, Report_template result){
        GameObject go = (GameObject)Instantiate(scoreEntry);
        go.transform.SetParent(this.transform, false);
        go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        go.transform.Find("TestName").GetComponent<Text>().text = testName;
        go.transform.Find("TimeTaken").GetComponent<Text>().text = result.Timespend.ToString();
        go.transform.Find("IncorrectViews").GetComponent<Text>().text = result.Incorrect_staring.ToString();
        go.transform.Find("IncorrectSpeaks").GetComponent<Text>().text = result.Incorrect_voice.ToString();
    }

    void addToScoreBoard(string testName, float Timespend, int Incorrect_staring, int Incorrect_voice)
    {
        GameObject go = (GameObject)Instantiate(scoreEntry);
        go.transform.SetParent(this.transform, false);
        go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        go.transform.Find("TestName").GetComponent<Text>().text = testName;
        go.transform.Find("TimeTaken").GetComponent<Text>().text = Timespend.ToString();
        go.transform.Find("IncorrectViews").GetComponent<Text>().text = Incorrect_staring.ToString();
        go.transform.Find("IncorrectSpeaks").GetComponent<Text>().text = Incorrect_voice.ToString();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
