using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleObjectScript : MonoBehaviour
{
    public int puzzleType;
    public bool puzzleSolved = false;
    public GameObject player;
    public GameObject puzzleImage;
    public GameObject puzzle1Obj;
    public GameObject puzzle1Cam;
    public GameObject puzzle2Obj;
    public GameObject puzzle2Cam;
    public GameObject puzzle3Obj;
    public GameObject puzzle3Cam;
    public GameObject puzzleParent;

    public Material puzzle1Mat;
    public Material puzzle2Mat;
    public Material puzzle3Mat;

    //public GameObject puzzle3Logic;
    //public GameObject puzzle2but1;
    //public GameObject puzzle2but2;
    //public GameObject puzzle2but3;
    //public GameObject puzzle2but4;

    public GameObject puzzleInstance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        puzzleParent = GameObject.Find("PuzzleParent");
        puzzleImage = FindObject(player, "PuzzleImage");
        //puzzle1Obj = FindObject(puzzleParent, "Puzzle1Object");
        puzzle1Cam = FindObject(puzzleParent, "Puzzle1Cam");
        //puzzle2Obj = FindObject(puzzleParent, "Puzzle2Object");
        puzzle2Cam = FindObject(puzzleParent, "Puzzle2Cam");
        //puzzle3Obj = FindObject(puzzleParent, "Puzzle3Object");
        puzzle3Cam = FindObject(puzzleParent, "Puzzle3Cam");
        //puzzle3Logic = FindObject(puzzle3Obj, "logic");
        //puzzle2but1 = FindObject(puzzle2Obj, "Red");
        //puzzle2but2 = FindObject(puzzle2Obj, "Green");
        //puzzle2but3 = FindObject(puzzle2Obj, "Blue");
        //puzzle2but4 = FindObject(puzzle2Obj, "StartFlow");

        //puzzle1Obj = (GameObject)Resources.Load("Puzzle1Object");
        //puzzle2Obj = (GameObject)Resources.Load("Puzzle2Object");
        //puzzle3Obj = (GameObject)Resources.Load("Puzzle3Object");

        if(puzzleType == 3) 
        {
            puzzleImage.SetActive(false);
            //puzzle1Obj.SetActive(false);
            puzzle1Cam.SetActive(false);
            //puzzle2Obj.SetActive(false);
            puzzle2Cam.SetActive(false);
            //puzzle3Obj.SetActive(false);
            puzzle3Cam.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, transform.position) < 5.0f && puzzleSolved == false) 
        {
            player.GetComponent<OtherPlayerMovement>().currentlyInPuzzle = true;

            if(puzzleType == 1) {
                puzzleImage.SetActive(true);
                //puzzle1Obj.SetActive(true);
                puzzleInstance = Instantiate(puzzle1Obj) as GameObject;
                puzzle1Cam.SetActive(true);
                puzzleImage.GetComponent<MeshRenderer>().material = puzzle1Mat;
                //puzzleImage.GetComponent<VirtualScreen>().screenCamera = puzzle1Cam.GetComponent<Camera>();

                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if(puzzleType == 2) {
                puzzleImage.SetActive(true);
                //puzzle2Obj.SetActive(true);
                puzzleInstance = Instantiate(puzzle2Obj) as GameObject;
                puzzle2Cam.SetActive(true);
                puzzleImage.GetComponent<MeshRenderer>().material = puzzle2Mat;
                //puzzleImage.GetComponent<VirtualScreen>().screenCamera = puzzle2Cam.GetComponent<Camera>();

                //puzzle2Logic.GetComponent<Logic>().Start();
                //puzzle2but1.GetComponent<Button>().Start();
                //puzzle2but2.GetComponent<Button>().Start();
                //puzzle2but3.GetComponent<Button>().Start();
                //puzzle2but4.GetComponent<ButtonBlock>().Start();

                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if(puzzleType == 3) {
                puzzleImage.SetActive(true);
                //puzzle3Obj.SetActive(true);
                puzzleInstance = Instantiate(puzzle3Obj) as GameObject;
                puzzle3Cam.SetActive(true);
                puzzleImage.GetComponent<MeshRenderer>().material = puzzle3Mat;
                //puzzleImage.GetComponent<VirtualScreen>().screenCamera = puzzle3Cam.GetComponent<Camera>();

                //puzzle3Logic.GetComponent<logic>().restart = true;

                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if(puzzleSolved) 
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            player.GetComponent<OtherPlayerMovement>().currentlyInPuzzle = true;

            puzzleImage.SetActive(false);
            //puzzle1Obj.SetActive(false);
            puzzle1Cam.SetActive(false);
            //puzzle2Obj.SetActive(false);
            puzzle2Cam.SetActive(false);
            //puzzle3Obj.SetActive(false);
            puzzle3Cam.SetActive(false);

            Destroy(puzzleInstance);

            this.enabled = false;
        }
    }

    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs)
        {
            if(t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
