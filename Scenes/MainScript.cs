using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void EnterMainGameMale()
    {
        PlayerPrefs.SetInt("playerType", 0);
        SceneManager.LoadScene("MainScene");
    }
    public void EnterMainGameFemale()
    {
        PlayerPrefs.SetInt("playerType", 1);
        SceneManager.LoadScene("MainScene");
    }

    public void AboutGame()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void OptionsGame()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ReturnGame()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
