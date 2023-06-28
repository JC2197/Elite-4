using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 self;
    Vector2 moveDirection;
    public BaseEnemy enemyStats;
    private bool hasTarget = false;
    public float distanceToTarget;
    private Animator mageEnemyAnim;
    private SpriteRenderer spriteRenderer;
    public GameObject magicShot;
    private Canvas canvas;
    public float damage;
    public float aggroRange = 8f;
    private float shotCooldown;
    public float startShotCooldown = 10f;
    bool hasFired = false;
    public Health health;
    private bool invincible = false;
    public float invincibilityTime = .5f;
    public AudioSource EnemyTakeDamageSFX;

    private void Awake()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        mageEnemyAnim = GetComponent<Animator>();
        damage = enemyStats.magicDamage + enemyStats.intelligence;
        shotCooldown = startShotCooldown;
        spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyTakeDamageSFX = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        GameObject player = GameObject.Find("Character");
        target = player.transform;
        self = transform.position;
        distanceToTarget = Vector2.Distance(self, target.position);
    }
    // Update is called once per frame
    void Update()
    {
        if(health.isDead){
            canvas.enabled = false;
            mageEnemyAnim.Play("MageEnemyDeath");
        }else{
            GameObject player = GameObject.Find("Character");
            self = transform.position;
            distanceToTarget = Vector2.Distance(self, target.position);
            if(distanceToTarget < aggroRange){
                hasTarget = true;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y*.001f));
            if(target && hasTarget && distanceToTarget >= 4f)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
                Animate();     
            }else if(target && hasTarget){
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y)* 0;
                Animate();
                if(hasFired){
                    shotCooldown -= Time.deltaTime;
                    if(shotCooldown <= 0)
                        hasFired = false;
                }else{
                    Cast();
                    hasFired = true;
                    shotCooldown = startShotCooldown;
                }
            }
        }
        
    }
    void Cast(){
        GameObject projectile = Instantiate(magicShot, transform.position, transform.rotation);
        magicShot projectileScript = projectile.GetComponent<magicShot>();
        projectileScript.Init(this);
    }

    void Animate()
    {
        mageEnemyAnim.SetFloat("MovementX", moveDirection.x);
        mageEnemyAnim.SetFloat("MovementY", moveDirection.y);
        if(moveDirection.x > 0){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!invincible)
        {
            if (other.gameObject.CompareTag("Weapon"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if (player != null)
                {
                    EnemyTakeDamageSFX.Play();
                    health.TakeDamage((int)player.damage);
                    StartCoroutine(Invulnerability());
                }
            }
        }
    }
    IEnumerator Invulnerability()
    {
        Color origColor = gameObject.GetComponent<Renderer>().material.color;
        invincible = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
        gameObject.GetComponent<Renderer>().material.color = origColor;
    }

    private void OnCollisionStay2D(Collision2D other)
    {        
        if (other.gameObject.CompareTag("Enemy"))
        {
            // this can be used to push objects outside of others to stop clipping issues if needed
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            var magnitude = 10; // how far is character knocked back
            Vector2 force = transform.position - other.transform.position; // force vector
            force.Normalize(); // normalize force vector to get direction only and trim magnitude
            rb.AddForce(force * magnitude);
        }        
    }
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
        public void Enlarge()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= 3;
        newScale.y *= 3;
        transform.localScale = newScale;
    }
    
}

