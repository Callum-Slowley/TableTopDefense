using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //enemy prefab
    public GameObject enemy;
    //Wave cooldown and current time used for spawning
    float waveCoolDown = 5f;
    float currentTime;
    //where the enemies will spawn in
    public Transform spawnPoint;
    //List of the eneimes 
    Enemy[] enemies;
    //Number of what wave we are on so we can increase the difficulty
    public int waveIndex = 0;
    //using the ui manager to check if there is lives left
    UiManager UI;


    private void Awake()
    {
        UI = GameObject.FindObjectOfType<UiManager>();
        UI.ChangeWave(waveIndex);
    }
    // Update is called once per frame
    void Update()
    {
        //creating a list of all the enemys in the scene
        enemies = GameObject.FindObjectsOfType<Enemy>();
        //If the wavecooldown has eneded and there are no enemies left spawn the next wave
        if (currentTime >= waveCoolDown && enemies.Length<=0 && UI.lives>0)
        {
            //Starting to spawn the wave
            StartCoroutine(SpawnWave());
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }

    //Ienumerator i used so we can use wait for secconds so the wave doesnt spawn all on itself

    IEnumerator SpawnWave()
    {
        //increase the wave then spawn the wave
        waveIndex++;
        UI.ChangeWave(waveIndex);
        for (int i = 0; i < waveIndex; i++)
        {
            //creating a new enemy on the spawnpoint then waiting half a seccond to spawn the next enemy
            Instantiate(enemy, spawnPoint.transform.position,spawnPoint.transform.rotation);
            //Waits 0.5 secconds intill the next spawn
            yield return new WaitForSeconds(0.1f);
        }
    }
}
