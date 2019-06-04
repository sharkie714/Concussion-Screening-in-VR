using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour {
    public static DataStore Instance;
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

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
