using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorScript : MonoBehaviour
{

    public GameObject player;
    public bool ableToExit = false;
    public GameObject mapGenerator;
    public GameObject[] levelComponents;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<OtherPlayerMovement>().keyFragments >= 3) {
            ableToExit = true;
        }

        if(Input.GetKey(KeyCode.E) && ableToExit == true && Vector3.Distance(player.transform.position, transform.position) < 5.0f) {
            
            levelComponents = GameObject.FindGameObjectsWithTag("LevelComponent");
            foreach(GameObject levelComponent in levelComponents) {
                Destroy(levelComponent);
            }

            Destroy(GameObject.FindGameObjectWithTag("Puzzle1"));
            Destroy(GameObject.FindGameObjectWithTag("Puzzle2"));
            Destroy(GameObject.FindGameObjectWithTag("Puzzle3"));

            mapGenerator.GetComponent<MapGenerator>().GenerateMap();
            player.GetComponent<OtherPlayerMovement>().keyFragments = 0;
            player.GetComponent<OtherPlayerMovement>().currentLevel += 1;
            player.GetComponent<OtherPlayerMovement>().score += 100;
            player.GetComponent<OtherPlayerMovement>().health = 5;
        }
    }
}
