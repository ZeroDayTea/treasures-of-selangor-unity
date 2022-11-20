using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeScreen : MonoBehaviour
{
    public Text MenuText;
    public GameObject Screen;
    public Sprite menuScreen;
    //public Button escapeButton;
    public GameObject player;
    //public GameObject terrain;
    public bool screenUp = false;
    public GameObject escapeButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuText.text = "";
        Screen.GetComponent<SpriteRenderer>().sprite = null;
        escapeButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && screenUp == false)
        {
            MenuText.text = "Menu";
            Screen.GetComponent<SpriteRenderer>().sprite = menuScreen;
            escapeButton.SetActive(true);
            //player.GetComponent<Other Player Movement>().playerSpeed = 0;
            screenUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && screenUp == true)
        {
            MenuText.text = "";
            Screen.GetComponent<SpriteRenderer>().sprite = null;
            escapeButton.SetActive(false);
            //player.GetComponent<PlayerController>().playerSpeed = 0.13f + (terrain.GetComponent<LevelGen>().levelNumber / 100f);
            screenUp = false;
        }
    }

    public void buttonClick()
    {
        SceneManager.LoadScene(1);
    }
}
