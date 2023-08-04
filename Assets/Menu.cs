using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        if(PlayerPrefs.GetString ("HIGHSCORENAME") != ""){
            highScoreText.text = "Current ranking player: " + PlayerPrefs.GetString("HIGHSCORENAME") + "\n" + "Score: " + PlayerPrefs.GetInt("HIGHSCORE");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Debug.Log ("Close Game");
        Application.Quit();
    }
}
