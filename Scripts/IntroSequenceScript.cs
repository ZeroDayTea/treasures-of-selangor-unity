using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequenceScript : MonoBehaviour
{
    public float timeUntilSwitch = 36.0f;

    // Update is called once per frame
    void Update()
    {
        if(timeUntilSwitch > 0)
        {
            timeUntilSwitch -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
