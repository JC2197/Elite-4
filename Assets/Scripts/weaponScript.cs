using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponScript : MonoBehaviour
{
    public Vector2 PointerPosition {get;set;}
    public float delay = 0.5f;
    private bool attackBlocked;
    PlayerScript player;
    private bool attacking = false;
    private float timer = 0f;
    private float timeToAttack = .25f;
    void Start(){
        
        player = GetComponentInParent<PlayerScript>();
    }
    void Update()
    {   
        if(Input.GetMouseButtonDown(0)){
            Attack();
        }else{
            if(!attacking){
                SwordSpriteAnimator();
            }
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
        if (attacking){
            return;
        }if(player.currDirection == "NORTHWEST"){
            transform.localPosition = new Vector3(-0.03f,0f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,42f);
            transform.localScale = new Vector3(1f,1f,1);
        }
        if(player.currDirection == "NORTHEAST"){
            transform.localPosition = new Vector3(0.061f,0.016f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,-45f);
            transform.localScale = new Vector3(1f,1f,1);
        }
        if(player.currDirection == "NORTH"){
            transform.localPosition = new Vector3(0.02f,0.05f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,-356f);            
            transform.localScale = new Vector3(1f,1f,1);
        }
        if(player.currDirection == "SOUTHWEST"){
            transform.localPosition = new Vector3(-0.053f,-0.017f,0.5f);               
            transform.localRotation = Quaternion.Euler(0,0,130f);             
            transform.localScale = new Vector3(1f,1f,1);
        }
        if(player.currDirection == "SOUTHEAST"){
            transform.localPosition = new Vector3(-0.038f,-0.024f,-0.5f);     
            transform.localRotation = Quaternion.Euler(0,0,-134f);              
            transform.localScale = new Vector3(1f,1f,1);
        }
        if(player.currDirection == "SOUTH"){
            transform.localPosition = new Vector3(-0.065f,-0.031f,-0.5f);             
            transform.localRotation = Quaternion.Euler(0,0,-178f);         
            transform.localScale = new Vector3(1f,1f,1f);
        }
        if(player.currDirection == "WEST"){
            transform.localPosition = new Vector3(-0.022f,0f,0.5f);    
            transform.localRotation = Quaternion.Euler(0,0,90.5f);     
            transform.localScale = new Vector3(1,1f,1);
        }
        if(player.currDirection == "EAST"){
            transform.localPosition = new Vector3(0.049f,-0.018f,-0.5f);;
                transform.localRotation = Quaternion.Euler(0,0,-88f);
                transform.localScale = new Vector3(1,1f,1);
        }
        attacking = true;
        StartCoroutine(DelayAttack());
    }
      private IEnumerator DelayAttack(){
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    private void SwordSpriteAnimator(){
        if(player.currDirection == "NORTHWEST"){
            transform.localPosition = new Vector3(0.02f,-0.028f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,21.1808357f);
            transform.localScale = new Vector3(0.8f,0.75f,1);
        }
        if(player.currDirection == "NORTHEAST"){
            transform.localPosition = new Vector3(0.044f,-0.045f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,-10.16f);
            transform.localScale = new Vector3(0.8f,0.75f,1);
        }
        if(player.currDirection == "NORTH"){
            transform.localPosition = new Vector3(0.06f,-0.015f,0.5f);
            transform.localRotation = Quaternion.Euler(0,0,9.56465244f);            
            transform.localScale = new Vector3(0.6f,.75f,1);
        }
        if(player.currDirection == "SOUTHWEST"){
            transform.localPosition = new Vector3(-0.034f,-0.056f,0.5f);               
            transform.localRotation = Quaternion.Euler(0,0,19.511898f);             
            transform.localScale = new Vector3(0.8f,0.75f,1);
        }
        if(player.currDirection == "SOUTHEAST"){
            transform.localPosition = new Vector3(-0.046f,-0.044f,-0.5f);     
            transform.localRotation = Quaternion.Euler(0,0,-19.5f);              
            transform.localScale = new Vector3(0.8f,0.75f,1);
        }
        if(player.currDirection == "SOUTH"){
            transform.localPosition = new Vector3(-0.058f,-0.05f,-0.5f);             
            transform.localRotation = Quaternion.Euler(0,0,352.273254f);         
            transform.localScale = new Vector3(0.6f,0.796424985f,1f);
        }
        if(player.currDirection == "WEST"){
            transform.localPosition = new Vector3(-0.008f,-0.056f,0.5f);    
            transform.localRotation = Quaternion.Euler(0,0,28.6478519f);     
            transform.localScale = new Vector3(1,.75f,1);
        }
        if(player.currDirection == "EAST"){
            transform.localPosition = new Vector3(0.008f,-0.056f,-0.5f);;
                transform.localRotation = Quaternion.Euler(0,0,-24.619f);
                transform.localScale = new Vector3(1,.75f,1);
        }
        
    }
}
