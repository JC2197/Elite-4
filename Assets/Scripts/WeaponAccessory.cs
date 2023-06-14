using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAccessory : MonoBehaviour
    {
    public Vector2 PointerPosition {get;set;}

    private void Start(){
 
    }
    private void Update(){
        Vector2 direction =  (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;

        }
    }
