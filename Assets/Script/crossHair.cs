using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Collections.Generic;


// for seshu:
// update pointer( current position on the nameList )
// remaining_num ( how many number left )
// incorrect_view
// lastCorrectNumber, targetNumber
// startTime, timeElapsed

// for Sudeep
// need to compared user voice input with targetNumber
// update incorrect_voice

public class crossHair : MonoBehaviour {
    //declaration for IBM watson speech to text call
    private string _recognizeModel;
    private string _iamApikey = "5pGnvvOiFBxACIsuaI5rJX0dRmftAsP0QDbe0yuiW8ab";

    private string _serviceUrl = "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize?smart_formatting=true";
    private int _recordingRoutine = 0;
    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingBufferSize = 1;
    private int _recordingHZ = 22050;
    private SpeechToText _service;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    //IDictionary<string, int> dict = new Dictionary<string, int>();
    public Camera CameraFacing;
    //public Text m_MyText1;
    [SerializeField] private LayerMask m_ExclusionLayers;
    //private GameDataBus gameDatabus;
    [SerializeField] private float m_RayLength = 30000f;


    // cube information from scene
    private List<string> item_nameList;
    private List<int> item_valueList;

    public float waittime = 1.0f;
    private int pointer=0;
    private int totalNumber;
    private int remaining_num;
    private int incorrect_view;
    private int incorrect_voice;
    private float startTime;
    private float timeElapsed;
    private float timer;
    private int currentSceneNum;
    string lastCorrentNum;
    private string lastHitObject = "";
    private string currentHitObject = "";
    int counter11 = 1;
    int counter = 0;
    int counters = 0;
    bool isTargetObject = false;
    private int lastCorrectNumber_index;
    private int targetNumber_value;
    private int testSkipped = 0;


    public void update_crosshair(List<string> _nameList, List<int> _valueList, int currentScene_num)
    {
        item_nameList = _nameList;
        item_valueList = _valueList;
        pointer = 0;
        currentSceneNum = currentScene_num;
        totalNumber = _nameList.Count;
        remaining_num = totalNumber;
        incorrect_view = 0;
        incorrect_voice = 0;
        startTime = 0.0f;
        timeElapsed = 0.0f;
        lastCorrentNum = item_nameList[0];
    }

    public int checkObject(string objectId, int counter1)
    {
        if (objectId == item_nameList[pointer])
        {
            pointer++;
            remaining_num--;
            lastCorrentNum = item_nameList[pointer - 1];
            return 1;
        }
        else if (objectId == lastCorrentNum)
        {
            if (counter1 == 1)
                return 1;
            else
                return 2;
        }
        else if (objectId == lastHitObject)
        {
            return 0;
        }

        lastHitObject = objectId;
        return 0;
    }


    public void setCurrenceSceneNum(int _currentScene_num)
    {
        currentSceneNum = _currentScene_num;
    }

    public void emptyData()
    {
        currentSceneNum = 0;
        totalNumber = 0;
        remaining_num = 100;
        incorrect_view = 0;
        incorrect_voice = 0;
        startTime = 0.0f;
        timeElapsed = 0.0f;
    }

    void UploadSceneResult()
    {
        DataStore dataControl = GameObject.FindObjectOfType<DataStore>();
        Report_template scene_result = new Report_template(currentSceneNum, timeElapsed, totalNumber, remaining_num, incorrect_view, incorrect_voice, testSkipped);
        dataControl.pushResult(scene_result);
    }


    int is_finished()
    {
        if (remaining_num == 0)
        {
            return 1;
        }

        return 0;
    }

    private readonly string[] scene_sequence = { "Menu", "Scene1", "T1", "Scene2", "T2", "Scene3", "T3", "Scene4", "T4", "Summary" };

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraFacing.transform.position +
                           CameraFacing.transform.rotation * Vector3.forward * 60f;
        transform.LookAt(CameraFacing.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);

        RaycastHit hit;

