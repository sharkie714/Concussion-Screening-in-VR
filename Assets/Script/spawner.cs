using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public Transform eyePosition;
    public GameObject spawnee;
    public Texture[] textures;

    private crossHair _crosshair;
    public GameObject lineGerneratorPrefab;
    public GameObject lineGerneratorPrefab2;

    private static float height = 30.0f;
    private static float width = 70.0f;
    private static float distanceFromEye = 45.0f;

    public int currentScene;
    private double[,] pos;
    private double[,] rot;
    private double[,] dis;

    //private GameDataBus gameDatabus;

    // positions for the number

    private double[,] scene1 = new double[,] {{0, 0.2,  0.5, 0.67,  1 },
                                           {0, 0.26,  0.45, 0.73, 1},
                                           {0, 0.175, 0.45, 0.7, 1},
                                           {0, 0.2,  0.5, 0.76,  1 },
                                           {0, 0.3, 0.72, 0.8, 1 }};

    private double[,] scene1_rot = new double[,] {{50, 37, 0, -25, -50 },
                                           {50, 35, 5, -35, -50},
                                           {50, 40, 5, -32, -50},
                                           {50, 40, 0, -35, -50},
                                           {50, 28, -27, -35, -50}};
    private double[,] scene1_dis = new double[,] {{0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 1.5, 1, 0 }};


    private double[,] scene2 = new double[,] {{0, 0.3,  0.56, 0.73,  1 },
                                           {0, 0.15,  0.4, 0.73, 1},
                                           {0, 0.24, 0.57, 0.79, 1},
                                           {0, 0.3,  0.53, 0.69,  1 },
                                           {0, 0.17, 0.45, 0.73, 1 },
                                           {0, 0.3, 0.52, 0.77, 1 },
                                           {0, 0.15, 0.42, 0.68, 1 },
                                           {0, 0.33, 0.61, 0.75, 1 },};

    private double[,] scene2_rot = new double[,] {{50, 30, -3, -35, -50},
                                           {50, 45, 7, -35, -50},
                                           {50, 35, -3, -38, -50},
                                           {50, 30, 0, -30, -50},
                                           {50,45, 3, -35, -50},
                                           {50, 30, 0, -35, -50},
                                           {50, 45, 5, -30, -50},
                                           {50, 25, -8, -35, -50}};

    private double[,] scene2_dis = new double[,] {{0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0},
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 }};

    private double[,] scene3 = new double[,] {{0, 0.25,  0.45, 0.63,  1 },
                                           {0, 0.23,  0.4, 0.66, 1},
                                           {0, 0.17, 0.45, 0.75, 1},
                                           {0, 0.3,  0.62, 0.76,  1 },
                                           {0, 0.17, 0.45, 0.69, 1 },
                                           {0, 0.32, 0.55, 0.64, 1 },
                                           {0, 0.18, 0.45, 0.69, 1 },
                                           {0, 0.28, 0.55, 0.64, 1 },};

    private double[,] scene3_rot = new double[,] {{50, 35, 5, -25, -50 },
                                           {50, 35, 10, -26, -50},
                                           {50, 43, 5, -35, -50},
                                           {50, 30, -12, -35, -50},
                                           {50, 43, 5, -40, -50},
                                           {50, 28, -5, -43, -50 },
                                           {50, 43, 5, -40, -50 },
                                           {50, 33, -5, -44, -50 }};
    private double[,] scene3_dis = new double[,] {{0, 1, 2, 1.5, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 1.5, 1, 0 },
                                           {0, 1, 2, 1, 0},
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 }};


    private double[,] scene4 = new double[,] {{0, 0.26,  0.43, 0.69,  1 },
                                           {0, 0.18,  0.53, 0.75, 1},
                                           {0, 0.27, 0.63, 0.72, 1},
                                           {0, 0.19,  0.45, 0.75,  1 },
                                           {0, 0.25, 0.53, 0.7, 1 },
                                           {0, 0.32, 0.45, 0.73, 1 },
                                           {0, 0.13, 0.52, 0.78, 1 },
                                           {0, 0.3, 0.53, 0.69, 1 },};

    private double[,] scene4_rot = new double[,] {{50, 35, 7, -40, -50 },
                                           {50, 44, -3, -35, -50},
                                           {50, 35, -13, -32, -50},
                                           {50, 40, 5, -35, -50},
                                           {50, 35, -3, -30, -50},
                                           {50, 28, 5, -32, -50 },
                                           {50, 48, -2, -38, -50 },
                                           {50, 30, -3, -29, -50 }};
    private double[,] scene4_dis = new double[,] {{0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0},
                                           {0, 1, 2, 1, 0 },
                                           {0, 1, 2, 1, 0 },
                                           {0, 1.5, 2, 1, 0 }};


    // Use this for initialization
    void Start()
    {
        select_scene();

        Vector3 offset = new Vector3(-width / 2.0f, height / 2.0f, distanceFromEye);

        GameObject newLineGen = Instantiate(lineGerneratorPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();
        lRend.positionCount = pos.Length;
        int index = 0;
        int index_2 = 0;
        // for data bus
        List<string> nameList = new List<string>();
        List<int> valueList = new List<int>();

        GameObject lastObject = new GameObject();
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            GameObject newLineGen_2 = Instantiate(lineGerneratorPrefab2);
            LineRenderer lRend_2 = newLineGen_2.GetComponent<LineRenderer>();
            index_2 = 0;

            for (int j = 0; j < pos.GetLength(1); j++)
            {
                // new number cube location diff from offset
                Vector3 vec = new Vector3(width * (float)pos[i, j], -height * i / pos.GetLength(0), (float)dis[i, j]);
                // final position after add all adjustment.
                Vector3 newPos = eyePosition.position + vec + offset;

                // generate number object
                GameObject projectile = Instantiate(spawnee, newPos, eyePosition.rotation);
                projectile.transform.rotation = Quaternion.Euler(0, 0, 0);

                // apply texture
                int texture_index = Random.Range(0, 10);
                projectile.GetComponent<Renderer>().material.mainTexture = textures[texture_index];

                // add canvas to be number parent
                string name = index.ToString() + 'a' + texture_index.ToString();
                projectile.name = name;
                //lastObject = projectile;

                // store object id and value to Databus
                nameList.Add(name);

                if (currentScene == 1)
                {
                    lRend.SetPosition(index, newPos);
                    index++;
                }
                if (currentScene == 2)
                {
                    lRend_2.positionCount = pos.GetLength(1);
                    lRend_2.SetPosition(index_2, newPos);
                    index_2++;
                }
            }
        }
        //lastObject.name = "0a0";

        _crosshair = crossHair.FindObjectOfType<crossHair>();
        _crosshair.update_crosshair(nameList, valueList, currentScene);

    }

    void select_scene()
    {
        switch (currentScene)
        {
            case 1:
                pos = scene1;
                //rot = scene1_rot;
                width = 43.5f;
                height = 32.4f;
                dis = scene1_dis;
                break;
            case 3:
                pos = scene2;
                //rot = scene2_rot;
                width = 43.5f;
                height = 30.6f;
                dis = scene2_dis;
                break;
            case 5:
                pos = scene3;
                width = 43.5f;
                height = 30.0f;
                //rot = scene3_rot;
                dis = scene3_dis;
                break;
            case 7:
                pos = scene4;
                width = 43.5f;
                height = 30.0f;
                //rot = scene4_rot;
                dis = scene4_dis;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
