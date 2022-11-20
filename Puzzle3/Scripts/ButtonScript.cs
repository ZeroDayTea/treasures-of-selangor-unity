using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool isClicked;
    public TextMesh t;
    public GameObject logic;
    public GameObject colorChanging;
    public int number;
    public int position;
    public int colorB;//1 = red, 2 = orange, 3 = green
    int curColor;
    public int buttonType;
    public bool numCorrect = false;

    void Start()
    {
        number = 1;
        isClicked = false;
        curColor = 1;
        colorB = 1;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if(buttonType == 1 && Input.GetKeyDown(KeyCode.Z)) {
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
        else if(buttonType == 3 && Input.GetKeyDown(KeyCode.C)) {
            if(isClicked) {
                isClicked = false;
            }
            else {
                isClicked = true;
            }
        }

        if(!isClicked)
        {
            number++;
            if(number > 9)
            {
                number = 1;
            }
            isClicked = true;
        }

        t.text = "" + number;
        if(curColor != colorB)
        {
            if(colorB == 1)
            {
                colorChanging.GetComponent<Renderer>().material = Resources.Load("Materials/Red") as Material;
            }
            else if (colorB == 2)
            {
                colorChanging.GetComponent<Renderer>().material = Resources.Load("Materials/Orange") as Material;
            }
            else
            {
                colorChanging.GetComponent<Renderer>().material = Resources.Load("Materials/Green") as Material;
                numCorrect = true;
            }
            curColor = colorB;
        }
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            isClicked = false;
            return;
        }

        if(!isClicked)
        {
            number++;
            if(number > 9)
            {
                number = 1;
            }
            isClicked = true;
        }

        
    }
}
