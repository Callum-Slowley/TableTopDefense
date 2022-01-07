using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //setting the base speed 
    public float speed = 15f;
    //target that will be accessed in the tower script
    public Transform target;
    //dmg the bullet can do 
    public float dmg = 1f;
    //direction to enemy
    Vector3 directionToEnemy;
    //distance the bullet will move this frame, this is done so we dont over shoot the target. 
    float distanceThisFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if there is a target move towards it 
        if (target != null)
        {
        //Getting the direction to the next enemy
        directionToEnemy = target.position - this.transform.localPosition;
        //Getting the distance we move this frame
        distanceThisFrame = speed * Time.deltaTime;
        //if the distance to the enemy is less than what we are going to move we have reached the enemy
            if (directionToEnemy.magnitude <= distanceThisFrame)
            {
             //once we reach the enemy we run the hit function
             BulletHit();
            }
            //otherwise we move
            else
            {
                //we move along the direction to the enemy by the distance we need to move this frame. Done in world space
                transform.Translate(directionToEnemy.normalized * distanceThisFrame, Space.World);
             //then we change our rotation to look at the enemy
             this.transform.rotation = Quaternion.LookRotation(directionToEnemy);
            }
        }
        //if target is already gone destroy this object
        else
        {
            Destroy(this.gameObject);
        }

    }
    void BulletHit()
    {
        target.GetComponent<Enemy>().HealthUpdate(-dmg);
        Destroy(this.gameObject);
    }
}
