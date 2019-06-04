using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize_datastore : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        DataStore store = new DataStore();
        DontDestroyOnLoad(store);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
