using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    Vector2 localDirection;
    public static float distance = 1f;
    public static bool playerDead = false;
    public static string selectedChar;

    public GameObject playerPrefab;
    public Transform spawn;
    public GameObject[] characterList;
    PlayerController playerController;

    void Start () {
        playerDead = false;
        selectedChar = PlayerPrefs.GetString("SelectedChar", "Default");
        spawn = GameObject.Find("PlayerSpawnPos").GetComponent<Transform>();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        var Player = (GameObject)Instantiate(characterList[0], spawn.position, transform.rotation);
        Player.name = "Player(Clone)";
        playerController = Player.GetComponent<PlayerController>();
    }

    //STORES A VECTOR2 DIRECTIOM WHICH IS FIRST GIVEN BY THE PLAYERCONTROLLER SCRIPT(1),
    //THEN RETRIEVED BY THE OBJECTIVEEXPLOSION SCRIPT (2)!
    public Vector2 Direction(Vector2 direction, int script)
    {
        if(script == 1)
        {
            localDirection = direction;
            return direction;
        }
        if(script == 2)
        {
            return -localDirection;
        }
        return direction;
    }

    //MOBILE CONTROLS
    public void MoveRight()
    {
        playerController.MoveRight();
    }
    public void MoveLeft()
    {
        playerController.MoveLeft();
    }

    public void MoveRightReleased()
    {
        playerController.MoveRightReleased();
    }
    public void MoveLeftReleased()
    {
        playerController.MoveLeftReleased();
    }

    public void JumpMobile()
    {
        playerController.JumpMobile();
    }
    public void JumpMobileReleased()
    {
        playerController.JumpMobileReleased();
    }
    public void Hit()
    {
        playerController.Hit();
    }
}