        if (Physics.Raycast(new Ray(CameraFacing.transform.position,
                                CameraFacing.transform.rotation * Vector3.forward),
                            out hit, m_RayLength, m_ExclusionLayers))
        {

            // transition to next scene
            string object_tag = hit.collider.gameObject.tag;
          
            if (object_tag != "Untagged" && object_tag != "Player")
            {
                if (object_tag == "Start Test")
                {
                    SceneManager.LoadScene("Scene2");
                }
                else if (object_tag == "Tutorial")
                {
                    SceneManager.LoadScene("Scene1");
                }
                else if ((object_tag == "Next" && remaining_num==0)||(object_tag=="Next" && currentSceneNum%2!=1))
                {
                    //  only test scene has odd scene number
                    if (currentSceneNum % 2 == 1)
                    {
                        timeElapsed = Mathf.Floor(timeElapsed);
                        UploadSceneResult();
                    }

                    int next_Scene_position = currentSceneNum + 1;
                    string Scene_to_load = scene_sequence[next_Scene_position];
                    string Scene_to_unload = scene_sequence[currentSceneNum];
                    SceneManager.LoadScene(Scene_to_load);
                    SceneManager.UnloadSceneAsync(Scene_to_unload);
                }
                else if (object_tag == "Restart")
                {
                    SceneManager.LoadScene("Menu");
                }

            }

            //if (is_finished() == 0)
            //{
            //    testSkipped = 1;
            //    UploadSceneResult();

            //    int next_Scene_position = currentSceneNum + 1;
            //    string Scene_to_load = scene_sequence[next_Scene_position];
            //    string Scene_to_unload = scene_sequence[currentSceneNum];
            //    SceneManager.LoadScene(Scene_to_load);
            //    SceneManager.UnloadSceneAsync(Scene_to_unload);
            //}

            if (hit.collider.gameObject.tag == "Player")
            {
                isTargetObject = true;
                timer = timer + Time.deltaTime;
                counter = 0;
                string objValue = transform.GetComponent<Renderer>().material.name;
                {
                    string objName = hit.collider.gameObject.name;
                    Debug.Log(objName);
                    currentHitObject = objName;

                    int result = checkObject(objName, counter11);

                    if (result == 1 && timer >= waittime)
                    {
                        counters = 1;
                        timeElapsed += Time.deltaTime;
                        transform.GetComponent<Renderer>().material.color = Color.green;
                        counter11 = 1;
                    }
                    else if (result == 2 && timer >= waittime)
                    {
                        
                        counters = 1;
                        timeElapsed += Time.deltaTime;
                        transform.GetComponent<Renderer>().material.color = Color.green;
                        counter11 = 0;
                    }
                    else if(timer >= waittime)
                    {
                        if (counters == 1)
                        {
                            timeElapsed += Time.deltaTime;
                            transform.GetComponent<Renderer>().material.color = Color.red;
                            counter = 1;
                            counter11 = 0;
                        }
                    }
                }
            }
            else
            {
                if(counters==1)
                    timeElapsed += Time.deltaTime;
                incorrect_view = incorrect_view + counter;
                counter = 0;
                timer = 0;
                counter11 = 0;
                transform.GetComponent<Renderer>().material.color = Color.yellow;
                isTargetObject = false;
                Debug.Log("hit not number target");
            }

        }
    }


    //Service calls for speechToText

    private IEnumerator CreateService()
    {
        //  Create credential and instantiate service
        Credentials credentials = null;
        if (!string.IsNullOrEmpty(_iamApikey))
        {
            //  Authenticate using iamApikey
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = _iamApikey,
            };

            credentials = new Credentials(tokenOptions, _serviceUrl);

            //  Wait for tokendata
            while (!credentials.HasIamTokenData())
                yield return null;
        }
        else
        {
            throw new WatsonException("Please provide either username and password or IAM apikey to authenticate the service.");
        }

        _service = new SpeechToText(credentials);
        _service.StreamMultipart = true;

        Active = true;
        StartRecording();
    }

    public bool Active
    {
        get { return _service.IsListening; }
        set
        {
            if (value && !_service.IsListening)
            {
                _service.RecognizeModel = (string.IsNullOrEmpty(_recognizeModel) ? "en-US_BroadbandModel" : _recognizeModel);
                _service.DetectSilence = true;
                _service.EnableWordConfidence = true;
                _service.EnableTimestamps = true;
                _service.SilenceThreshold = 0.01f;
                _service.MaxAlternatives = 0;
                _service.EnableInterimResults = true;
                _service.OnError = OnError;
                _service.InactivityTimeout = -1;
                _service.ProfanityFilter = false;
                _service.SmartFormatting = true;
                _service.SpeakerLabels = false;
                _service.WordAlternativesThreshold = null;
                _service.StartListening(OnRecognize);
            }
            else if (!value && _service.IsListening)
            {
                _service.StopListening();
            }
        }
    }

    private void StartRecording()
    {
        if (_recordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            _recordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    private void StopRecording()
    {
        if (_recordingRoutine != 0)
        {
            Microphone.End(_microphoneID);
            Runnable.Stop(_recordingRoutine);
            _recordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("ExampleStreaming.OnError()", "Error! {0}", error);
    }

    private IEnumerator RecordingHandler()
    {
        Log.Debug("ExampleStreaming.RecordingHandler()", "devices: {0}", Microphone.devices);
        _recording = Microphone.Start(_microphoneID, true, _recordingBufferSize, _recordingHZ);
        yield return null;      // let _recordingRoutine get set..

        if (_recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = _recording.samples / 2;
        float[] samples = null;

        while (_recordingRoutine != 0 && _recording != null)
        {
            int writePos = Microphone.GetPosition(_microphoneID);
            if (writePos > _recording.samples || !Microphone.IsRecording(_microphoneID))
            {
                Log.Error("ExampleStreaming.RecordingHandler()", "Microphone disconnected.");

                StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
              || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                _recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                record.Clip.SetData(samples, 0);

                _service.OnListen(record);

                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio, 
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (_recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)_recordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }

        }

        yield break;
    }

    private void OnRecognize(SpeechRecognitionEvent result, Dictionary<string, object> customData)
    {
        string[] numValue = currentHitObject.Split('a');
        string[] voiceNum;
        if (result != null && result.results.Length > 0 && isTargetObject)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    if (res.final)
                    {
                        Debug.Log(alt.transcript);
                        voiceNum = alt.transcript.Split(' ');
                        if (voiceNum.Length < 2)
                        {
                            Debug.Log("unable to detect number");
                        }
                        else
                        {
                            if (voiceNum[1] != numValue[1]) { incorrect_voice++; }
                            Debug.Log(numValue[1] + "--" + voiceNum[1]);
                        }

                    }
                    //Debug.Log(item_nameList[pointer] + " <---------> " + alt.transcript + alt.confidence);
                }
            }
        }
    }

}
