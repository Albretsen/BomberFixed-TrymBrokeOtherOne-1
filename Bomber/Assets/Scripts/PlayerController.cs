﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //PUBLIC VARIABLES
    [Header("Movement settings")]
    public float speed;
    public float jumpStrength;
    [Header("Raycast ground check")]
    private Transform GroundCheck;
    public LayerMask WhatIsGround;
    public LayerMask WhatAffectedByExplosion;
    [Header("Explosion")]
    public GameObject explosionPrefab;
    public GameObject explosionFirePrefab;
    public float explosionRadius;

    //SCRIPT VARIABLES
    public float GroundedRadius = .2f;
    Vector2 movement;
    
    bool moveRight;
    bool moveLeft;
    bool jump;

    //REFERENCES
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    PlayAnimation playAnim;
    Objective obj;
    Transform tfObj;
    GameMaster gameMaster;

    // Use this for initialization
    void Start () {
        //SETTING UP REFERENCES
        rb = GetComponent<Rigidbody2D>();
        GroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        obj = GameObject.Find("Objective").GetComponent<Objective>();
        tfObj = GameObject.Find("Objective").GetComponent<Transform>();
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        //playAnim = GameObject.Find("Dead").GetComponent<PlayAnimation>();
    }

    public void Hit()
    {
        //CREATE EXPLOSION
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, WhatAffectedByExplosion);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                obj.Hit((transform.position - tfObj.position).normalized);
                GameMaster.distance = Vector2.Distance(transform.position, tfObj.position);
                Debug.Log(GameMaster.distance);
            }
        }

        gameMaster.Direction((transform.position - tfObj.position).normalized, 1);

        var explodingPlayer = (GameObject)Instantiate(explosionPrefab, rb.position, transform.rotation);
        var explosion = (GameObject)Instantiate(explosionFirePrefab, rb.position, transform.rotation);
        Destroy(gameObject);
        GameMaster.playerDead = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, explosionRadius);
        Gizmos.DrawSphere(GroundCheck.position, GroundedRadius);
    }

    // Update is called once per frame
    void Update () {

        //DETECT MANUAL EXPLOSION
        if (Input.GetKeyDown("space"))
        {
            Hit();
        }

        //DETECT HORIZONTAL INPUT
        if(Input.GetAxisRaw("Horizontal") != 0 || moveRight || moveLeft)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                HorizontalMovement(Input.GetAxisRaw("Horizontal"));
            }
            else if (moveRight)
            {
                HorizontalMovement(1);
            }
            else if (moveLeft)
            {
                HorizontalMovement(-1);
            }

            //STARTS WALKING ANIMATION
            if (IsGrounded())
            {
                anim.SetInteger("State", 1);
            }
            //FLIPS THE SPRITE RENDERER
            if(Input.GetAxisRaw("Horizontal") < 0 || moveLeft)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
            //MOVE AFTER LANDING
            if(rb.velocity.y > 0.5f || rb.velocity.y < -0.2f)
            {
                if (IsGrounded())
                {
                    anim.SetInteger("State", 2);
                }
            }
        }
        else
        {
            HorizontalMovement(0);
            //STARTS IDLE ANIMATION
            if (IsGrounded())
            {
                anim.SetInteger("State", 0);
            }
        }

        //DETECT VERTICAL INPUT
        if(Input.GetAxisRaw("Vertical") > 0 || jump)
        {
            if (IsGrounded())
            {
                Jump();
            }
        }

        //FALLING ANIMATION WHEN GOING DOWN WITHOUT JUMPING
        if(!IsGrounded() && rb.velocity.y < -0.5)
        {
            anim.SetInteger("State", 4);
        }

        if(!IsGrounded() && rb.velocity.y > 0.5)
        {
            anim.SetInteger("State", 3);
        }
	}

    //JUMP
    void Jump()
    {
        //STARTS JUMP ANIMATION
        //anim.SetInteger("State", 3);
        rb.velocity = new Vector2(rb.velocity.x,jumpStrength);
    }

    //DIRECTION IS THE HORIZONTAL INPUT VALUE (-1 to 1)
    void HorizontalMovement(float direction)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //STARTS IDLE ANIMATION AFTER LANDING
            anim.SetInteger("State", 0);
        }
    }

    //CHECK IF GROUNDED
    bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        Gizmos.color = Color.yellow;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;
        }
        return false;
    }

    //MOBILE CONTROLS
    public void MoveRight()
    {
        moveRight = true;
    }
    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRightReleased()
    {
        moveRight = false;
    }
    public void MoveLeftReleased()
    {
        moveLeft = false;
    }

    public void JumpMobile()
    {
        jump = true;
    }
    public void JumpMobileReleased()
    {
        jump = false;
    }
}
