using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    //Always start on 100 lives and money
    public int lives = 100;
    public int money = 100;
    //Used to change the UI
    public Text livesText;
    public Text moneyText;
    public Text wavesText;
    //gamover
    public GameObject gameOverObj;

    //when the program starts the UI is updated for the first time
    private void Awake()
    {
        livesText.text = "Lives: " + lives.ToString();
        moneyText.text = "Money: " + money.ToString();
    }

    //changes the lives amount
    public void changeLives(int amount)
    {
        lives += amount;
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            gameOver();
        }
    }
    //changes the money amount
    public void ChangeMoney(int amount)
    {
        money += amount;
        moneyText.text = "Money: " + money.ToString();
    }

    public void ChangeWave(int amount)
    {
        wavesText.text = "Waves: " + amount;
    }

    void gameOver()
    {
        gameOverObj.SetActive(true);
    }
    
    public void Restart()
    {
        //reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
