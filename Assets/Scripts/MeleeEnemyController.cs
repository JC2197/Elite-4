using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector3 self;
    Vector2 moveDirection;
    private Canvas canvas;
    private bool hasTarget = false;
    public float distanceToTarget;
    private Animator meleeEnemyAnim;
    public Health health;
    public float aggroRange = 5f;
    private bool invincible = false;
    public float invincibilityTime = .001f;
    private void Awake()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        meleeEnemyAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        target = GameObject.Find("Character").transform;
        self = transform.position;
        distanceToTarget = Vector3.Distance(self, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(health.isDead){
            canvas.enabled = false;
            meleeEnemyAnim.Play("skeletonDeath");
        }else{
            distanceToTarget = Vector3.Distance(self, target.position);
            if(distanceToTarget < aggroRange){
                hasTarget = true;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y*.001f));
            if(target && hasTarget)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;            
            }
        }
    }

    void Animate()
    {
        meleeEnemyAnim.SetFloat("MovementX", moveDirection.x);
        meleeEnemyAnim.SetFloat("MovementY", moveDirection.y);
    }

    void FixedUpdate()
    {
        if(target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            Animate();
            //rb.AddForce(moveDirection * 10f); //useful for anotehr enemy
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

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Weapon"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if(player != null)
                {
                    health.TakeDamage((int)player.damage);
                }
        }
    }

    IEnumerator Invulnerability()
    {
        Color origColor = gameObject.GetComponent<Renderer>().material.color;
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    private void OnTriggerStay2D(Collider2D other)
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

}
