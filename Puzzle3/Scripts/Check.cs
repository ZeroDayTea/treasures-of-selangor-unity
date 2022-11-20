using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    // Start is called before the first frame update
    bool isClicked;
    public bool check;
    void Start()
    {
        check = false;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            isClicked = false;
        }

        if (!isClicked)
        {
            check = true;
            isClicked = true;
        }
    }


    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            isClicked = false;
            return;
        }

        if (!isClicked)
        {
            check = true;
            isClicked = true;
        }


    }
}
