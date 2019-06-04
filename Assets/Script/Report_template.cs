using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Report_template : MonoBehaviour {

    private int scene_num;
    private float timespend;
    // total number in the scene
    private int total_number;
    // the number left unvisited
    private int remaining_num;
    private int incorrect_staring;
    private int incorrect_voice;
    private int testSkipped = 1;

    public Report_template(int _scene_name, float _timespend, int _total_number, int _remaining_num, int _incorrect_staring, int _incorrect_voice, int _testSkipped)
    {
        scene_num = _scene_name;
        timespend = _timespend;
        total_number = _total_number;
        incorrect_staring = _incorrect_staring;
        incorrect_voice = _incorrect_voice;
        Remaining_num = _remaining_num;
        testSkipped = _testSkipped;
    }

    public int Scene_num
    {
        get
        {
            return scene_num;
        }

        set
        {
            scene_num = value;
        }
    }

    public float Timespend
    {
        get
        {
            return timespend;
        }

        set
        {
            timespend = value;
        }
    }

    public int Total_number
    {
        get
        {
            return total_number;
        }

        set
        {
            total_number = value;
        }
    }

    public int Incorrect_staring
    {
        get
        {
            return incorrect_staring;
        }

        set
        {
            incorrect_staring = value;
        }
    }

    public int Incorrect_voice
    {
        get
        {
            return incorrect_voice;
        }

        set
        {
            incorrect_voice = value;
        }
    }

    public int Remaining_num
    {
        get
        {
            return remaining_num;
        }

        set
        {
            remaining_num = value;
        }
    }
}
