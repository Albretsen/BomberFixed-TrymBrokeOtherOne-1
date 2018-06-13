﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //PUBLIC VARIABLES
    public float speed;
    public float bulletSpeed;
    public int rotationOffset;
    public float fireRate;
    public float viewDistance;
    public float bulletDestroySeconds;
    public bool disable;

    //PUBLIC REFERENCES
    public GameObject bulletPrefab;
    public GameObject muzzleFlashEffect;
    public Transform bulletSpawn;
    public Transform rotate;
    public LayerMask ignoreLayer;

    //SCRIPT VARIABLES
    bool walkLeft;
    bool walkRight;
    bool firstTimeSeen = true;
    float timeFired;
    int walkDirection;
    float nextFire = 0.0f;
    float shootAllowed = 0.0f;

    //REFERENCES
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform tf;
    Transform lsr; //LeftSideRay
    Transform rsr; //RightSideRay
    Transform arm;
    Transform player;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        lsr = transform.Find("LeftSideRay");
        rsr = transform.Find("RightSideRay");
        arm = transform.Find("Arm");

        //DECIDE INITIAL DIRECTION
        walkDirection = Random.Range(1, 3);
        switch (walkDirection)
        {
            case 1:
                walkLeft = true;
                break;
            case 2:
                walkRight = true;
                break;
            default:
                Debug.LogWarning("DID NOT FIND A WALK DIRECTION (ENEMYCONTROLLER)");
                break;
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fire()
    {
        //Checks if NEXTFIRE time has passed
        if (Time.time > nextFire && !disable)
        {

            nextFire = Time.time + fireRate;

            var heading = player.position - tf.position;

            //ARM ROTATION TEST
            Vector3 difference = player.position - arm.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            arm.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            var muzzleFlash = (GameObject)Instantiate(muzzleFlashEffect, bulletSpawn.position, bulletSpawn.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = heading.normalized * bulletSpeed;
            Destroy(muzzleFlash, 1f);

            Destroy(bullet, bulletDestroySeconds);
        }
    }

    void FixedUpdate()
    {
        //ARM ROTATION!
        //arm.RotateAround(rotate.position, Vector3.forward, 20 * Time.deltaTime);

        //PLAYERHIT SIDE RAYCAST
        if (!GameMaster.playerDead)
        {
            var heading = player.position - tf.position;
            RaycastHit2D playerhit = Physics2D.Raycast(arm.position, heading, viewDistance, ignoreLayer);
            Debug.DrawRay(arm.position, heading, Color.magenta);
            if (playerhit.collider != null)
            {
                if (playerhit.transform.tag == "Player" && Time.time > shootAllowed)
                {
                    if (firstTimeSeen)
                    {
                        shootAllowed = Time.time + 0.1f;
                        firstTimeSeen = false;
                        return;
                    }
                    Fire();
                }
            }
        }
        

        //LEFT SIDE RAYCAST
        RaycastHit2D hitLeftSide = Physics2D.Raycast(lsr.position, Vector2.left,1f);
        Debug.DrawRay(lsr.position, Vector2.left, Color.red);
        if (hitLeftSide.collider != null)
        {
            if(hitLeftSide.transform.tag == "PatrolPost")
            {
                walkLeft = false;
                walkRight = true;
            }
        }

        //RIGHT SIDE RAYCAST
        RaycastHit2D hitRightSide = Physics2D.Raycast(rsr.position, Vector2.right, 1f);
        Debug.DrawRay(rsr.position, Vector2.right, Color.red);
        if (hitRightSide.collider != null)
        {
            if (hitRightSide.transform.tag == "PatrolPost")
            {
                walkLeft = true;
                walkRight = false;
            }
        }

        //WALK LEFT
        if (walkLeft)
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-speed * Time.deltaTime * 100f, rb.velocity.y);
        }
        //WALK RIGHT
        if (walkRight)
        {
            sr.flipX = false;
            rb.velocity = new Vector2(speed * Time.deltaTime * 100f, rb.velocity.y);
        }
    }
}
