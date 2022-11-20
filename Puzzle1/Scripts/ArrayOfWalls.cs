using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayOfWalls : MonoBehaviour {

    public GameObject[,] walls;
    GameObject player;
    public GameObject parentboi;
    private Player_Script a;
    public bool[,] blocking;
    public int finalx, finaly;
    public bool restart;
    bool diff;




    void Start () {
        walls = new GameObject[16, 10];
        blocking = new bool[16, 10];
        finalx = 0;
        finaly = 0;
        player = GameObject.Find("Player");
        a = player.GetComponent<Player_Script>();
        for (int i = 0; i < 16; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                blocking[i,j] = false;
            }
        }
        
        for(int i = 0; i < 16; i++)
        {
            addWall(i, 0);
            addWall(i, 9);
        }
        for(int i = 1; i < 10; i++)
        {
            addWall(0, i);
            addWall(15, i);
        }
        diff = true;
        randomPuzzle2();

    }
	
	// Update is called once per frame
	void Update () {
		if(restart)
        {
            restarts();
            restart = false;
            print(Random.Range(0, 100));
            randomPuzzle2();
        }
	}

    void addWall(int x, int y)
    {
        x = x;
        y = y;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = Resources.Load("Material/WallColor") as Material;
        cube.transform.position = new Vector3(((float)(x-8)), 0.0f, ((float)(y-4.5)));
        cube.transform.parent = parentboi.transform;
        //cube.transform.rotation = Quaternion.Euler(0, 0, 0);
        blocking[x, y] = true;
        walls[x, y] = cube;
    }

    void removeWall(int x, int y)
    {
        if(blocking[x,y])
        {
            Destroy(walls[x, y]);
            blocking[x, y] = false;
        }
        
    }


    //Testing Map

    

    void puz1(int a)
    {

        if(a == 1)
        {
            addWall(1, 2);
            addWall(5, 3);
            addWall(4, 7);
            addWall(4, 8);
            finalx = 4;
            finaly = 6;
        }
    }

    void restarts()
    {
        for(int i = 1; i < 15; i++)
        {
            for(int j = 1; j < 9; j++)
            {
                removeWall(i, j);
            }
        }
    }

    void randomPuzzle()
    {
        bool[,] occ = new bool[16,10];
        for(int i = 0; i < 16; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                occ[i, j] = false;
            }
        }
        int r; //To be randomized a lot
        int dir, newdir;//0 = up, 1 = right, 2 = down, 3 = left
        occ[1, 1] = true;
        int counter = 0;
        bool works;
        finalx = 1;
        finaly = 1;
        dir = Random.Range(0, 2);
        
        while(counter < 5 || (counter < 8 && Random.Range(0, 3) == 0))
        {
            works = false;
            switch (dir)
            {
                case 0:
                    //checks for available spot. If there is none, breaks the whole thing;
                    print("dir = " + dir);

                    for (int i = finaly; i < 8; i++)
                    {
                        if(!occ[finalx,i])
                        {
                            works = true;
                        }
                    }
                    if (!works || finaly == 8)
                    {
                        newdir = 2;
                        break;
                    }
                    r = Random.Range(finaly, 8);
                    while(occ[finalx,r+1] || occ[finalx,r])
                    {
                        print("r =  " + r);
                        r = Random.Range(finaly, 8);
                    }
                    print("r =  " + r);
                    addWall(finalx, r + 1);
                    occ[finalx, r + 1] = true;
                    for(int i = finaly; i <= r; i++)
                    {
                        occ[finalx, i] = true;
                    }

                    finaly = r;
                    
                    if (Random.Range(0, 2) == 1) newdir = 1;
                    else newdir = 3;
                    break;
                case 1:

                    print("dir = " + dir);

                    for (int i = finalx; i < 14; i++)
                    {
                        if (!occ[i, finaly])
                        {
                            works = true;
                        }
                    }
                    if (!works || finalx == 14)
                    {
                        newdir = 3;
                        break;
                    }
                    r = Random.Range(finalx, 14);
                    while (occ[r + 1, finaly] || occ[r, finaly])
                    {
                        print("r =  " + r);
                        r = Random.Range(finalx, 14);
                    }
                    print("r =  " + r);
                    addWall(r+1, finaly);
                    occ[r + 1, finaly] = true;
                    for (int i = finalx; i <= r; i++)
                    {
                        occ[i, finaly] = true;
                    }
                    finalx = r;

                    if (Random.Range(0, 2) == 1) newdir = 0;
                    else newdir = 2;
                    break;
                case 2:

                    print("dir = " + dir);

                    for (int i = 1; i < finaly; i++)
                    {
                        if (!occ[finalx, i])
                        {
                            works = true;
                        }
                    }
                    if (!works || finaly == 1)
                    {
                        newdir = 0;
                        break;
                    }
                    r = Random.Range(1, finaly);
                    while (occ[finalx, r - 1] || occ[finalx, r])
                    {
                        print("r =  " + r);
                        r = Random.Range(1, finaly);
                    }
                    print("r =  " + r);
                    addWall(finalx, r - 1);
                    occ[finalx, r - 1] = true;
                    for (int i = finaly; i >= r; i--)
                    {
                        occ[finalx, i] = true;
                    }
                    finaly = r;
                    if (Random.Range(0, 2) == 1) newdir = 1;
                    else newdir = 3;
                    break;
                case 3:

                    print("dir = " + dir);

                    for (int i = 1; i < finalx; i++)
                    {
                        if (!occ[i, finaly])
                        {
                            works = true;
                        }
                    }
                    if (!works || finalx == 1)
                    {
                        newdir = 1;
                        break;
                    }
                    r = Random.Range(1, finalx);
                    while (occ[r - 1, finaly] || occ[r, finaly])
                    {
                        print("r =  " + r);
                        r = Random.Range(1, finalx);
                    }
                    print("r =  " + r);
                    addWall(r - 1, finaly);
                    occ[r - 1, finaly] = true;
                    for (int i = finalx; i >= r; i--)
                    {
                        occ[i, finaly] = true;
                    }

                    finalx = r;

                    if (Random.Range(0, 2) == 1) newdir = 0;
                    else newdir = 2;
                    break;
                default:
                    newdir = 0;
                    break;
                    
            }
            dir = newdir;
            counter++;
        }
    }

    void randomPuzzle2()
    {
        int curx, cury;
        bool[,] occ = new bool[16, 10];
        for (int i = 1; i < 15; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                occ[i, j] = false;
            }
        }
        for(int i = 0; i < 16; i++)
        {
            occ[i, 0] = true;
            occ[i, 9] = true;
        }
        for (int i = 0; i < 10; i++)
        {
            occ[0, i] = true;
            occ[15, i] = true;
        }
        ArrayList arr = new ArrayList();
        
        int counter = 0;
        //selects middle square to start from, and direction
        int dir;//0 = up, 1 = right, 2 = down, 3 = left
        curx = Random.Range(2,14);
        cury = Random.Range(2,8);
        int temp;
        if (Random.Range(0, 2) == 0)//right first
        {
            addWall(curx + 1, 1);
            addWall(curx, cury + 1);
            for (int i = 0; i <= curx; i++)
            {
                occ[i, 1] = true;
            }
            for (int i = 0; i <= cury; i++)
            {
                occ[curx, i] = true;
            }
            if (Random.Range(0, 2) == 0) dir = 1;
            else dir = 3;

        }
        else
        {
            addWall(1, cury + 1);
            addWall(curx + 1, cury);
            for (int i = 0; i <= cury; i++)
            {
                occ[1, i] = true;
            }
            for (int i = 0; i <= curx; i++)
            {
                occ[i, cury] = true;
            }
            if (Random.Range(0, 2) == 0) dir = 0;
            else dir = 2;
        }
        

        while(counter < 5 || (counter < 7 && Random.Range(0,3) == 0))
        {
            switch(dir)
            {
                case 0:
                    for(int i = cury + 1; i < 8; i++)//adds potential new Y to the arraylist
                    {
                          if (blocking[curx, i]) break;
                          if(!occ[curx,i] && !occ[curx,i+1])
                          {
                            arr.Add(i);
                          }
                    }
                    if(arr.Count == 0)
                    {
                        dir = 2;
                        break;
                    }
                    temp = (int)arr[Random.Range(0, arr.Count)];
                    for(int i = cury; i <= temp; i++)
                    {
                        occ[curx, i] = true;
                    }
                    addWall(curx, temp + 1);
                    cury = temp;
                    if (Random.Range(0, 2) == 0) dir = 1;
                    else dir = 3;
                    break;
                case 1:
                    for (int i = curx + 1; i < 14; i++)//adds potential new X to the arraylist
                    {
                        if (blocking[i, cury]) break;
                        if (!occ[i, cury] && !occ[i + 1, cury])
                        {
                            arr.Add(i);
                        }
                    }
                    if (arr.Count == 0)
                    {
                        dir = 3;
                        break;
                    }
                    temp = (int)arr[Random.Range(0, arr.Count)];
                    for (int i = curx; i <= temp; i++)
                    {
                        occ[i, cury] = true;
                    }
                    addWall(temp + 1, cury);
                    curx = temp;
                    if (Random.Range(0, 2) == 0) dir = 0;
                    else dir = 2;
                    break;
                case 2:
                    for (int i = cury - 1; i > 1; i--)//adds potential new Y to the arraylist
                    {
                        if (blocking[curx, i]) break;
                        if (!occ[curx, i] && !occ[curx, i - 1])
                        {
                            arr.Add(i);
                        }
                    }
                    if (arr.Count == 0)
                    {
                        dir = 0;
                        break;
                    }
                    temp = (int)arr[Random.Range(0, arr.Count)];
                    for (int i = cury; i >= temp; i--)
                    {
                        occ[curx, i] = true;
                    }
                    addWall(curx, temp - 1);
                    cury = temp;
                    if (Random.Range(0, 2) == 0) dir = 1;
                    else dir = 3;
                    break;
                case 3:
                    for (int i = curx -1; i > 1; i--)//adds potential new X to the arraylist
                    {
                        if (blocking[i, cury]) break;
                        if (!occ[i, cury] && !occ[i - 1, cury])
                        {
                            arr.Add(i);
                        }
                    }
                    if (arr.Count == 0)
                    {
                        dir = 1;
                        break;
                    }
                    temp = (int)arr[Random.Range(0, arr.Count)];
                    for (int i = curx; i >= temp; i--)
                    {
                        occ[i, cury] = true;
                    }
                    addWall(temp - 1, cury);
                    curx = temp;
                    if (Random.Range(0, 2) == 0) dir = 0;
                    else dir = 2;
                    break;
                default:
                break;
            }
            counter++;
            arr.Clear();
                
        }

        finalx = curx;
        finaly = cury;
    }
}
