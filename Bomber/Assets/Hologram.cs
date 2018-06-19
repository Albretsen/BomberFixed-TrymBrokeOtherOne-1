using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hologram : MonoBehaviour {

    public GameObject text;
    public Text description;
    public Transform hologram;
    public Transform spawnPoint;
    Animator anim;
    Animator charAnim;
    string tomDescription;
    string tommyDescription;
    string tom2xDescription;
    GameObject instantiatedChar;

    public GameObject tom;
    public GameObject tommy;
    public GameObject tom2X;

    int pos;
    float timePassed;

    void Start()
    {
        //INITIALIZE DESCRIPTIONS
        tomDescription = "This is a very general description about the most average bomber you will ever find, Tom.";
        tommyDescription = "This is a very general description about the most average bomber you will ever find, Tommy.";
        tom2xDescription = "This is a very general description about the most average bomber you will ever find, Tom2X.";

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        anim = text.GetComponent<Animator>();

        switch (PlayerPrefs.GetString("SelectedChar"))
        {
            case "Tom":
                pos = 0;
                anim.SetInteger("State", 10);
                description.text = tomDescription;
                break;
            case "Tommy":
                pos = 1;
                anim.SetInteger("State", 20);
                description.text = tommyDescription;
                break;
            case "Tom2X":
                pos = 2;
                anim.SetInteger("State", 30);
                description.text = tom2xDescription;
                break;
        }

        InstantiateChar(false);
    }

    public void ArrowRight()
    {
        if (Time.time > timePassed)
        {
            timePassed = Time.time + 0.9f;
            switch (pos)
            {
                case 0:
                    DeleteChar();
                    pos = 1;
                    charAnim.SetInteger("Anim", 1);
                    anim.SetInteger("State", pos);
                    PlayerPrefs.SetString("SelectedChar", "Tommy");
                    description.text = tommyDescription;
                    InstantiateChar(true);
                    break;
                case 1:
                    DeleteChar();
                    pos = 2;
                    charAnim.SetInteger("Anim", 1);
                    anim.SetInteger("State", pos);
                    PlayerPrefs.SetString("SelectedChar", "Tom2X");
                    description.text = tom2xDescription;
                    InstantiateChar(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void ArrowLeft()
    {
        if (Time.time > timePassed)
        {
            timePassed = Time.time + 0.9f;
            switch (pos)
            {
                case 2:
                    DeleteChar();
                    pos = 1;
                    charAnim.SetInteger("Anim", 1);
                    anim.SetInteger("State", pos);
                    PlayerPrefs.SetString("SelectedChar", "Tommy");
                    description.text = tommyDescription;
                    InstantiateChar(true);
                    break;
                case 1:
                    DeleteChar();
                    pos = 0;
                    charAnim.SetInteger("Anim", 1);
                    anim.SetInteger("State", pos);
                    PlayerPrefs.SetString("SelectedChar", "Tom");
                    description.text = tomDescription;
                    InstantiateChar(true);
                    break;
                default:
                    break;
            }
        }
    }

    void InstantiateChar(bool includeDelay)
    {
        switch (PlayerPrefs.GetString("SelectedChar"))
        {
            case "Tom":
                if (!includeDelay)
                {
                    instantiatedChar = (GameObject)Instantiate(tom, spawnPoint.position, spawnPoint.rotation, hologram);
                    charAnim = instantiatedChar.GetComponent<Animator>();
                }
                else if (includeDelay)
                {
                    StartCoroutine(TomWait());
                }      
                break;
            case "Tommy":
                if (!includeDelay)
                {
                    instantiatedChar = (GameObject)Instantiate(tommy, spawnPoint.position, spawnPoint.rotation, hologram);
                    charAnim = instantiatedChar.GetComponent<Animator>();
                }
                else if (includeDelay)
                {
                    StartCoroutine(TommyWait());
                }
                break;
            case "Tom2X":
                if (!includeDelay)
                {
                    instantiatedChar = (GameObject)Instantiate(tom2X, spawnPoint.position, spawnPoint.rotation, hologram);
                    charAnim = instantiatedChar.GetComponent<Animator>();
                }
                else if (includeDelay)
                {
                    StartCoroutine(Tom2XWait());
                }
                break;
            default:
                break;
        }
    }

    void DeleteChar()
    {
        Destroy(instantiatedChar,1f);
    }

    IEnumerator TomWait()
    {
        yield return new WaitForSeconds(0.1f);
        instantiatedChar = (GameObject)Instantiate(tom, spawnPoint.position, spawnPoint.rotation, hologram);
        charAnim = instantiatedChar.GetComponent<Animator>();
    }

    IEnumerator TommyWait()
    {
        yield return new WaitForSeconds(0.1f);
        instantiatedChar = (GameObject)Instantiate(tommy, spawnPoint.position, spawnPoint.rotation, hologram);
        charAnim = instantiatedChar.GetComponent<Animator>();
    }

    IEnumerator Tom2XWait()
    {
        yield return new WaitForSeconds(0.1f);
        instantiatedChar = (GameObject)Instantiate(tom2X, spawnPoint.position, spawnPoint.rotation, hologram);
        charAnim = instantiatedChar.GetComponent<Animator>();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
