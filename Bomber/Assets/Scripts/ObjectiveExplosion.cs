using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveExplosion : MonoBehaviour
{    
    //TWEAKABLE VARIABLES
    public float explosionForce;

    //REFERENCES
    public Rigidbody2D head;
    public Rigidbody2D armRight;
    public Rigidbody2D armLeft;
    public Rigidbody2D footRight;
    public Rigidbody2D footLeft;
    public Rigidbody2D torso;

    PauseMenu pauseMenu;

    //
    Vector2 direction = new Vector2(1f, 0.1f);
    GameMaster gm;
    bool hasExploded = false;
    float timePassed;

    // Use this for initialization
    void Start()
    {
        pauseMenu = GameObject.Find("MENU").GetComponent<PauseMenu>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        Objective.objectiveDead = true;
    }

    void Update()
    {
        Vector2 dir = new Vector2(0, 0);
        direction = gm.Direction(dir, 2);
        if(direction.y > 0.01 || direction.x > 0.01 || direction.y < -0.01 || direction.x < -0.01)
        {
            Explode(direction);
        }

        //IF IT HAS EXPLODED AND 1 SECONDS HAVE PASSED, CHANGE SCENE!
        if(hasExploded && timePassed < Time.time)
        {
            pauseMenu.WinScreen(true);
        }
    }

    public void Explode(Vector2 direction)
    {
        if (!hasExploded)
        {
            head.AddForce(direction * explosionForce * 1.2f / GameMaster.distance, ForceMode2D.Impulse);
            armRight.AddForce(direction * explosionForce * 1.5f / GameMaster.distance, ForceMode2D.Impulse);
            armLeft.AddForce(direction * explosionForce * 1.5f / GameMaster.distance, ForceMode2D.Impulse);
            footRight.AddForce(direction * explosionForce * 1.35f / GameMaster.distance, ForceMode2D.Impulse);
            footLeft.AddForce(direction * explosionForce * 1.35f / GameMaster.distance, ForceMode2D.Impulse);
            torso.AddForce(direction * explosionForce * 1f / GameMaster.distance, ForceMode2D.Impulse);

            hasExploded = true;
        }
    }
}
