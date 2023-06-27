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
    public List<Sprite> idleSprites;
    public List<Sprite> hurtSprites;
    public float frameRate;
    private float idleTime;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(isIdle){
            selectedSprites = idleSprites;
        }else if (isHurt){
            selectedSprites = hurtSprites;
        }
        return selectedSprites;
    }
}
