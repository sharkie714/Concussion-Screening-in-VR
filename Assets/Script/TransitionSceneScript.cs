using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSceneScript : MonoBehaviour {
    public int scene_num;
    private crossHair _crosshair;


    // Use this for initialization
    void Start()
    {
        _crosshair = crossHair.FindObjectOfType<crossHair>();
        _crosshair.emptyData();
        _crosshair.setCurrenceSceneNum(scene_num);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
