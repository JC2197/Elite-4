using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponScript : MonoBehaviour
{
    public Vector2 PointerPosition {get;set;}
    public float delay = 0.5f;
    private bool attackBlocked;
    PlayerScript player;
    public Animator weaponAnim;
    private bool attacking = false;
    private float timer = 0f;
    private float timeToAttack = 4f;
    void Start(){
        player = GetComponentInParent<PlayerScript>();
    }
    void Update()
    {
       
        if(Input.GetMouseButtonDown(0)){
            Attack();
        }else{
            GetSwordSpriteDirection();
        }

        if(attacking){
            timer += Time.deltaTime;
            if(timer >= timeToAttack){
                timer = 0;
                attacking = false;
            }
        }
    }
    public void Attack(){
        if (attackBlocked){
            return;
        }
        weaponAnim.SetTrigger("IsAttacking"+player.currDirection);
        attackBlocked = true;
        attacking = true;
        StartCoroutine(DelayAttack());
    }
      private IEnumerator DelayAttack(){
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    private void GetSwordSpriteDirection(){
        //if(attackBlocked != true){
            if(player.direction.y > 0){ //north..
                if(player.direction.x < 0){ //west
                    player.currDirection = "NORTHWEST";
                    transform.localPosition = new Vector3(0.02f,-0.028f,0.5f);
                    transform.localRotation = Quaternion.Euler(0,0,21.1808357f);
                    transform.localScale = new Vector3(0.8f,0.75f,1);
                }else if(player.direction.x > 0){ //east
                    player.currDirection = "NORTHEAST";
                    
                    transform.localPosition = new Vector3(0.044f,-0.045f,0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,-10.16f);
                    
                    transform.localScale = new Vector3(0.8f,0.75f,1);
                }else {//north
                    player.currDirection = "NORTH";
                    
                    transform.localPosition = new Vector3(0.06f,-0.015f,0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,9.56465244f);
                    
                    transform.localScale = new Vector3(0.6f,.75f,1);
                }

            }else if(player.direction.y < 0){
                if(player.direction.x < 0){ //west
                    player.currDirection = "SOUTHWEST";
                    
                    transform.localPosition = new Vector3(-0.034f,-0.056f,0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,19.511898f);
                    
                    transform.localScale = new Vector3(0.8f,0.75f,1);
                }else if (player.direction.x > 0){ //east;
                    player.currDirection = "SOUTHEAST";
                    
                    transform.localPosition = new Vector3(-0.046f,-0.044f,-0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,-19.5f);
                    
                    transform.localScale = new Vector3(0.8f,0.75f,1);
                }else { //south
                    player.currDirection = "SOUTH";
                    
                    transform.localPosition = new Vector3(-0.058f,-0.05f,-0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,352.273254f);
                    
                    transform.localScale = new Vector3(0.6f,0.796424985f,1f);
                }
            }else{
                if(player.direction.x < 0){ //west
                    player.currDirection = "WEST";
                    
                    transform.localPosition = new Vector3(-0.008f,-0.056f,0.5f);
                    
                    transform.localRotation = Quaternion.Euler(0,0,28.6478519f);
                    
                    transform.localScale = new Vector3(1,.75f,1);
                }else if(player.direction.x > 0){  //east
                    player.currDirection  = "EAST";
                    transform.localPosition = new Vector3(0.008f,-0.056f,-0.5f);;
                    transform.localRotation = Quaternion.Euler(0,0,-24.619f);
                    transform.localScale = new Vector3(1,.75f,1);
                }
                
            }
        
    }
}
