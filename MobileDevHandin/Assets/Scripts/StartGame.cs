using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject game;
    public GameObject overLay;

    public void startGame()
    {
        game.SetActive(true);
        Destroy(overLay);
    }
}
