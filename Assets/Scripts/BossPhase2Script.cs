using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Script : MonoBehaviour
{
    private float waitingTime;
    public float moveSpeed;
    public float damage;
    public SpriteRenderer spriteRenderer;
    public Health health;
    Rigidbody2D rb;
    Transform target;
    Vector3 self;
    private Canvas canvas;
    private Collider2D coll;
    public float aggroRange = 10f;
    public Vector2 direction;
    string currDirection = "SOUTH";
    private bool hasTarget = false;
    public float distanceToTarget;
    BossFireBulletScript2 castScript;
    public List<Sprite> walkN;
    public List<Sprite> walkNE;
    public List<Sprite> walkE;
    public List<Sprite> walkSE;
    public List<Sprite> walkS;
    public List<Sprite> walkSW;
    public List<Sprite> walkW;
    public List<Sprite> walkNW;
    public List<Sprite> idleN;
    public List<Sprite> idleNE;
    public List<Sprite> idleE;
    public List<Sprite> idleSE;
    public List<Sprite> idleS;
    public List<Sprite> idleSW;
    public List<Sprite> idleW;
    public List<Sprite> idleNW;
    public List<Sprite> attackN;
    public List<Sprite> attackNE;
    public List<Sprite> attackE;
    public List<Sprite> attackSE;
    public List<Sprite> attackS;
    public List<Sprite> attackSW;
    public List<Sprite> attackW;
    public List<Sprite> attackNW;
    public List<Sprite> deathSprites;
    private float idleTime;
    public float frameRate;
    public AudioSource EnemyTakeDamageSFX;
    bool hasFired = false;
    bool isMoving = true;
    bool isAttacking = false;
    bool isDying = false;
    bool casting = false;
    private float shotCooldown;
    public float startShotCooldown = 2f;
    // Start is called before the first frame update
    void Start()
    {
        castScript = GetComponent<BossFireBulletScript2>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
        GameObject player = GameObject.Find("Character");
        EnemyTakeDamageSFX = GetComponent<AudioSource>();
        target = player.transform;
        self = transform.position;
        distanceToTarget = Vector2.Distance(self, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        List<Sprite> actions;
        if(health.isDead){
            frameRate = 12;
            isDying = true;
            actions = Dying();
                if(actions != null)
                {
                    float playTime = Time.time - waitingTime;
                    int frame = (int)((playTime * frameRate) % actions.Count);
                    spriteRenderer.sprite = actions[frame];
                }
            canvas.enabled = false;
            StartCoroutine(dyingWait());  
        }else{
            waitingTime = Time.time;
            if(!hasTarget){
                GameObject player = GameObject.Find("Character");
                if(distanceToTarget < aggroRange){
                    hasTarget = true;
                }
                hasTarget = true;
                self = transform.position;
                target = player.transform;
                
            }
            distanceToTarget = Vector3.Distance(self, target.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y*.001f));
            GetDirection();
            HandleSpriteFlip();
            if(target && distanceToTarget > 5f)
            {
                Vector2 dir = (target.position - transform.position).normalized;
                direction = dir;
                rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
                actions = Walking();
                if(actions != null)
                {
                    float playTime = Time.time - idleTime;
                    int frame = (int)((playTime * frameRate) % actions.Count);
                    spriteRenderer.sprite = actions[frame];
                } else {
                idleTime = Time.time;
                }
            }else if (target && distanceToTarget <=5f){
                Vector2 dir = (target.position - transform.position).normalized;
                direction = dir;
                rb.velocity = new Vector2(direction.x, direction.y) * 0;
                if(hasFired){
                    if(casting){
                        actions = Attacking();
                    }else{
                        actions = Idle();
                    }
                    if(actions != null)
                    {
                        float playTime = Time.time - idleTime;
                        int frame = (int)((playTime * frameRate) % actions.Count);
                        spriteRenderer.sprite = actions[frame];
                    } else {
                    idleTime = Time.time;
                    }
                    shotCooldown -= Time.deltaTime;
                    if(shotCooldown <= 0)
                        hasFired = false;
                }else{
                    
                    actions = Attacking();
                    StartCoroutine(castTime());
                    if(actions != null)
                    {
                        float playTime = Time.time - idleTime;
                        int frame = (int)((playTime * frameRate) % actions.Count);
                        spriteRenderer.sprite = actions[frame];
                    } else {
                    idleTime = Time.time;
                    }
                    castScript.Cast();
                    hasFired = true;
                    shotCooldown = startShotCooldown;
                }
            }
        }
    }
    
    void HandleSpriteFlip(){
        if(direction.x > 0){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (!invincible)
        //{
            if (other.gameObject.CompareTag("Weapon"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if (player != null)
                {
                    //EnemyTakeDamageSFX.Play();
                    health.TakeDamage((int)player.damage);
                }
            }
        //}
    }
    List<Sprite> Walking(){
        frameRate = 4;
        List<Sprite> selectedSprites = null;
        switch(currDirection)
        {
            case "NORTH":
                selectedSprites = walkN;
                break;
            case "NORTHEAST":
                selectedSprites = walkNE;
                break;
            case "EAST":
                selectedSprites = walkE;
                break;
            case "SOUTHEAST":
                selectedSprites = walkSE;
                break;
            case "SOUTH":
                selectedSprites = walkS;
                break;
            case "SOUTHWEST":
                selectedSprites = walkSW;
                break;
            case "WEST":
                selectedSprites = walkW;
                break;
            case "NORTHWEST":
                selectedSprites = walkNW;
                break;
        }
        return selectedSprites;
    }
    List<Sprite> Idle(){
        frameRate = 4;
        List<Sprite> selectedSprites = null;
        switch(currDirection)
        {
            case "NORTH":
                selectedSprites = idleN;
                break;
            case "NORTHEAST":
                selectedSprites = idleNE;
                break;
            case "EAST":
                selectedSprites = idleE;
                break;
            case "SOUTHEAST":
                selectedSprites = idleSE;
                break;
            case "SOUTH":
                selectedSprites = idleS;
                break;
            case "SOUTHWEST":
                selectedSprites = idleSW;
                break;
            case "WEST":
                selectedSprites = idleW;
                break;
            case "NORTHWEST":
                selectedSprites = idleNW;
                break;
        }
        return selectedSprites;
    }

    List<Sprite> Dying(){
        List<Sprite> selectedSprites = deathSprites;
        return selectedSprites;
    }
    IEnumerator dyingWait()
    {
        
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    IEnumerator castTime()
    {
        casting = true;
        yield return new WaitForSeconds(1);
        casting = false;
        
    }
    
    List<Sprite> Attacking(){
        
        frameRate = 10;
        List<Sprite> selectedSprites = null;
        switch(currDirection)
        {
            case "NORTH":
                selectedSprites = attackN;
                break;
            case "NORTHEAST":
                selectedSprites = attackNE;
                break;
            case "EAST":
                selectedSprites = attackE;
                break;
            case "SOUTHEAST":
                selectedSprites = attackSE;
                break;
            case "SOUTH":
                selectedSprites = attackS;
                break;
            case "SOUTHWEST":
                selectedSprites = attackSW;
                break;
            case "WEST":
                selectedSprites = attackW;
                break;
            case "NORTHWEST":
                selectedSprites = attackNW;
                break;
        }
        return selectedSprites;
    }

}
