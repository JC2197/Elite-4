using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject obj;
    public LayerMask enemyLayer;

    public GameObject player;
    public LayerMask playerLayer;

    private Transform closest;

    public bool checkClosest() 
    {    

        Transform child = getClosestCheckpoint();

        Debug.Log("CURRENT CHECKPOINT: " + child.name);

        Checkpoint childScript = child.GetComponent<Checkpoint>();
        Collider2D childCol = child.GetComponent<Collider2D>();

        return childScript.clearCheck(childCol, enemyLayer);

    }

    public Transform getPlayerTransform() 
    {
        return player.transform;
    }


    public Transform getClosestCheckpoint() 
    {
        float distance;
        float minDist = 100000f;
        foreach(Transform child in obj.transform) 
        {
            Checkpoint childScript = child.GetComponent<Checkpoint>();
            Transform origin = childScript.refPoint.transform;
            distance = Vector3.Distance(origin.position, player.transform.position);
            
            if(distance < minDist)
            {
                minDist = distance;
                closest = child;
            }
        }
        
        //Debug.Log(closest.gameObject.name);
        return closest;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        checkClosest();
    }
}
