using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour {
    public int playerSpeed = 10;
    public int playerJumpPower = 1250;

    private float moveX;

    private bool facingRight = false;
    private bool pause = false;
    public bool ground;
    
    Rigidbody2D rb;

    Animator anim;

    SpriteRenderer arrow;

    GameObject player;

    private void Awake()
    {
        rb = GameObject.Find("Player_0").GetComponent<Rigidbody2D>();
        arrow = GameObject.Find("Arrow").GetComponent<SpriteRenderer>();
        arrow.enabled = false;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (pause)
        {
            WaitForClick();
        } else
        {
            PlayerMove();
        }
    }

    void WaitForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pause = false;
            rb.isKinematic = false;
            arrow.enabled = false;
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            rb.AddForce(dir * 2000);
        }
    }
 

    void PlayerMove()
    {
        moveX = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveX));

        if (Input.GetButtonDown("Jump") && ground)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && !ground)
        {
            DisableMovement();
        }

        if (moveX < 0.0f && !facingRight)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight)
        {
            FlipPlayer();
        }

        if (ground)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void DisableMovement()
    {
        pause = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        moveX = 0;
        anim.SetFloat("Speed", 0);

        arrow.enabled = true;
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        ground = false;
        anim.SetBool("Ground", false);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        Vector2 arrowLocalScale = arrow.transform.localScale;
        arrowLocalScale.x *= -1;
        arrow.transform.localScale = arrowLocalScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            anim.SetBool("Ground", true);
        }
    }
}
