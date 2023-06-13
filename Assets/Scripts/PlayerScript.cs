using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D body;
    public Collider playerCollider;
    public SpriteRenderer spriteRenderer;
    public Health health;
    private Animator anim;
    public int maxHealth = 100;
    public int currentHealth; 
    
    public string currDirection = "NORTH";
    
    public bool isDead = false;
    public float walkSpeed;
    public float frameRate;
    // Start is called before the first frame update
    float idleTime;
    float timer;
    public Vector2 direction;
    private Vector2 pointerInput;
    private weaponScript weapon;
    

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private void Start(){
        weapon = GetComponentInChildren<weaponScript>();
        health = GetComponentInChildren<Health>();
        Spawn();
    }
    private void Awake(){
        body = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        GetDirection();
        if(health.isDead == false){ 
            //LINK UP WEAPON ACCESSORY WITH CURSOR
            Move();
            Animate();
        }else{
            Spawn();
        }
    }
    private void Move(){
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        if(Horizontal == 0 && Vertical == 0){
            body.velocity = new Vector2(0,0);
            anim.Play("IdleTree", 0, 0.01f);
            anim.SetBool("IsMoving", false);
        }else{
            direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            body.velocity = direction * walkSpeed * Time.fixedDeltaTime;
        }
    }
    private void Animate(){
        anim.SetFloat("MovementX", direction.x);
        anim.SetFloat("MovementY", direction.y);
    }
    private Vector2 GetPointerInput(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos); 
    }
    private void Spawn(){
        health.ResetHealth();
        player.transform.position = respawnPoint.transform.position;
        health.isDead = false;
    }
    private void GetDirection(){
        if(
            direction.y > 0){ //north..
            if(direction.x < 0){ //west
                currDirection = "NORTHWEST";
            }else if(direction.x > 0){ //east
                currDirection = "NORTHEAST";
            }else {//north   
                currDirection = "NORTH";
            }

        }else if(
            direction.y < 0){
            if(direction.x < 0){ //west                
                currDirection = "SOUTHWEST";
            }else if (direction.x > 0){ //east;
                currDirection = "SOUTHEAST";
            }else { //south   
                currDirection = "SOUTH";

            }
        }else{
            if(direction.x < 0){ //west               
                currDirection = "WEST";
            }else if(direction.x > 0){  //east
                currDirection  = "EAST";

            }        
        }
    }
}
