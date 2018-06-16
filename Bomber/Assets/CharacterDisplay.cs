using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    string currChar;

    public GameObject displayTomPrefab;
    public GameObject displayBigBombTomPrefab;
    public GameObject displayTommmyPrefab;

    bool characterDisplayedCurrently;

    GameObject displayedChar;
    Animator anim;


    void Start () {
        characterDisplayedCurrently = false;
        currChar = PlayerPrefs.GetString("SelectedChar");
        Display(currChar);	
	}

    public void Display(string character)
    {
        PlayerPrefs.SetString("SelectedChar", character);

        currChar = PlayerPrefs.GetString("SelectedChar");

        if (characterDisplayedCurrently)
        {
            anim.SetInteger("State", 1);
            currChar = PlayerPrefs.GetString("SelectedChar");

            switch (character)
            {
                case "Tom":
                    displayedChar = (GameObject)Instantiate(displayTomPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    break;
                case "BigBombTom":
                    displayedChar = (GameObject)Instantiate(displayBigBombTomPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    break;
                case "Tommy":
                    displayedChar = (GameObject)Instantiate(displayTommmyPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    break;
                default:
                    break;
            }
        }

        if (!characterDisplayedCurrently)
        {
            switch (currChar)
            {
                case "Tom":
                    displayedChar = (GameObject)Instantiate(displayTomPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    characterDisplayedCurrently = true;
                    break;
                case "BigBombTom":
                    displayedChar = (GameObject)Instantiate(displayBigBombTomPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    characterDisplayedCurrently = true;
                    break;
                case "Tommy":
                    displayedChar = (GameObject)Instantiate(displayTommmyPrefab, transform.position, transform.rotation);
                    anim = displayedChar.GetComponent<Animator>();
                    characterDisplayedCurrently = true;
                    break;
                default:
                    break;
            }
        }
    }
}
