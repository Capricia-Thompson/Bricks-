using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int level = 1;
    public int lives = 5;
    public int score = 0;
    public int brickAmount;

    public Text livesText;
    public Text scoreText;
    public Text highScoreTextLost;
    public Text highScoreTextWon;
    public InputField nameImputWon;
    public InputField nameImputLost;

    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public GameObject canvas;
    public GameObject EventSystem;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(EventSystem);
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        gameOver = false;
    }

    private void NewGame()
    {
        this.lives=5;
        this.score=0;
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level "+ level);
    }

    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        brickAmount = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void LifeLost()
    {
        lives --;
        livesText.text = "Lives: " + lives;

        if(lives <= 0){
            lives=0;
            GameOver();
        }
    }

    public void BrickHit(Bricks brick)
    {
        this.score += brick.points;
        scoreText.text = "Score: " + score;
    }

    public void brickDestroyed()
    {
        brickAmount--;
        if(Cleared()){
            if(this.level==5){
                gameWonPanel.SetActive (true);
                HighScore();
                return;
            }
            LoadLevel(this.level +1);
        }

    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive (true);
        HighScore();
    }

    private bool Cleared()
    {
        if (brickAmount == 0){
            return true;
        }
        return false;
    }

    public void Restart()
    {
        gameOverPanel.SetActive (false);
        gameWonPanel.SetActive (false);
        NewGame();
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    public void HighScore()
    {
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if(score>highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreTextLost.text ="New High Score: " + "\n" + "Enter your name below!";
            nameImputLost.gameObject.SetActive(true);
            highScoreTextWon.text ="New High Score: " + score + "\n" + "Nice Job!";
            nameImputWon.gameObject.SetActive(true);

        }else{
            highScoreTextLost.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s high score was " + highScore + "\n" + "Keep trying!";
            highScoreTextWon.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s high score was " + highScore + "\n" + "Keep trying!";
        }

    }

    public void NewHighScore()
    {
        if(gameOver){
            string highScoreName = nameImputLost.text;
            PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
            nameImputLost.gameObject.SetActive(false);
            highScoreTextLost.text = "Congrats " + highScoreName + "\n" + "New high score is " + score;
        }else{
            string highScoreName = nameImputWon.text;
            PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
            nameImputWon.gameObject.SetActive(false);
            highScoreTextWon.text = "Congrats " + highScoreName + "\n" + "New high score is " + score;
    }
    }

    public void Quit()
    {   
        gameOverPanel.gameObject.SetActive(false);
        gameWonPanel.gameObject.SetActive(false);
        Destroy(this.gameObject);
        Destroy(canvas);
        Destroy(EventSystem);
        SceneManager.LoadScene("Start");
    }
}
