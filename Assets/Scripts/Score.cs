using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    public Text scoreText; //Scoreboard text
    public Text highScore; //Saved data text

    public GameObject reloadObj;
    public GameObject spawnerObj;

    private static Score myScore; //Instance of Score script
    private Reload reload; //Reload script
    private Spawner spawner; //Spawner script

    private int gameScore; //Current game score
    private int level; //Current level
    private float deltaT; //Time till next level
    private float levelTime; //Start time of level
    private int health; //Amount of health player has

    private float fixedDeltaTime;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //Initial values
        level = 1;
        health = 5;
        deltaT = 10;
        gameScore = 0;
        levelTime = Time.time;
        fixedDeltaTime = Time.fixedDeltaTime;
        gameOver = false;

        //Get instances of needed scripts
        reload = Reload.Instance();
        spawner = Spawner.Instance();

        //Get saved data for high score and previous score
        highScore.text = "Previous: " + PlayerPrefs.GetInt("previousScore").ToString();
        highScore.text += "\nHigh Score: " + PlayerPrefs.GetInt("highScore").ToString();

        //Update text
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        //Increases level by 1 every deltaT seconds
        if (Time.time - levelTime >= deltaT)
        {
            levelTime = Time.time;
            level++;
            UpdateText();
        }
    }

    //Spawns a new projectile
    public void NewProj()
    {
        reload.newProj();
    }

    //Increases score by 1 and updates text
    public void IncreaseScore()
    {
        gameScore++;
        UpdateText();
    }

    //Returns current score
    public int GetScore()
    {
        return gameScore;
    }

    //Returns current level
    public int GetLevel()
    {
        return level;
    }

    //Lower health score and updates text
    public void TakeDamage()
    {
        health -= 1;
        UpdateText();
    }

    //Displays game info
    private void UpdateText()
    {
        string message;
        message = "Score: " + gameScore.ToString();
        message += "\nLevel: " + level.ToString();
        message += "\nLives: " + health.ToString();

        if(health <= 0)
        {
            message += GameOver();
        }

        scoreText.text = message;

    }

    private string GameOver()
    { 
        PlayerPrefs.SetInt("previousScore", gameScore);
        if (gameScore > PlayerPrefs.GetInt("highScore")) PlayerPrefs.SetInt("highScore", gameScore);
        PauseGame();
        gameOver = true;
        MovingItem.GameOver();
        return "\nGAME OVER";
    }

    //Returns an instance of the script
    public static Score Instance()
    {
        if (!myScore)
        {
            myScore = FindObjectOfType(typeof(Score)) as Score;
            if (!myScore)
                Debug.LogError("There needs to be one active Score script");
        }

        return myScore;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        MovingItem.PauseGame();
    }

    public void ResumeGame()
    {
        if (gameOver) return;
        Time.timeScale = 1;
        MovingItem.ResumeGame();
    }

    public void RestartGame()
    {
        GameOver();
        gameOver = false;
        gameScore = 0;
        level = 1;
        health = 5;
        levelTime = Time.time;
        ResumeGame();
        MovingItem.RestartGame();
        UpdateText();
        //Instantiate(spawnerObj, transform);
        //Instantiate(reloadObj, transform);
        //NewProj();
    }

    //private void OnGUI()
    //{
    //    string message = "";
    //    message += "X: " + scoreText.transform.position.x + "\n";
    //    message += "Y: " + scoreText.transform.position.y + "\n";
    //    message += "Z: " + scoreText.transform.position.z + "\n";

    //    GUI.Label(new Rect(300, 50, 120, 100), message);
    //}
}
