using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataBus : MonoBehaviour {
    private List<string> nameList;
    private List<int> valueList;
    private int pointer;
    private int incorrectView;
    private int length;
    private string lastHitObject = "";
    private float startTime;
    private float timeElapsed;

    // initialize the data
    // @param index: ObjectID based on order
    // @param value: object material value based on order
    public void InitializedData(List<string> index, List<int> value)
    {
        nameList = index;
        valueList = value;
        pointer = 0;
        incorrectView = 0;
        length = index.Count;
        startTime = Time.deltaTime;
    }

    // return 1 if it's corret object, otherwise 0
    // @param objectID:  crosshair hit object ID, use GetInstanceID()
    // @param value: the value texture of the object
    public int checkObject(string objectId)
    {
        string lastCorrentNum = nameList[pointer];
        if (objectId == nameList[pointer]) {
            pointer++;
            return 1;
        } 

        if (objectId == lastCorrentNum)
        {
            return 1;
        }
        else if(objectId == lastHitObject)
        {
            incorrectView++;
        }

        lastHitObject = objectId;
        return 0;
    }

    //public int checkObject(int objectId, int value)
    //{
    //    if ((objectId == IndexList[pointer]) && (value == valueList[pointer]))
    //    {
    //        pointer++;
    //        return 1;
    //    }

    //    incorrectView++;
    //    return 0;
    //}

    // return 1 if continue, 0 means already checked all number

    public float returnTimeSofar()
    {
        return Time.deltaTime - startTime;
    }

    public float returnTotalTime()
    {
        return timeElapsed;
    }

    public int isContinue(){
        if( pointer < length)
        {
            return 1;
        }
        else
        {
            timeElapsed = Time.deltaTime - startTime;
            return 0;
        }
    }

    // return wrong view count
    public int returnWrongViewCount()
    {
        return incorrectView;
    }
}
