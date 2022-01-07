using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    //speed of the enemy
    public float speed = 5f;
    //health of the enemy
    public float health = 1f;
    //money gained by the player after enemy is killed
    public int moneyValue = 1;
    //Damage of the enemy
    public int dmg=1;
    //Getting the path node manager which will have the path as children
    GameObject pathNodeManager;
    // next node to move to 
    Transform targetNode;
    //node index starting at 0 as we are at the start
    int nodeIndex = 0;
    //ui manager used to change score
    UiManager UI;

    //direction to next node
    Vector3 directionToNode;
    //distance the enemy will move this frame, this is done so we dont over shoot the target. This should fix the problem from the prototype
    float distanceThisFrame;
    // Start is called before the first frame update
    void Start()
    {
        //First we find the pathNodeManager in the scene
        pathNodeManager = GameObject.Find("pathNodeManager");
        //finds the ui manager
        UI = GameObject.FindObjectOfType<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if we dont have a node get the next node
        if(targetNode == null)
        {
            GetNextNode();
            //if after getting the next node is there none we have reached the goal
            if(targetNode == null)
            {
                ReachedGoal();
                //once we reach the goal we return so theres no errors
                return;
            }
        }
        //Getting the direction to the next node
        directionToNode = targetNode.position - this.transform.localPosition;
        //Getting the distance we move this frame
        distanceThisFrame = speed * Time.deltaTime;
        //if the distance to the node is less than what we are going to move we have reached the node
        if(directionToNode.magnitude <= distanceThisFrame)
        {
            //we have reached the node so it is set to null allowing the node to change next update
            targetNode = null;
        }
        //otherwise we move
        else
        {
            //we move along the direction to the node by the distance we need to move this frame. Done in world space
            transform.Translate(directionToNode.normalized * distanceThisFrame,Space.World);
            //then we change our rotation to look at the next node
            this.transform.rotation = Quaternion.LookRotation(directionToNode);
        }
    }
    //function that will ittereate over the nodes in the path for the enemy
    void GetNextNode()
    {
        //if we havent reached the last node
        if(nodeIndex < pathNodeManager.transform.childCount)
        {
            //We set the node as the node in the index
            targetNode = pathNodeManager.transform.GetChild(nodeIndex);
            //we step over to the next node
            nodeIndex++;
        }
    }
    //function for when we reach the goal
    void ReachedGoal()
    {
        //reduce the player lives
        UI.changeLives(-dmg);
        Destroy(this.gameObject);
    }
    //updating the health
    public void HealthUpdate( float changeBy)
    {
        health += changeBy;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        //increases the player money
        UI.ChangeMoney(moneyValue);
        Destroy(this.gameObject);
    }
}
