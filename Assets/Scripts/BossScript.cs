using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossScript : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    bool isIdle = true;
    bool isHurt;
    Transform target;
    Vector2 self;
    public Health health;
    public GameObject boss2;
    public List<Sprite> idleSprites;
    public List<Sprite> hurtSprites;
    public List<Sprite> transitionSprites;
    public float distanceToTarget;
    public float frameRate;
    private float idleTime;
    private float waitingTime;
    public float damage;
    bool casting = false;
    private Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        damage = 20f;
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(idleTime);
        if(health.isDead){
            canvas.enabled = false;
            List<Sprite> actions = Animate();
            if(actions != null)
            {
                float playTime = Time.time - waitingTime;
                int frame = (int)((playTime * frameRate) % actions.Count);
                spriteRenderer.sprite = actions[frame];
            } else {
                idleTime = Time.time;
            }
            newPhase();
        }else{
            waitingTime = Time.time;
            GameObject player = GameObject.Find("Character");
            self = transform.position;
            target = player.transform;
            distanceToTarget = Vector2.Distance(self, target.position);
            List<Sprite> actions = Animate();
            if(actions != null)
            {
                float playTime = Time.time - idleTime;
                int frame = (int)((playTime * frameRate) % actions.Count);
                spriteRenderer.sprite = actions[frame];
            } else {
                idleTime = Time.time;
            }
        } 
    }

    private void newPhase(){
        StartCoroutine(TransitionAnimation());
    }
    IEnumerator TransitionAnimation()
    {
        yield return new WaitForSeconds(2);
        GameObject newBoss = Instantiate(boss2, transform.position, transform.rotation);
        BossPhase2Script boss2Script = newBoss.GetComponent<BossPhase2Script>();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Weapon"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if(player != null)
                {
                    health.TakeDamage((int)player.damage);
                    isIdle = false;
                    isHurt = true;
                    StartCoroutine(IsHit());  
                }
        }
    }
    public IEnumerator IsHit()
    {
        Color origColor = gameObject.GetComponent<Renderer>().material.color;
        
        yield return new WaitForSeconds(1);
        isHurt = false;
        isIdle = true;
    }
    List<Sprite> Animate(){
        List<Sprite> selectedSprites = null;
        if (health.isDead){
            selectedSprites = transitionSprites;
        }else if(isIdle){
            selectedSprites = idleSprites;
        }else if (isHurt){
            selectedSprites = hurtSprites;
        } 
        return selectedSprites;
    }
}
