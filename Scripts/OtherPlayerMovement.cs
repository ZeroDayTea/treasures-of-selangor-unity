using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OtherPlayerMovement : MonoBehaviour
{
    //Components
    Rigidbody myRB;
    Transform myAvatar;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //animator controllers
    public RuntimeAnimatorController maleController;
    public RuntimeAnimatorController femaleController;

    public AudioSource torchSwing;

    //Player movement
    [SerializeField] InputAction WASD;
    Vector2 movementInput;
    [SerializeField] float movementSpeed;

    public int keyFragments = 0;
    public int health = 5;
    public int score = 0;
    public bool currentlyInPuzzle = false;
    public bool isDead = false;
    public int currentLevel = 1;

    public Text healthText;
    public Text keyText;
    public Text scoreText;

    public GameObject deadImage;
    public Text deadText;
    public Text deadText2;
    public Text deadScoreText;
    public GameObject deadButton;
    public Text levelText;
    
    
    private void OnEnable() {
        WASD.Enable();
    }

    private void OnDisable() {
        WASD.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAvatar = transform.GetChild(0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetBool("isRunning", false);
        animator.SetBool("isStunned", false);

        //Male Code
        if(PlayerPrefs.GetInt("playerType") == 0) 
        {
            animator.runtimeAnimatorController = maleController as RuntimeAnimatorController;
        }
        //Female Code
        else if(PlayerPrefs.GetInt("playerType") == 1) 
        {
            animator.runtimeAnimatorController = femaleController as RuntimeAnimatorController;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        keyText.text = keyFragments.ToString() + "/3";
        scoreText.text = "Score: " + score.ToString();
        levelText.text = "Level: " + currentLevel.ToString();

        if(health <= 0) 
        {
            //death sequence
            isDead = true;
            animator.SetBool("isStunned", true);
            myRB.constraints = RigidbodyConstraints.FreezeAll;

            deadImage.SetActive(true);
            deadText.gameObject.SetActive(true);
            deadText2.gameObject.SetActive(true);
            deadScoreText.gameObject.SetActive(true);
            deadScoreText.text = "Final Score: " + score.ToString();
            deadButton.SetActive(true);
        }
        
        movementInput = WASD.ReadValue<Vector2>();
        if(movementInput.x == 0 && movementInput.y == 0) {
            animator.SetBool("isRunning", false);
        }
        else {
            animator.SetBool("isRunning", true);
        }

        if(movementInput.x != 0)
        {
            myAvatar.localScale = new Vector2(Mathf.Sign(movementInput.x), 1);

            if(Mathf.Sign(movementInput.x) < 0 && !isDead){
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            //if(currentlyInPuzzle == false) 
            //{
                animator.SetBool("isRunning", false);
                animator.SetTrigger("isAttacking");
                torchSwing.Play();
            //}
        }

    }

    private void FixedUpdate() {
        myRB.velocity = new Vector3(movementInput.x, 0, movementInput.y) * movementSpeed;
    }
}
