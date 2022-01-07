using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //Tower prefab that gets placed
    public GameObject towerPrefab;
    //this string is used so we can check what we have hit
    string hitTag;
    //using the ui manager to check if there is lives left and money
    UiManager UI;
    //cost of a base tower
    int towerCost = 50;

    private void Awake()
    {
        UI = GameObject.FindObjectOfType<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if we are touching the screen
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && UI.lives>0)
        {
            Debug.Log("touch detected");
            //We send a ray from the camera to where we touch on the screen.
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //get the tag of what we hit
                hitTag = hit.transform.tag;
                switch (hitTag)
                {
                    //if the tag is the tower location we can place a tower
                    case ("TowerLocation"):
                        if(UI.money >= towerCost)
                        {
                            //place tower and update the money
                            Debug.Log("placing tower");
                            UI.ChangeMoney(-towerCost);
                            Instantiate(towerPrefab,hit.transform.position,hit.transform.rotation);
                            //removing the placement model
                            Destroy(hit.transform.gameObject);
                        }
                        else
                        {
                            //Not enough money for a tower
                            Debug.Log("not enough money");
                        }
                        
                        break;
                }
            }
        }
    }
}
