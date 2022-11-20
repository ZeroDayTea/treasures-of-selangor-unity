using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    bool isClicked;
    public bool blocking;
    public int buttonType;

    public void Start()
    {
        isClicked = true;
        blocking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonType == 1 && Input.GetKeyDown(KeyCode.C)) {
            if(isClicked) {
                isClicked = false;
            }
            else {
                isClicked = true;
            }
        }
        else if(buttonType == 2 && Input.GetKeyDown(KeyCode.X)) {
            if(isClicked) {
                isClicked = false;
            }
            else {
                isClicked = true;
            }
        }
        else if(buttonType == 3 && Input.GetKeyDown(KeyCode.Z)) {
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
  