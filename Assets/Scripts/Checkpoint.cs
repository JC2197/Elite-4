using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour

{
    public GameObject refPoint;
    private static bool clear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setClear(bool val) 
    {
        clear = val;
    }
    public bool getClear() 
    {
        return clear;
    }

    public bool clearCheck(Collider2D c, LayerMask x) 
    {
        if(c.IsTouchingLayers(x)) {
            //Debug.Log(name + ": ENEMY SPOTTED");
            setClear(false);
        } else {
            //Debug.Log(name + ": NO ENEMIES");
            setClear(true);
        }
        return getClear();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
