using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpin : MonoBehaviour
{
    public GameObject player;
    public GameObject arrow;

    GameObject exit;
    GameObject puzzle1;
    GameObject puzzle2;
    GameObject puzzle3;
    Vector3 finalpos;
    Vector3 playerpos;
    Vector3 dir;
    float dist1;
    float dist2;
    float dist3;
    float dist4;
    float lowest;
    float angle;

    // Update is called once per frame
    void Update()
    {
        exit = GameObject.Find("ExitDoor(Clone)");
        puzzle1 = GameObject.FindWithTag("Puzzle1");
        puzzle2 = GameObject.FindWithTag("Puzzle2");
        puzzle3 = GameObject.FindWithTag("Puzzle3");

        lowest = 999999.0f;

        dist1 = Vector3.Distance(player.transform.position, exit.transform.position);
        dist2 = Vector3.Distance(player.transform.position, puzzle1.transform.position);
        dist3 = Vector3.Distance(player.transform.position, puzzle2.transform.position);
        dist4 = Vector3.Distance(player.transform.position, puzzle3.transform.position);

        if (dist1 < lowest) 
        {
            lowest = dist1;
            finalpos = exit.transform.position;
        }

        if (dist2 < lowest) 
        {
            lowest = dist2;
            finalpos = puzzle1.transform.position;
        }

        if (dist3 < lowest) 
        {
            lowest = dist3;
            finalpos = puzzle2.transform.position;
        }

        if (dist4 < lowest) 
        {
            lowest = dist4;
            finalpos = puzzle3.transform.position;
        }

        //finalpos.z = 0;
        
        dir = player.transform.InverseTransformDirection(player.transform.position - finalpos);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.eulerAngles = new Vector3(0, 0, angle + 90.0f);
    }
}
