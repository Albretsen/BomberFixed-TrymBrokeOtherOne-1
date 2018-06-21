using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //PUBLIC VARIABLES
    public float speed;
    public float bulletSpeed;
    public int rotationOffset;
    public float fireRate;
    public float viewDistance;
    public float bulletDestroySeconds;
    public bool gravityBullets;
    public bool disable;

    //PUBLIC REFERENCES
    public GameObject bulletPrefab;
    public GameObject bulletGravityPrefab;
    public GameObject muzzleFlashEffect;
    public Transform bulletSpawn;
    public Transform rotate;
    public LayerMask ignoreLayer;
    public LayerMask patrolpost;

    //SCRIPT VARIABLES
    bool walkLeft;
    bool walkRight;
    bool firstTimeSeen = true;
    bool flipLeft;
    bool flipRight;
    bool stop;
    float timeFired;
    float timePassed = 0;
    int walkDirection;
    float nextFire = 0.0f;
    float shootAllowed = 0.0f;
    GameObject bullet;

    //REFERENCES
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform tf;
    public SpriteRenderer armRenderer;
    public Transform lsr; //LeftSideRay
    public Transform rsr; //RightSideRay
    public Transform arm;
    Transform player;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        //player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        /*lsr = transform.Find("LeftSideRay");
        rsr = transform.Find("RightSideRay");
        arm = gameObject.transform.GetChild(0);*/

        //DECIDE INITIAL DIRECTION
        walkDirection = Random.Range(1, 3);
        switch (walkDirection)
        {
            case 1:
                walkLeft = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
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
    void Update()
    {

    }

    void Fire()
    {
        //Checks if NEXTFIRE time has passed
        if (Time.time > nextFire && !disable)
        {

            nextFire = Time.time + fireRate;

            var heading = player.position - tf.position;

            //ARM ROTATION TEST
            /*Vector3 difference = player.position - arm.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            arm.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);*/

            if (!gravityBullets)
            {
                bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            }
            else
            {
                bullet = (GameObject)Instantiate(bulletGravityPrefab, bulletSpawn.position, bulletSpawn.rotation);
            }
            var muzzleFlash = (GameObject)Instantiate(muzzleFlashEffect, bulletSpawn.position, bulletSpawn.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = heading.normalized * bulletSpeed;
            Destroy(muzzleFlash, 1f);

            Destroy(bullet, bulletDestroySeconds);
        }
    }

    void FixedUpdate()
    {
        if (!GameMaster.playerDead)
        {
            player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        }
        //ARM ROTATION!
        //arm.RotateAround(rotate.position, Vector3.forward, 20 * Time.deltaTime);
        var heading = new Vector2(0,0);
        //PLAYERHIT SIDE RAYCAST
        if (!GameMaster.playerDead)
        {
            heading = player.position - tf.position;
            RaycastHit2D playerhit = Physics2D.Raycast(bulletSpawn.position, heading, viewDistance, ignoreLayer);
            Debug.DrawRay(bulletSpawn.position, heading, Color.magenta);
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
                    stop = true;
                    Vector3 difference = player.position - arm.position;
                    difference.Normalize();
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    if(tf.rotation.y != 0)
                    {
                        arm.rotation = Quaternion.Euler(0f, 0, rotZ + rotationOffset);
                        armRenderer.flipY = true;
                    }
                    else
                    {
                        arm.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
                        armRenderer.flipY = false;
                    }

                }
                else
                {
                    stop = false;
                }
            }
        }


        //LEFT SIDE RAYCAST
        /*RaycastHit2D hitLeftSide = Physics2D.Raycast(lsr.position, Vector2.left, 0.2f);
        Debug.DrawRay(lsr.position, Vector2.left, Color.red);
        if (hitLeftSide.collider != null)
        {
            if (hitLeftSide.transform.tag == "PatrolPost")
            {
                walkLeft = false;
                walkRight = true;
            }
        }

        //RIGHT SIDE RAYCAST
        RaycastHit2D hitRightSide = Physics2D.Raycast(rsr.position, Vector2.right, 0.2f);
        Debug.DrawRay(rsr.position, Vector2.right, Color.red);
        if (hitRightSide.collider != null)
        {
            if (hitRightSide.transform.tag == "PatrolPost")
            {
                walkLeft = true;
                walkRight = false;
            }
        }*/

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1, patrolpost);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.name == "PatrolpostLeft")
            {
                walkLeft = false;
                walkRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (colliders[i].gameObject.name == "PatrolpostRight")
            {
                walkRight = false;
                walkLeft = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        //WALK LEFT
        if (stop)
        {
            if(heading.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        if (!stop)
        {
            if (walkLeft)
            {
                //sr.flipX = true;
                rb.velocity = new Vector2(-speed * Time.deltaTime * 100f, rb.velocity.y);
            }
            //WALK RIGHT
            if (walkRight)
            {
                //sr.flipX = false;
                rb.velocity = new Vector2(speed * Time.deltaTime * 100f, rb.velocity.y);
            }
        }
    }
}
