using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {

    // Use this for initialization
    int x, y, inver, inhor;
    
    float horspeed;
    float verspeed;
    bool moving;
    public GameObject logic;
    private ArrayOfWalls a;
    bool restart;
    public int score;
    public GameObject player;
    public GameObject puzzleObj;

    


    void Start () {
        gameObject.transform.position = new Vector3(-7.0f, 0f, -3.5f);
        x = 10;
        y = 10;
        horspeed = 0;
        verspeed = 0;
        inver = 0;
        inhor = 0;
        moving = false;
        //logic = GameObject.Find("Logic");
        a = logic.GetComponent<ArrayOfWalls>();
        restart = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(score >= 3) {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<OtherPlayerMovement>().keyFragments += 1;
            puzzleObj = GameObject.FindGameObjectWithTag("Puzzle1");
            puzzleObj.GetComponent<PuzzleObjectScript>().puzzleSolved = true;
        }

        if (restart)
        {
            gameObject.transform.position = new Vector3(-7.0f, 0f, -3.5f);
            x = 10;
            y = 10;
            horspeed = 0;
            verspeed = 0;
            inver = 0;
            inhor = 0;
            moving = false;
            restart = false;
        }
        if (!moving)
        {
            checkInput();
        }
        checkBlock();
        transform.Translate(horspeed*0.1f, 0, verspeed*0.1f);
        x += inhor;
        y += inver;
        
	}

    private void OnTriggerEnter(Collider other)
    {
        score += 1;
        restart = true;
        a.restart = true;
    }

    void checkInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                horspeed = 1f;
                inhor = 1;
                moving = true;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                horspeed = -1f;
                inhor = -1;
                moving = true;
            }
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                verspeed = 1f;
                inver = 1;
                moving = true;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                verspeed = -1f;
                inver = -1;
                moving = true;
            }
        }
    }

    void checkBlock()
    {
        int caseSwitch = inhor * 2 + inver;
        
        switch(caseSwitch)
        {
            case -2:
                if(a.blocking[(x-1)/10,y/10])
                {
                    moving = false;
                    horspeed = 0;
                    verspeed = 0;
                    inver = 0;
                    inhor = 0;
                }
                break;
            case -1:
                if (a.blocking[x/10, (y-1)/10])
                {
                    moving = false;
                    horspeed = 0;
                    verspeed = 0;
                    inver = 0;
                    inhor = 0;
                }
                break;
            case 1:
                if (a.blocking[x/10, y/10+1])
                {
                    moving = false;
                    horspeed = 0;
                    verspeed = 0;
                    inver = 0;
                    inhor = 0;
                }
                break;
            case 2:
                if (a.blocking[x/10 + 1, y/10])
                {
                    moving = false;
                    horspeed = 0;
                    verspeed = 0;
                    inver = 0;
                    inhor = 0;
                }
                break;
            default:
                break;
        }
    }

    
}
