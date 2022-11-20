using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public RuntimeAnimatorController bat;
    public RuntimeAnimatorController snake;
    public Animator animator;
    public bool hurtPlayer = false;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        float rand = UnityEngine.Random.Range(0.0f, 2.0f);

        if(rand < 1) 
        {
            animator.runtimeAnimatorController = bat as RuntimeAnimatorController;
            Vector3 pos = new Vector3(transform.position.x, -3.2f, transform.position.z);
            transform.position = pos;
        }
        if(rand >= 1) 
        {
            animator.runtimeAnimatorController = snake as RuntimeAnimatorController;
            Vector3 pos = new Vector3(transform.position.x, -3.5f, transform.position.z);
            transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        float dist = Vector3.Distance(transform.position, player.transform.position);
        //RaycastHit hit;
        if (!Physics.Linecast(transform.position, player.transform.position)) 
        {
            if(dist < 25.0f && !hurtPlayer)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
            if(dist < 4.0f && !hurtPlayer)
            {
                animator.SetTrigger("isAttack");
                hurtPlayer = true;
                player.GetComponent<OtherPlayerMovement>().health -= 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(dist < 6.0f)
            {
                animator.SetBool("isStunned", true);
                this.enabled = false;
            }
        }
    }
}
