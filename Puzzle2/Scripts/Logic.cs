using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    GameObject[,] walls;
    GameObject[,] waters;
    public GameObject parentboi;
    public GameObject player;
    public GameObject puzzleObj;
    bool[,] blocking;
    bool[,] hasWater;
    bool[,] nextCheck;
    int sourceX, sourceY;
    Queue q;
    int qcount;
    bool solving;
    bool flowed;

    GameObject done;
    bool done1;

    public GameObject flowButton;
    public GameObject redButton;
    ArrayList redBlocks;
    ArrayList redCoordinates;
    ArrayList redToggle;
    public GameObject blueButton;
    ArrayList blueBlocks;
    ArrayList blueCoordinates;
    ArrayList blueToggle;
    public GameObject greenButton;
    ArrayList greenBlocks;
    ArrayList greenCoordinates;
    ArrayList greenToggle;

    ArrayList AnswerCords;
    ArrayList Answers;

    // Start is called before the first frame update
    void Start()
    {
        walls = new GameObject[17, 12];
        waters = new GameObject[17, 12];
        blocking = new bool[17, 12];
        hasWater = new bool[17, 12];
        nextCheck = new bool[17, 12];
        qcount = 0;
        done1 = false;
        solving = false;
        q = new Queue();
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                blocking[i, j] = false;
            }
        }

        for (int i = 0; i < 17; i++)
        {
            addWall(i, 0);
            addWall(i, 11);
        }
        for (int i = 1; i < 12; i++)
        {
            addWall(0, i);
            addWall(16, i);
        }
        flowed = false;

        redBlocks = new ArrayList();
        redCoordinates = new ArrayList();
        redToggle = new ArrayList();
        blueBlocks = new ArrayList();
        blueCoordinates = new ArrayList();
        blueToggle = new ArrayList();
        greenBlocks = new ArrayList();
        greenCoordinates = new ArrayList();
        greenToggle = new ArrayList();

        AnswerCords = new ArrayList();
        Answers = new ArrayList();
        if(Random.Range(1,5) < 4)
        {
            map1(Random.Range(1, 4));
        }
        else
        {
            map1(Random.Range(4, 6));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!flowed && flowButton.GetComponent<ButtonBlock>().blocking)
        {
            solveIt();
        }
        if(flowed && flowButton.GetComponent<ButtonBlock>().blocking)
        {
            unsolve();
        }
        if(!solving && !flowed)
        {
            if(redButton.GetComponent<Button>().blocking)
            {
                redButton.GetComponent<Button>().blocking = false;
                toggleIt(0);
            }
            if(blueButton.GetComponent<Button>().blocking)
            {
                blueButton.GetComponent<Button>().blocking = false;
                toggleIt(1);
            }
            if(greenButton.GetComponent<Button>().blocking)
            {
                greenButton.GetComponent<Button>().blocking = false;
                toggleIt(2);
            }
        }
        else
        {
            redButton.GetComponent<Button>().blocking = false;
            blueButton.GetComponent<Button>().blocking = false;
            greenButton.GetComponent<Button>().blocking = false;
        }
        if(!done1 && flowed && !solving)
        {
            if(checkAnswer())
            {
                finished();

            }
        }

    }
    bool checkAnswer()
    {
        bool result = true;
        for (int i = 0; i < Answers.Count;i++)
        {
            if (!hasWater[(int)AnswerCords[2*i], (int)AnswerCords[2*i + 1]]) result = false;

        }

        return result;

    }
    void finished()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Materials/GreenOn") as Material;
        cube.transform.localScale = new Vector3(20f, 0.75f, 20f);
        cube.transform.position = new Vector3((float)(0), 2.0f, (float)(0));
        cube.transform.parent = parentboi.transform;
        done = cube;
        done1 = true;

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<OtherPlayerMovement>().keyFragments += 1;
        puzzleObj = GameObject.FindGameObjectWithTag("Puzzle2");
        puzzleObj.GetComponent<PuzzleObjectScript>().puzzleSolved = true;
    }

    void addWall(int x, int y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Materials/Wall") as Material;
        cube.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        cube.transform.position = new Vector3((float)(x*3/4.0 - 4), 0.0f, (float)(y*3/4.0 - 4.5));
        cube.transform.parent = parentboi.transform;
        blocking[x, y] = true;
        walls[x, y] = cube;
    }

    void addSource(int x, int y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Materials/WaterSource") as Material;
        cube.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        cube.transform.position = new Vector3((float)(x * 3 / 4.0 - 4), 0.0f, (float)(y * 3 / 4.0 - 4.5));
        cube.transform.parent = parentboi.transform;
        blocking[x, y] = true;
        walls[x, y] = cube;
        sourceX = x;
        sourceY = y;
    }

    void removeWall(int x, int y)
    {
        if (blocking[x, y])
        {
            Destroy(walls[x, y]);
            blocking[x, y] = false;
        }
    }

    void addBlock(int col, int x, int y, bool toggle)
    {

    }

    void addAnswer(int x, int y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Materials/AnswerColor") as Material;
        cube.transform.localScale = new Vector3(0.75f, 0.70f, 0.75f);
        cube.transform.position = new Vector3((float)(x * 3 / 4.0 - 4), 0.001f, (float)(y * 3 / 4.0 - 4.5));
        cube.transform.parent = parentboi.transform;
        blocking[x, y] = false;
        Destroy(walls[x, y]);
        Answers.Add(cube);
        AnswerCords.Add(x);
        AnswerCords.Add(y);
    }

    void addWater(int x, int y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Materials/Water") as Material;
        cube.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        cube.transform.position = new Vector3((float)(x * 3 / 4.0 - 4), 0.001f, (float)(y * 3 / 4.0 - 4.5));
        cube.transform.parent = parentboi.transform;
        waters[x, y] = cube;
        hasWater[x, y] = true;
    }

    void removeWater(int x, int y)
    {
        if(hasWater[x,y])
        {
            Destroy(waters[x, y]);
            hasWater[x, y] = false;
        }
    }

    void toggleIt(int col)//0 = red 1 = blue 2 = green
    {

        switch (col)
        {
            case 0:
                for(int i = 0; i < redBlocks.Count; i++)
                {
                    blocking[(int)redCoordinates[2 * i], (int)redCoordinates[2 * i + 1]] = !(bool)redToggle[i];
                    if(!(bool)redToggle[i])
                    {
                        ((GameObject)redBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/RedOn") as Material;
                    }
                    else
                    {
                        ((GameObject)redBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/RedOff") as Material;
                    }
                    redToggle[i] = !(bool)redToggle[i];
                }
                break;
            case 1:
                for (int i = 0; i < blueBlocks.Count; i++)
                {
                    blocking[(int)blueCoordinates[2 * i], (int)blueCoordinates[2 * i + 1]] = !(bool)blueToggle[i];
                    if (!(bool)blueToggle[i])
                    {
                        ((GameObject)blueBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/BlueOn") as Material;
                    }
                    else
                    {
                        ((GameObject)blueBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/BlueOff") as Material;
                    }
                    blueToggle[i] = !(bool)blueToggle[i];
                }
                break;
            case 2:
                for (int i = 0; i < greenBlocks.Count; i++)
                {
                    blocking[(int)greenCoordinates[2 * i], (int)greenCoordinates[2 * i + 1]] = !(bool)greenToggle[i];
                    if (!(bool)greenToggle[i])
                    {
                        ((GameObject)greenBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/GreenOn") as Material;
                    }
                    else
                    {
                        ((GameObject)greenBlocks[i]).GetComponent<Renderer>().material = Resources.Load("Materials/GreenOff") as Material;
                    }
                    greenToggle[i] = !(bool)greenToggle[i];
                }
                break;
            default:
                break;

        }
    }

    void addToggle(int x, int y, bool tog, int col)//0 = red 1 = blue 2 = green
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        cube.transform.position = new Vector3((float)(x * 3 / 4.0 - 4), 0.001f, (float)(y * 3 / 4.0 - 4.5));
        cube.transform.parent = parentboi.transform;
        switch (col)
        {
            case 0:
                if (tog)
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/RedOn") as Material;
                    blocking[x, y] = true;
                    redBlocks.Add(cube);
                    redCoordinates.Add(x);
                    redCoordinates.Add(y);
                    redToggle.Add(true);
                }
                else
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/RedOff") as Material;
                    blocking[x, y] = false;
                    redBlocks.Add(cube);
                    redCoordinates.Add(x);
                    redCoordinates.Add(y);
                    redToggle.Add(false);
                }
                break;
            case 1:
                if (tog)
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/BlueOn") as Material;
                    blocking[x, y] = true;
                    blueBlocks.Add(cube);
                    blueCoordinates.Add(x);
                    blueCoordinates.Add(y);
                    blueToggle.Add(true);
                }
                else
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/BlueOff") as Material;
                    blocking[x, y] = false;
                    blueBlocks.Add(cube);
                    blueCoordinates.Add(x);
                    blueCoordinates.Add(y);
                    blueToggle.Add(false);
                }
                break;
            case 2:
                if (tog)
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/GreenOn") as Material;
                    blocking[x, y] = true;
                    greenBlocks.Add(cube);
                    greenCoordinates.Add(x);
                    greenCoordinates.Add(y);
                    greenToggle.Add(true);
                }
                else
                {
                    cube.GetComponent<Renderer>().material = Resources.Load("Materials/GreenOff") as Material;
                    blocking[x, y] = false;
                    greenBlocks.Add(cube);
                    greenCoordinates.Add(x);
                    greenCoordinates.Add(y);
                    greenToggle.Add(false);
                }
                break;
            default:
                break;

        }
    }
    void map1(int r)
    {
        switch (r)
        {
            case 0:
                addSource(1, 11);
                for (int i = 1; i < 6; i++)
                {
                    addWall(i, 9);
                }
                for(int i = 10; i > 7; i--)
                {
                    addToggle(6, i, false, 0);
                    addToggle(9, i, false, 1);
                    addToggle(11, i, false, 2);
                    addToggle(6, i-3, true, 0);
                    addToggle(9, i-3, true, 1);
                    addToggle(11, i-3, true, 2);
                }
                addAnswer(0, 1);
                break;
            case 1://Horizontal Widths stuff
                addSource(1, 11);
                addAnswer(3, 0);
                addAnswer(13, 0);
                for(int i = 2; i < 9; i++)
                {
                    addWall(i, 2);
                }
                for(int i = 4; i < 9; i++)
                {
                    addWall(i, 4);
                }
                for (int i = 1; i < 3; i++)
                {
                    addWall(i, 4);
                    addWall(i, 5);
                }
                for(int i = 2; i < 7; i++)
                {
                    addWall(i, 7);
                    addWall(i, 9);
                }
                for(int i = 8; i < 13; i++)
                {
                    addWall(i, 6);
                }
                for(int i = 10; i < 13; i++)
                {
                    addWall(i, 9);
                }
                //vertial stuff
                for(int i = 2; i < 5; i++)
                {
                    addWall(10, i);
                }
                for(int i = 1; i < 5; i++)
                {
                    addWall(12, i);
                }
                for(int i = 2; i < 8; i++)
                {
                    addWall(14, i);
                }
                for(int i = 7; i < 10; i++)
                {
                    addWall(8, i);
                }
                //not worth for loops
                addWall(4, 5);
                addWall(6, 6);
                addWall(12, 7);
                addWall(14, 9);
                int rand1 = Random.Range(0, 21);
                if(rand1 < 7)
                {
                    if(rand1 % 2 == 1)
                    {
                        //red
                        addToggle(4, 1, true, 0);
                        addToggle(3, 4, true, 0);
                        addToggle(9, 9, true, 0);
                        addToggle(6, 5, false, 0);
                        addToggle(11, 4, false, 0);
                        addToggle(13, 9, false, 0);
                        addToggle(2, 3, false, 0);
                        addToggle(13, 2, false, 0);
                    }
                    else
                    {
                        addToggle(4, 1, false, 0);
                        addToggle(3, 4, false, 0);
                        addToggle(9, 9, false, 0);
                        addToggle(6, 5, true, 0);
                        addToggle(11, 4, true, 0);
                        addToggle(13, 9, true, 0);
                        addToggle(2, 3, true, 0);
                        addToggle(13, 2, true, 0);
                    }
                    rand1 /= 2;
                    if(rand1 % 2 == 1)
                    {
                        //blue
                        addToggle(6, 8, true, 1);
                        addToggle(12, 8, true, 1);
                        addToggle(15, 2, true, 1);
                        addToggle(1, 2, false, 1);
                        addToggle(13, 6, false, 1);
                    }
                    else
                    {
                        addToggle(6, 8, false, 1);
                        addToggle(12, 8, false, 1);
                        addToggle(15, 2, false, 1);
                        addToggle(1, 2, true, 1);
                        addToggle(13, 6, true, 1);
                    }
                    rand1 /= 2;
                    if(rand1 % 2 == 1)
                    {
                        //green
                        addToggle(1, 9, true, 2);
                        addToggle(9, 2, true, 2);
                        addToggle(15, 9, true, 2);
                        addToggle(4, 6, false, 2);
                        addToggle(2, 1, false, 2);
                        addToggle(7, 9, false, 2);
                        addToggle(14, 1, false, 2);
                        addToggle(15, 6, false, 2);
                    }
                    else
                    {
                        addToggle(1, 9, false, 2);
                        addToggle(9, 2, false, 2);
                        addToggle(15, 9, false, 2);
                        addToggle(4, 6, true, 2);
                        addToggle(2, 1, true, 2);
                        addToggle(7, 9, true, 2);
                        addToggle(14, 1, true, 2);
                        addToggle(15, 6, true, 2);
                    }
                }
                else if (rand1 < 14)
                {
                    rand1 -= 7;
                    if (rand1 % 2 == 1)
                    {

                        //green
                        addToggle(1, 9, true, 2);
                        addToggle(9, 2, true, 2);
                        addToggle(15, 9, true, 2);
                        addToggle(4, 6, false, 2);
                        addToggle(2, 1, false, 2);
                        addToggle(7, 9, false, 2);
                        addToggle(14, 1, false, 2);
                        addToggle(15, 6, false, 2);
                    }
                    else
                    {
                        addToggle(1, 9, false, 2);
                        addToggle(9, 2, false, 2);
                        addToggle(15, 9, false, 2);
                        addToggle(4, 6, true, 2);
                        addToggle(2, 1, true, 2);
                        addToggle(7, 9, true, 2);
                        addToggle(14, 1, true, 2);
                        addToggle(15, 6, true, 2);
                    }
                    rand1 /= 2;
                    if (rand1 % 2 == 1)
                    {
                        //red
                        addToggle(4, 1, true, 0);
                        addToggle(3, 4, true, 0);
                        addToggle(9, 9, true, 0);
                        addToggle(6, 5, false, 0);
                        addToggle(11, 4, false, 0);
                        addToggle(13, 9, false, 0);
                        addToggle(2, 3, false, 0);
                        addToggle(13, 2, false, 0);
                    }
                    else
                    {
                        addToggle(4, 1, false, 0);
                        addToggle(3, 4, false, 0);
                        addToggle(9, 9, false, 0);
                        addToggle(6, 5, true, 0);
                        addToggle(11, 4, true, 0);
                        addToggle(13, 9, true, 0);
                        addToggle(2, 3, true, 0);
                        addToggle(13, 2, true, 0);
                    }
                    rand1 /= 2;
                    if (rand1 % 2 == 1)
                    {
                        //blue
                        addToggle(6, 8, true, 1);
                        addToggle(12, 8, true, 1);
                        addToggle(15, 2, true, 1);
                        addToggle(1, 2, false, 1);
                        addToggle(13, 6, false, 1);
                    }
                    else
                    {
                        addToggle(6, 8, false, 1);
                        addToggle(12, 8, false, 1);
                        addToggle(15, 2, false, 1);
                        addToggle(1, 2, true, 1);
                        addToggle(13, 6, true, 1);
                    }
                }
                else
                {
                    rand1 -= 14;
                    if (rand1 % 2 == 1)
                    {
                        //blue
                        addToggle(6, 8, true, 1);
                        addToggle(12, 8, true, 1);
                        addToggle(15, 2, true, 1);
                        addToggle(1, 2, false, 1);
                        addToggle(13, 6, false, 1);
                    }
                    else
                    {
                        addToggle(6, 8, false, 1);
                        addToggle(12, 8, false, 1);
                        addToggle(15, 2, false, 1);
                        addToggle(1, 2, true, 1);
                        addToggle(13, 6, true, 1);
                    }
                    rand1 /= 2;
                    if (rand1 % 2 == 1)
                    {

                        //green
                        addToggle(1, 9, true, 2);
                        addToggle(9, 2, true, 2);
                        addToggle(15, 9, true, 2);
                        addToggle(4, 6, false, 2);
                        addToggle(2, 1, false, 2);
                        addToggle(7, 9, false, 2);
                        addToggle(14, 1, false, 2);
                        addToggle(15, 6, false, 2);
                    }
                    else
                    {
                        addToggle(1, 9, false, 2);
                        addToggle(9, 2, false, 2);
                        addToggle(15, 9, false, 2);
                        addToggle(4, 6, true, 2);
                        addToggle(2, 1, true, 2);
                        addToggle(7, 9, true, 2);
                        addToggle(14, 1, true, 2);
                        addToggle(15, 6, true, 2);
                    }
                    rand1 /= 2;
                    if (rand1 % 2 == 1)
                    {
                        //red
                        addToggle(4, 1, true, 0);
                        addToggle(3, 4, true, 0);
                        addToggle(9, 9, true, 0);
                        addToggle(6, 5, false, 0);
                        addToggle(11, 4, false, 0);
                        addToggle(13, 9, false, 0);
                        addToggle(2, 3, false, 0);
                        addToggle(13, 2, false, 0);
                    }
                    else
                    {
                        addToggle(4, 1, false, 0);
                        addToggle(3, 4, false, 0);
                        addToggle(9, 9, false, 0);
                        addToggle(6, 5, true, 0);
                        addToggle(11, 4, true, 0);
                        addToggle(13, 9, true, 0);
                        addToggle(2, 3, true, 0);
                        addToggle(13, 2, true, 0);
                    }
                }

                break;
            case 2:
                //Source and Answer
                addSource(8, 11);
                addAnswer(3, 0);
                addAnswer(5, 0);
                addAnswer(15, 0);
                //Horizontal Walls
                for(int i = 1; i < 4; i++)
                {
                    addWall(i, 9);
                }
                for (int i = 2; i < 5; i++)
                {
                    addWall(i, 5);
                }
                for (int i = 5; i < 12; i++)
                {
                    addWall(i, 9);
                }
                for (int i = 4; i < 8; i++)
                {
                    addWall(i, 2);
                }
                for (int i = 9; i < 15; i++)
                {
                    addWall(i, 4);
                }
                for (int i = 9; i < 12; i++)
                {
                    addWall(i, 6);
                }
                for (int i = 12; i < 15; i++)
                {
                    addWall(i, 7);
                }
                for (int i = 13; i < 15; i++)
                {
                    addWall(i, 2);
                    addWall(i, 9);
                }
                //Vertical
                for (int i = 4; i < 8; i++)
                {
                    addWall(7, i);
                }
                //other
                addWall(2, 8);
                addWall(2, 7);
                addWall(2, 1);
                addWall(2, 3);
                addWall(4, 1);
                addWall(4, 3);
                addWall(4, 6);
                addWall(4, 7);
                addWall(5, 7);
                addWall(6, 5);
                addWall(6, 4);
                addWall(9, 2);
                addWall(11, 2);
                addWall(13, 5);
                addWall(9, 7);
                int rand2 = Random.Range(0, 21);
                bool a2, b2, c2;
                if(rand2 < 7)
                {
                    if (rand2 % 2 == 1) a2 = true;
                    else a2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) b2 = true;
                    else b2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) c2 = true;
                    else c2 = false;
                }
                else if(rand2 < 14)
                {
                    rand2 -= 7;
                    if (rand2 % 2 == 1) b2 = true;
                    else b2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) c2 = true;
                    else c2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) a2 = true;
                    else a2 = false;
                }
                else
                {
                    rand2 -= 14;
                    if (rand2 % 2 == 1) c2 = true;
                    else c2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) a2 = true;
                    else a2 = false;
                    rand2 /= 2;
                    if (rand2 % 2 == 1) b2 = true;
                    else b2 = false;
                }
                //red
                addToggle(3, 3, a2, 0);
                addToggle(5, 5, a2, 0);
                addToggle(11, 8, a2, 0);
                addToggle(11, 3, a2, 0);
                addToggle(4, 8, !a2, 0);
                addToggle(7, 3, !a2, 0);
                addToggle(15, 6, !a2, 0);
                addToggle(13, 1, !a2, 0);
                //blue
                addToggle(7, 10, b2, 1);
                addToggle(10, 2, b2, 1);
                addToggle(15, 2, b2, 1);
                addToggle(1, 3, !b2, 1);
                addToggle(8, 2, !b2, 1);
                addToggle(13, 6, !b2, 1);
                //green
                addToggle(15, 9, c2, 2);
                addToggle(6, 7, c2, 2);
                addToggle(9, 3, c2, 2);
                addToggle(2, 6, !c2, 2);
                addToggle(2, 2, !c2, 2);
                addToggle(12, 9, !c2, 2);
                addToggle(13, 3, !c2, 2);
                break;

            case 3:
                //Source and Answer
                addSource(14, 11);
                addAnswer(4, 0);
                addAnswer(9, 0);
                addAnswer(15, 0);
                //Horizontal
                for(int i = 2; i < 5; i++)
                {
                    addWall(i, 9);
                }
                for (int i = 6; i < 10; i++)
                {
                    addWall(i, 9);
                }
                for (int i = 11; i < 15; i++)
                {
                    addWall(i, 9);
                }
                for(int i = 2; i < 6; i++)
                {
                    addWall(i, 6);
                }
                for (int i = 9; i < 13; i++)
                {
                    addWall(i, 7);
                }
                for (int i = 1; i < 4; i++)
                {
                    addWall(i, 4);
                }
                for (int i = 4; i < 7; i++)
                {
                    addWall(i, 2);
                }
                for (int i = 10; i < 15; i++)
                {
                    addWall(i, 2);
                }
                //vertical
                for (int i = 6; i < 9; i++)
                {
                    addWall(14, i);
                }
                for (int i = 2; i < 6; i++)
                {
                    addWall(8, i);
                }
                //ind
                addWall(4, 8);
                addWall(2, 7);
                addWall(5, 4);
                addWall(2, 2);
                addWall(7, 4);
                addWall(7, 5);
                addWall(6, 8);
                addWall(7, 8);
                addWall(7, 7);
                addWall(10, 4);
                addWall(10, 5);
                addWall(12, 4);
                addWall(12, 5);
                addWall(13, 4);
                addWall(14, 4);

                int rand3 = Random.Range(0, 21);
                bool a3, b3, c3;
                if (rand3 < 7)
                {
                    if (rand3 % 2 == 1) a3 = true;
                    else a3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) b3 = true;
                    else b3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) c3 = true;
                    else c3 = false;
                }
                else if (rand3 < 14)
                {
                    rand3 -= 7;
                    if (rand3 % 2 == 1) b3 = true;
                    else b3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) c3 = true;
                    else c3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) a3 = true;
                    else a3 = false;
                }
                else
                {
                    rand3 -= 14;
                    if (rand3 % 2 == 1) c3 = true;
                    else c3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) a3 = true;
                    else a3 = false;
                    rand3 /= 2;
                    if (rand3 % 2 == 1) b3 = true;
                    else b3 = false;
                }
                //red
                addToggle(15, 9, a3, 0);
                addToggle(14, 3, a3, 0);
                addToggle(7, 2, a3, 0);
                addToggle(5, 5, a3, 0);
                addToggle(1, 2, !a3, 0);
                addToggle(2, 8, !a3, 0);
                addToggle(8, 1, !a3, 0);
                //blue
                addToggle(5, 9, b3, 1);
                addToggle(3, 2, b3, 1);
                addToggle(11, 5, b3, 1);
                addToggle(12, 3, b3, 1);
                addToggle(10, 9, !b3, 1);
                addToggle(6, 7, !b3, 1);
                addToggle(14, 5, !b3, 1);
                addToggle(10, 1, !b3, 1);
                //green
                addToggle(1, 9, c3, 2);
                addToggle(5, 1, c3, 2);
                addToggle(9, 2, c3, 2);
                addToggle(10, 3, c3, 2);
                addToggle(3, 5, !c3, 2);
                addToggle(5, 3, !c3, 2);
                addToggle(8, 7, !c3, 2);
                addToggle(14, 1, !c3, 2);
                addToggle(7, 6, !c3, 2);
                addToggle(9, 5, !c3, 2);
                break;
            case 4:
                //Source and Answers
                addSource(8, 11);
                addAnswer(1, 0);
                addAnswer(6, 0);
                addAnswer(15, 0);
                //Walls
                for(int i = 2; i < 7; i++)
                {
                    addWall(i, 9);
                }
                for(int i = 2; i < 6; i++)
                {
                    addWall(i, 1);
                    addWall(i, 3);
                }
                for(int i = 5; i < 8; i++)
                {
                    addWall(i, 2);
                }
                for(int i = 5; i < 9; i++)
                {
                    addWall(i, 4);
                }
                for(int i = 5; i < 12; i++)
                {
                    addWall(i, 6);
                }
                for(int i = 9; i < 15; i++)
                {
                    addWall(i, 1);
                    addWall(i, 2);
                }
                for(int i = 8; i < 13; i++)
                {
                    addWall(i, 9);
                }
                for(int i = 11; i < 15; i++)
                {
                    addWall(i, 8);
                }
                for(int i = 7; i < 10; i++)
                {
                    addWall(i, 5);
                }
                //Vertical
                for(int i = 5; i < 8; i++)
                {
                    addWall(2, i);
                    addWall(3, i);
                }
                //Other
                addWall(2, 2);
                addWall(3, 2);
                addWall(5, 7);
                addWall(5, 8);
                addWall(6, 8);
                addWall(8, 8);
                addWall(9, 8);
                addWall(14, 9);
                addWall(14, 7);
                addWall(14, 6);
                addWall(13, 6);
                addWall(14, 4);
                addWall(13, 4);
                addWall(11, 4);
                addWall(10, 4);
                //toggles
                int rand4 = Random.Range(0, 21);
                bool a4, b4, c4;
                if (rand4 < 7)
                {
                    if (rand4 % 2 == 1) a4 = true;
                    else a4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) b4 = true;
                    else b4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) c4 = true;
                    else c4 = false;
                }
                else if (rand4 < 14)
                {
                    rand4 -= 7;
                    if (rand4 % 2 == 1) b4 = true;
                    else b4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) c4 = true;
                    else c4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) a4 = true;
                    else a4 = false;
                }
                else
                {
                    rand4 -= 14;
                    if (rand4 % 2 == 1) c4 = true;
                    else c4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) a4 = true;
                    else a4 = false;
                    rand4 /= 2;
                    if (rand4 % 2 == 1) b4 = true;
                    else b4 = false;
                }
                //red
                addToggle(1, 5, a4, 0);
                addToggle(13, 9, a4, 0);
                addToggle(15, 4, !a4, 0);
                //blue
                addToggle(3, 8, !b4, 1);
                addToggle(8, 9, !b4, 1);
                addToggle(13, 10, !b4, 1);
                addToggle(1, 6, b4, 1);
                addToggle(12, 4, b4, 1);
                //green
                addToggle(2, 10, !c4, 2);
                addToggle(4, 6, !c4, 2);
                addToggle(9, 3, !c4, 2);
                addToggle(12, 6, !c4, 2);
                addToggle(1, 7, c4, 2);
                break;
            case 5:
                addSource(2, 11);
                addAnswer(1, 0);
                addAnswer(10, 0);
                addAnswer(16, 1);
                //Horizontal
                for(int i = 2; i < 6; i++)
                {
                    addWall(i, 9);
                    addWall(i, 2);
                    addWall(i, 3);
                }
                for (int i = 1; i < 4; i++)
                {
                    addWall(i, 7);
                }
                for (int i = 7; i < 15; i++)
                {
                    addWall(i, 9);
                }
                for (int i = 5; i < 10; i++)
                {
                    addWall(i, 5);
                    addWall(i, 7);
                }
                for (int i = 7; i < 10; i++)
                {
                    addWall(i, 2);
                    addWall(i, 3);
                }
                for (int i = 11; i < 15; i++)
                {
                    addWall(i, 5);
                    addWall(i, 7);
                    addWall(i, 2);
                }
                for(int i = 11; i < 14; i++)
                {
                    addWall(i, 1);
                }
                //Else
                addWall(2, 5);
                addWall(3, 5);
                addWall(5, 6);
                addWall(5, 4);
                addWall(14, 8);
                addWall(14, 6);
                addWall(14, 3);
                addWall(12, 3);
                addWall(11, 3);
                addWall(2, 1);
                addWall(3, 1);
                removeWall(13, 2);
                removeWall(13, 1);

                int rand5 = Random.Range(0, 21);
                bool a5, b5, c5;
                if (rand5 < 7)
                {
                    if (rand5 % 2 == 1) a5 = true;
                    else a5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) b5 = true;
                    else b5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) c5 = true;
                    else c5 = false;
                }
                else if (rand5 < 14)
                {
                    rand5 -= 7;
                    if (rand5 % 2 == 1) b5 = true;
                    else b5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) c5 = true;
                    else c5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) a5 = true;
                    else a5 = false;
                }
                else
                {
                    rand5 -= 14;
                    if (rand5 % 2 == 1) c5 = true;
                    else c5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) a5 = true;
                    else a5 = false;
                    rand5 /= 2;
                    if (rand5 % 2 == 1) b5 = true;
                    else b5 = false;
                }
                //red
                addToggle(1, 5, !a5, 0);
                addToggle(3, 6, !a5, 0);
                addToggle(4, 5, a5, 0);
                addToggle(15, 9, a5, 0);
                //blue
                addToggle(4, 7, !b5, 1);
                addToggle(15, 3, !b5, 1);
                addToggle(6, 3, b5, 1);
                addToggle(13, 3, b5, 1);
                //green
                addToggle(1, 9, !c5, 2);
                addToggle(1, 3, !c5, 2);
                addToggle(6, 8, !c5, 2);
                addToggle(10, 7, !c5, 2);
                addToggle(10, 3, !c5, 2);
                addToggle(15, 5, c5, 2);
                break;
            default:
                break;
        }
    }


    void solveIt()
    {
        if(solving == false)
        {
            flow(sourceX, sourceY - 1);
            solving = true;
        }
        else
        {
            int temp = qcount;
            int curx, cury;
            qcount = 0;
            for (int i = 0; i < temp; i++)
            {
                curx = (int)q.Dequeue();
                cury = (int)q.Dequeue();
                flow(curx, cury);
            }
            if (qcount == 0)
            {
                flowButton.GetComponent<ButtonBlock>().blocking = false;
                flowed = true;
                solving = false;
            }

        }
        System.Threading.Thread.Sleep(100);



    }
    void flow(int x,int y)
    {
        addWater(x, y);
        nextCheck[x, y] = false;
        
        if(x > 0 && !blocking[x-1,y] && !hasWater[x-1,y] && !nextCheck[x-1,y])
        {
            qcount++;
            q.Enqueue(x - 1);
            q.Enqueue(y);
            nextCheck[x - 1, y] = true;
        }
        if (x < 16 && !blocking[x + 1, y] && !hasWater[x + 1, y] && !nextCheck[x + 1, y])
        {
            qcount++;
            q.Enqueue(x + 1);
            q.Enqueue(y);
            nextCheck[x + 1, y] = true;
        }
        if (y > 0 && !blocking[x, y - 1] && !hasWater[x, y - 1] && !nextCheck[x, y - 1])
        {
            qcount++;
            q.Enqueue(x);
            q.Enqueue(y - 1);
            nextCheck[x, y - 1] = true;
        }
        if (y < 11 && !blocking[x, y + 1] && !hasWater[x, y + 1] && !nextCheck[x, y + 1])
        {
            qcount++;
            q.Enqueue(x);
            q.Enqueue(y + 1);
            nextCheck[x, y + 1] = true;
        }
    }

    void unsolve()
    {
        if (solving == false)
        {
            unflow(sourceX, sourceY - 1);
            solving = true;
        }
        else
        {
            int temp = qcount;
            int curx, cury;
            qcount = 0;
            for (int i = 0; i < temp; i++)
            {
                curx = (int)q.Dequeue();
                cury = (int)q.Dequeue();
                unflow(curx, cury);
            }
            if (qcount == 0)
            {
                flowButton.GetComponent<ButtonBlock>().blocking = false;
                flowed = false;
                solving = false;
            }

        }
        System.Threading.Thread.Sleep(100);
    }

    void unflow(int x, int y)
    {
        removeWater(x, y);
        nextCheck[x, y] = false;

        if (x > 0 && hasWater[x - 1, y] && !nextCheck[x - 1, y])
        {
            qcount++;
            q.Enqueue(x - 1);
            q.Enqueue(y);
            nextCheck[x - 1, y] = true;
        }
        if (x < 16 && hasWater[x + 1, y] && !nextCheck[x + 1, y])
        {
            qcount++;
            q.Enqueue(x + 1);
            q.Enqueue(y);
            nextCheck[x + 1, y] = true;
        }
        if (y > 0 && hasWater[x, y - 1] && !nextCheck[x, y - 1])
        {
            qcount++;
            q.Enqueue(x);
            q.Enqueue(y - 1);
            nextCheck[x, y - 1] = true;
        }
        if (y < 11 && hasWater[x, y + 1] && !nextCheck[x, y + 1])
        {
            qcount++;
            q.Enqueue(x);
            q.Enqueue(y + 1);
            nextCheck[x, y + 1] = true;
        }
    }
}
