using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector3 self;
    Vector2 moveDirection;
    private bool hasTarget = false;
    public float distanceToTarget;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if(distanceToTarget < 3f){
            hasTarget = true;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y*.001f));
        if(target && hasTarget)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //not required
            //rb.rotation = angle;  // Rotation is *not* required, depends on spriting
            moveDirection = direction;
        }
    }

    void FixedUpdate()
    {
        if(target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
}
