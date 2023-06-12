using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    PlayerScript player;
    void Start(){
        GetComponentInParent<PlayerScript>();
    }
    void Update()
    {
        GetSwordSpriteDirection();
    }
    private void GetSwordSpriteDirection(){
        if(player.direction.y > 0){ //north..
            if(player.direction.x < 0){ //west
                player.currDirection = "NORTHWEST";
                transform.localPosition = new Vector3(-0.0209999997f,0.200000003f,0.5f);
                transform.localRotation = Quaternion.Euler(0,0,21.1808357f);
                transform.localScale = new Vector3(0.8f,0.75f,1);
            }else if(player.direction.x > 0){ //east
                player.currDirection = "NORTHEAST";
                
                transform.localPosition = new Vector3(0.066f,0.206f,0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,-10.16f);
                
                transform.localScale = new Vector3(0.8f,0.75f,1);
            }else {//north
                player.currDirection = "NORTH";
                
                transform.localPosition = new Vector3(0.0526000001f,0.211999997f,0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,9.56465244f);
                
                transform.localScale = new Vector3(0.6f,.75f,1);
            }

        }else if(player.direction.y < 0){
            if(player.direction.x < 0){ //west
                player.currDirection = "SOUTHWEST";
                
                transform.localPosition = new Vector3(-0.0790000036f,0.189999998f,0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,19.511898f);
                
                transform.localScale = new Vector3(0.8f,0.75f,1);
            }else if (player.direction.x > 0){ //east;
                player.currDirection = "SOUTHEAST";
                
                transform.localPosition = new Vector3(-0.00200000009f,0.203999996f,-0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,-19.5f);
                
                transform.localScale = new Vector3(0.8f,0.75f,1);
            }else { //south
                player.currDirection = "SOUTH";
                
                transform.localPosition = new Vector3(-0.0480000004f,0.204999998f,-0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,352.273254f);
                
                transform.localScale = new Vector3(0.6f,0.796424985f,1f);
            }
        }else{
            if(player.direction.x < 0){ //west
                player.currDirection = "WEST";
                
                transform.localPosition = new Vector3(-0.0480000004f,0.202999994f,0.5f);
                
                transform.localRotation = Quaternion.Euler(0,0,28.6478519f);
                
                transform.localScale = new Vector3(1,.75f,1);
            }else if(player.direction.x > 0){  //east
                player.currDirection  = "EAST";
                transform.localPosition = new Vector3(0.0480000004f,0.202999994f,-0.5f);;
                transform.localRotation = Quaternion.Euler(0,0,-24.619f);
                transform.localScale = new Vector3(1,.75f,1);
            }
               
        }
    }
}
