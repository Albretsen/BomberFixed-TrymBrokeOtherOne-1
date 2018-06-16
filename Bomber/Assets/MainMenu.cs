﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Dropdown dropdown;
    public Scrollbar scroll;
    public Text preview;

    Scene scene;
    int dropDownSelected;
	// Use this for initialization
	void Start () {
        //FIND CURRENT SCENE
        scene = SceneManager.GetActiveScene();

        if (scene.name == "DONT DO THIS")
        {
            switch (PlayerPrefs.GetString("SelectedChar"))
            {
                case "Tom":
                    dropdown.value = 0;
                    break;
                case "BigBombTom":
                    dropdown.value = 1;
                    break;
                case "Tommy":
                    dropdown.value = 2;
                    break;
                default:
                    Debug.Log("CURR CHAR NOT FOUND IN DROPDOWN LIST");
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (scene.name == "Character")
        {
            preview.text = PlayerPrefs.GetString("SelectedChar");
        }
	}

    public void Dropdown()
    {
        dropDownSelected = dropdown.value;
        switch (dropDownSelected) {
            case 0:
                PlayerPrefs.SetString("SelectedChar", "Tom");
                break;
            case 1:
                PlayerPrefs.SetString("SelectedChar", "BigBombTom");
                break;
            case 2:
                PlayerPrefs.SetString("SelectedChar", "Tommy");
                break;
            default:
                Debug.Log("CHARACTER NOT FOUND!!");
                break;
        }
    }

    public void Scroll()
    {
        Debug.Log(scroll.value);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
        Debug.Log(PlayerPrefs.GetString("SelectedChar"));
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Character()
    {
        SceneManager.LoadScene("Character");
    }

    //CHARACTER SELECTOR
    public void Tom()
    {
        PlayerPrefs.SetString("SelectedChar", "Tom");
        preview.text = "Tom";
    }
    public void BigBombTom()
    {
        PlayerPrefs.SetString("SelectedChar", "BigBombTom");
        preview.text = "BigBombTom";
    }
    public void Tommy()
    {
        PlayerPrefs.SetString("SelectedChar", "Tommy");
        preview.text = "Tommy";
    }
}

