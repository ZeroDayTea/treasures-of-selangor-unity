using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject check;
    Check c;
    public bool restart;
    public GameObject button1, button2, button3;
    ButtonScript b1, b2, b3;
    int[] combo;
    public GameObject player;
    public GameObject puzzleObj;

    void Start()
    {
        c = check.GetComponent<Check>();
        restart = true;
        combo = new int[3];
        b1 = button1.GetComponent<ButtonScript>();
        b2 = button2.GetComponent<ButtonScript>();
        b3 = button3.GetComponent<ButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(restart)
        {
            chooseNumbers();
            restart = false;
        }

        if(b1.numCorrect == true && b2.numCorrect == true && b3.numCorrect == true) {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<OtherPlayerMovement>().keyFragments += 1;
            puzzleObj = GameObject.FindGameObjectWithTag("Puzzle3");
            puzzleObj.GetComponent<PuzzleObjectScript>().puzzleSolved = true;
        }

        if (c.check)
        {
            if (b1.number == combo[0])
            {
                b1.colorB = 3;
            }
            else if (b1.number == combo[1] || b1.number == combo[2])
            {
                b1.colorB = 2;
            }
            else b1.colorB = 1;



            if (b2.number == combo[1])
            {
                b2.colorB = 3;
            }
            else if (b2.number == combo[0] || b2.number == combo[2])
            {
                b2.colorB = 2;
            }
            else b2.colorB = 1;



            if (b3.number == combo[2])
            {
                b3.colorB = 3;
            }
            else if (b3.number == combo[1] || b3.number == combo[0])
            {
                b3.colorB = 2;
            }
            else b3.colorB = 1;

            c.check = false;
        }
    }

    void chooseNumbers()
    {
        int r;
        combo[0] = 0;
        combo[1] = 0;
        combo[2] = 0;
        r = Random.Range(1, 10);
        combo[0] = r;
        while(combo[0] == r)
        {
            r = Random.Range(1, 10);
        }
        combo[1] = r;
        while (combo[0] == r || combo[1] == r)
        {
            r = Random.Range(1, 10);
        }
        combo[2] = r;

    }
}

