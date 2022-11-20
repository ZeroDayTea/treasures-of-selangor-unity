using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Script : MonoBehaviour
{
    int x, y;
    public GameObject logic;
    private ArrayOfWalls a;

    void Start()
    {
        //logic = GameObject.Find("Logic");
        a = logic.GetComponent<ArrayOfWalls>();
    }


    void Update()
    {
        if (x != a.finalx || y != a.finaly)
        {
            x = a.finalx;
            y = a.finaly;
            gameObject.transform.position = new Vector3((float)(x - 8), 0.0f, (float)(y - 4.5));
            
        }
    }
        
}
