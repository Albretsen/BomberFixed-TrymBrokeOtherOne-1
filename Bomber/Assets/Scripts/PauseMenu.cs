using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenu;
    public GameObject winScreen;
    public GameObject buttons;
    public Text bestTime;
    public Text score;
    public AudioMixer audioMixer;
    public Slider mainSlider;


    float volume1;

    float timeSpent;
    float levelMaxTime;
    float levelScore;
    float timeStarted;
    float highScore;

    //FIREBASE TEST
    string userId = "ASGEIRTEST";
    public string uid;

    string scene;
    bool scoreSet;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        scoreSet = false;
        timeStarted = Time.time;

        //AUDIO
        mainSlider.value = PlayerPrefs.GetFloat("Volume");
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /*public void WinScreen(bool won)
    {
        scene = SceneManager.GetActiveScene().name;
        buttons.SetActive(false);
        winScreen.SetActive(true);
        timeSpent = Time.time - timeStarted;
        float highestScore = 0;

        //HIGHSCORE MANAGING
        switch (scene)
        {
            case "TestScene2":
                //NOT NEW HIGHSCORE
                if (PlayerPrefs.GetInt("Precise") != 0)
                {
                    timeSpent = (float)System.Math.Round(timeSpent, 5);
                    highestScore = PlayerPrefs.GetFloat("TestScene2");
                    highestScore = (float)System.Math.Round(highestScore, 5);
                }
                else
                {
                    timeSpent = (float)System.Math.Round(timeSpent, 2);
                    highestScore = PlayerPrefs.GetFloat("TestScene2");
                    highestScore = (float)System.Math.Round(highestScore, 2);
                }

                //SETTING THE NORMAL SCORE
                if (won)
                {
                    if (!scoreSet)
                    {
                        score.text = "Time: " + timeSpent;
                        scoreSet = true;
                    }

                    //NEW HIGH SCORE
                    if (timeSpent < PlayerPrefs.GetFloat("TestScene2", 99999))
                    {
                        PlayerPrefs.SetFloat("TestScene2", timeSpent);

                        if (PlayerPrefs.GetInt("Precise") != 0)
                        {
                            timeSpent = (float)System.Math.Round(timeSpent, 5);
                            highestScore = PlayerPrefs.GetFloat("TestScene2");
                            highestScore = (float)System.Math.Round(highestScore, 5);
                        }
                        else
                        {
                            timeSpent = (float)System.Math.Round(timeSpent, 2);
                            highestScore = PlayerPrefs.GetFloat("TestScene2");
                            highestScore = (float)System.Math.Round(highestScore, 2);
                        }
                    }

                }

                //IF LEVEL WAS NOT COMPLETED, SET NO TIME
                else
                {
                    score.text = "Time: NO TIME";
                }
                bestTime.text = "Best time: " + highestScore;
                break;
            case "TestScene3":
                break;
        }
    }*/

    public void WinScreen(bool won)
    {
        scene = SceneManager.GetActiveScene().name;
        buttons.SetActive(false);
        winScreen.SetActive(true);
        timeSpent = Time.time - timeStarted;
        float highestScore = 0;

        //NOT NEW HIGHSCORE
        if (PlayerPrefs.GetInt("Precise") != 0)
        {
            timeSpent = (float)System.Math.Round(timeSpent, 5);
            highestScore = PlayerPrefs.GetFloat(scene);
            highestScore = (float)System.Math.Round(highestScore, 5);
        }
        else
        {
            timeSpent = (float)System.Math.Round(timeSpent, 2);
            highestScore = PlayerPrefs.GetFloat(scene);
            highestScore = (float)System.Math.Round(highestScore, 2);
        }

        //SETTING THE NORMAL SCORE
        if (won)
        {
            if (!scoreSet)
            {
                score.text = "Time: " + timeSpent;
                scoreSet = true;
            }

            //NEW HIGH SCORE
            if (timeSpent < PlayerPrefs.GetFloat(scene, 99999))
            {
                PlayerPrefs.SetFloat(scene, timeSpent);

                if (PlayerPrefs.GetInt("Precise") != 0)
                {
                    timeSpent = (float)System.Math.Round(timeSpent, 5);
                    highestScore = PlayerPrefs.GetFloat(scene);
                    highestScore = (float)System.Math.Round(highestScore, 5);
                }
                else
                {
                    timeSpent = (float)System.Math.Round(timeSpent, 2);
                    highestScore = PlayerPrefs.GetFloat(scene);
                    highestScore = (float)System.Math.Round(highestScore, 2);
                }
            }

        }

        //IF LEVEL WAS NOT COMPLETED, SET NO TIME
        else
        {
            score.text = "Time: NO TIME";
        }
        bestTime.text = "Best time: " + highestScore;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void SetVolume (float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
    }

    public void Reset()
    {
        PlayerPrefs.SetFloat("Volume", 0);
        PlayerPrefs.Save();
        mainSlider.value = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
    }

    public void Back()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        optionsMenu.SetActive(false);
        buttons.SetActive(true);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        buttons.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        buttons.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}


