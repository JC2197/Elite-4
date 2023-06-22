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
    private bool hasTarget = false;
    public float distanceToTarget;
    private Animator meleeEnemyAnim;
    public float aggroRange = 5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeEnemyAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Character").transform;
        self = transform.position;
        distanceToTarget = Vector3.Distance(self, target.position);
    }

    // Update is called once per frame
    void Update()
    {
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
}
