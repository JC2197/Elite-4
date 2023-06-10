using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D body;
    public Collider playerCollider;
    public int maxHealth = 100;
    public int currentHealth; 
    public HealthBar healthbar;
    public SpriteRenderer spriteRenderer;
    public string currDirection = "NORTH";
    public List<Sprite> nSprites;
    public List<Sprite> nwSprites;
    public List<Sprite> wSprites;
    public List<Sprite> sSprites;
    public List<Sprite> swSprites;
    public bool isDead = false;
    public float walkSpeed;
    public float frameRate;
    // Start is called before the first frame update
    float idleTime;
    float timer;
    Vector2 direction;
    private Vector2 pointerInput;
    private WeaponParent weaponParent;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private void Start(){
        weaponParent = GetComponentInChildren<WeaponParent>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false){
            pointerInput = GetPointerInput();
            weaponParent.PointerPosition = pointerInput;
            direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            body.velocity = direction * walkSpeed;
            takeDamage();
            setSprite();
        }else{
            direction.x = 0;
            direction.y = 0;
            body.velocity = direction * walkSpeed;
            Spawn();
        }
    }
    private Vector2 GetPointerInput(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
        
    }
    void takeDamage(){
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
            
        }
    }
    void TakeDamage(int damage){
        currentHealth -=damage;
        healthbar.SetHealth(currentHealth);
        if(currentHealth==0){
            isDead = true;
        }
    }
    private void Spawn(){
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        player.transform.position = respawnPoint.transform.position;
        Invoke(nameof(Start), 2);
    }
    void setSprite(){
        List<Sprite> directionSprites = GetSpriteDirection();

        if(directionSprites != null){

            float playTime = Time.time - idleTime; //time since we started walking
            int totalFrames = (int)(playTime * frameRate); //total frames
            int frame = totalFrames % directionSprites.Count; //current frame
            spriteRenderer.sprite = directionSprites[frame];
        }else{
            idleTime = Time.time;
        }
    }
    void HandleSpriteFlip(){
        if(!spriteRenderer.flipX && direction.x > 0){
            spriteRenderer.flipX = true;
            
        }else if(spriteRenderer.flipX && direction.x < 0){
            spriteRenderer.flipX = false;
        }
    }
    List<Sprite> GetSpriteDirection(){
        HandleSpriteFlip();
        List<Sprite> selectedSprites = null;

        if(direction.y > 0){ //north
            if(Mathf.Abs(direction.x) > 0){ //east/west
                selectedSprites = nwSprites;
                currDirection = "NORTHWEST";
            }else{//neutral X
                selectedSprites = nSprites;
                currDirection = "NORTH";
            }

        }else if(direction.y < 0){
            if(Mathf.Abs(direction.x) > 0){
                selectedSprites = swSprites;
                currDirection = "SOUTHWEST";
            }else{
                selectedSprites = sSprites;
                currDirection = "SOUTH";
            }

        }else{
            if(Mathf.Abs(direction.x) > 0){
                selectedSprites = wSprites;
                currDirection = "WEST";
            }
        }
        if(spriteRenderer.flipX == true){
            if(currDirection == "NORTHWEST"){
                    currDirection = "NORTHEAST";
                }
            if(currDirection == "WEST"){
                    currDirection  = "EAST";
                } 
            if (currDirection == "SOUTHWEST"){
                    currDirection = "SOUTHEAST";
                }
        }
        return selectedSprites; 
    }
}
