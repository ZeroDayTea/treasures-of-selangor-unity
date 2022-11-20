using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    bool isClicked;
    public bool blocking;
    public void Start()
    {
        isClicked = true;
        blocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(isClicked) {
                isClicked = false;
            }
            else {
                isClicked = true;
            }
        }

        if (!isClicked)
        {
            blocking = true;
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
            blocking = true;
            isClicked = true;
        }


    }
}
