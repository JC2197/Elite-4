using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitDoor : MonoBehaviour
{

    private static bool closeEnough = false;
    public bool complete = false;

    public GameObject p;
    public GameObject m;

    public Vector2Int LeftDoorPosition;
    private static Vector3Int LPos;
    private static Vector3Int RPos;

    private static Vector3 DoorPos = new Vector3(0,0,0);


    private int zPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool checkOpenCondition() {
        Vector3Int playerPos = new Vector3Int(1000,1000,1000);
        float minDist = 10;
        float newDist = 10;
        foreach (Vector3Int i in doorFinder.leftDoor) {
            
            int index = doorFinder.leftDoor.IndexOf(i);
            Vector3Int newPosR = doorFinder.rightDoor[index];
            Vector3Int newPosL = i;
            newPosL.z = (int)p.transform.position.z;
            
            playerPos.x = (int) p.transform.position.x;
            playerPos.y = (int) p.transform.position.y;
            playerPos.z = (int) p.transform.position.z;
            
            Vector3 finPos = new Vector3((float) (newPosL.x+newPosR.x)/2, (float) (newPosL.y+newPosR.y)/2,p.transform.position.z);

            newDist = Vector3.Distance(finPos, playerPos);

        }
        if(newDist <= 3) {
            closeEnough = true;
        } else {
            closeEnough = false;
        }
        return closeEnough;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkOpenCondition()) {
            if(!complete) {
                doorFinder.Open();
                complete = true;
            }
        } else {
            if(complete) {
                doorFinder.Close();
                complete = false;
            }
        }
    }
}
