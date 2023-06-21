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
    public float damage;
    public float aggroRange = 5f;
    private float shotCooldown;
    public float startShotCooldown = 10f;
    bool hasFired = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mageEnemyAnim = GetComponent<Animator>();
        damage = enemyStats.magicDamage + enemyStats.intelligence;
        shotCooldown = startShotCooldown;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Character");
        target = player.transform;
        self = transform.position;
        distanceToTarget = Vector2.Distance(self, target.position);
    }
    // Update is called once per frame
    void Update()
    {
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
        }else{
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

    
}

