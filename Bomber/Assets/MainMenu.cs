using System.Collections;
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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        scene = SceneManager.GetActiveScene();

        /*if (scene.name == "DONT DO THIS")
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
        }*/

        if(PlayerPrefs.GetString("SelectedChar") != "Tom" && PlayerPrefs.GetString("SelectedChar") != "Tommy" && PlayerPrefs.GetString("SelectedChar") != "Tom2X")
        {
            PlayerPrefs.SetString("SelectedChar", "Tom");
        }

        //FIREBASE SETUP
    }
	
	// Update is called once per frame
	void Update () {
        if (scene.name == "Character")
        {
            //preview.text = PlayerPrefs.GetString("SelectedChar");
        }
	}

    /*public void Dropdown()
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
    }*/

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Options()
    {
        SceneManager.LoadScene(4);
    }

    public void Precise()
    {
        if(PlayerPrefs.GetInt("Precise") == 0)
        {
            PlayerPrefs.SetInt("Precise",1);
        }
        else
        {
            PlayerPrefs.SetInt("Precise", 0);
        }
    }

    public void Scroll()
    {
        Debug.Log(scroll.value);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Character()
    {
        SceneManager.LoadScene(3);
    }

    //CHARACTER SELECTOR
    public void Tom()
    {
        PlayerPrefs.SetString("SelectedChar", "Tom");
    }
    public void BigBombTom()
    {
        PlayerPrefs.SetString("SelectedChar", "BigBombTom");
    }
    public void Tommy()
    {
        PlayerPrefs.SetString("SelectedChar", "Tommy");
    }


}


