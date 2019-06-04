using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour {
    public int scene_num;
    public float timespend;
    // total number in the scene
    public int total_number;
    // the number left unvisited
    public int remaining_num;
    public int incorrect_staring;
    public int incorrect_voice;

    public static GlobalControl Instance;

    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    private void Awake()
    {
        if( Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if( Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private List<Report_template> testResults = new List<Report_template>();

    // Update is called once per frame
    public void pushResult(Report_template result)
    {
        testResults.Add(result);
    }

    public List<Report_template> getResults()
    {
        return testResults;
    }


}
