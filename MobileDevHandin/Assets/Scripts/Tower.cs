using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //distance between tower and closest enemy.
    float distance;
    //distance to the current enemy in the array
    float enemyDistance;
    //direction is used to rotate the turret
    Vector3 directionToEnemy;
    //Turret top, rotated towards the cloest enemy
    public Transform turretTop;
    //the range of the tower
    float range =5f;
    //bullet prefab
    public GameObject bullet;
    //shooting cooldown
    float cooldown = .5f;
    //curret time waited
    float currentTime=0f;
    //array of enemys in the scene 
    Enemy[] enemies;
    //used for the nearest enemy
    Enemy nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //we get a list of all of the current enemys in the scene.(if something has an enemy script its added)
        enemies = GameObject.FindObjectsOfType<Enemy>();
        //we dont know what the nearest enemy is 
        nearestEnemy = null;
        //We make the distance really large so we can itterate through the array
        distance = Mathf.Infinity;
        //for each loop used to find the nearest enemy
        foreach(Enemy e in enemies)
        {
            //Calculating the distance to the current enemy in the array
            enemyDistance = Vector3.Distance(this.transform.position, e.transform.position);
            //if a nearest enemy hasnt been set or the current distance is greater the last enemy distance
            if(nearestEnemy == null ||distance> enemyDistance)
            {
                //the new enemy is the closest
                nearestEnemy = e;
                //and the distance is set to the current distance
                distance = enemyDistance;
            }
        }
        //if there are no enemys on the map we return out
        if(nearestEnemy == null)
        {
            Debug.Log("no enemies");
            return;
        }

        //setting the direction to the closest enemy
        directionToEnemy = nearestEnemy.transform.position - this.transform.position;
        //making the turret look at the enemy in the y
        turretTop.rotation = Quaternion.Euler(0, (Quaternion.LookRotation(directionToEnemy).eulerAngles.y), 0);

        //shooting 
        //increasing the time waited
        currentTime += Time.deltaTime;
        //we shoot if its within range and the the cooldown is complete
        if(currentTime >= cooldown && directionToEnemy.magnitude <= range)
        {
            currentTime = 0f;
            Shoot(nearestEnemy);
        }
    }

    void Shoot(Enemy target)
    {
        //shooting a bullet form the turret
        GameObject firedBullet = (GameObject)Instantiate(bullet, turretTop.transform.position, this.transform.rotation);
        //getting the bullets component so we can set the target
        Bullet fB =firedBullet.GetComponent<Bullet>();
        //setting the bullets target
        fB.target = target.transform; 
    }
}
