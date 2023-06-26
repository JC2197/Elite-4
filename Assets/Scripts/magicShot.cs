using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicShot : MonoBehaviour
{
    
    public float speed;
    Transform target;
    Rigidbody2D rb;
    Vector2 self;
    Vector2 moveDirection;
    Animator magicShotAnim;
    public float aliveTime = 2f;
    private MageEnemyController mage;
    private float damage;
    public float scaleFactor = 2f;
    public AudioSource PlayerTakeDamageRangedSFX;

    // Start is called before the first frame update
    public void Init(MageEnemyController instantiatingMage)
    {
        mage = instantiatingMage;
    }
    void Awake()
    {
        PlayerTakeDamageRangedSFX = GetComponent<AudioSource>();
        transform.localScale *= scaleFactor;
        magicShotAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Character");
        target = player.transform;
        self = transform.position;
        Vector2 direction = (target.position - transform.position).normalized;
        transform.up = direction;
        moveDirection = direction;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }

    // Update is called once per frame
    void Update()
    {
        aliveTime -= Time.deltaTime;
        if(aliveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if(player != null)
                {
                magicShotAnim.Play("HitEffect");
                PlayerTakeDamageRangedSFX.Play();
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 0;
                player.health.TakeDamage((int)mage.damage);
                }
        }
    }
    public void AnimationEvent_HitEffectFinished()
    {
        Destroy(gameObject);
    }
}
