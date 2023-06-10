using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
    {
    public Vector2 PointerPosition {get;set;}
    public Vector2 positionNorth {get;set;}
    public Vector2 positionSouth {get;set;}
    public PlayerScript characterScript;
    private void Start(){
        characterScript = GetComponentInParent<PlayerScript>();
        Vector3 positionNorth = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            transform.localPosition.z
            );
        Vector3 positionSouth = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            transform.localPosition.z
            );
    }
    private void Update(){
        
        if(characterScript.currDirection == "SOUTH"){
            transform.Translate(positionSouth);
            //transform.localPosition = positionSouth;
        }
        Vector2 direction =  (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;
        
        
        
        }
    }
