using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private Vector2 moveDirection;
    public float speed;
    // Start is called before the first frame update
    Animator bossShotAnim;
    private BossFireBulletScript boss;
    Rigidbody2D rb;
    public AudioSource PlayerTakeDamageRangedSFX;
    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    public void Init(BossFireBulletScript instBoss)
    {
        boss = instBoss;
    }
    void Start()
    {
        boss = GetComponentInParent<BossFireBulletScript>();
        PlayerTakeDamageRangedSFX = GetComponent<AudioSource>();
        bossShotAnim = GetComponent<Animator>();
        speed = 5f;
    }

    // Update is called once per frame

    public void SetMoveDirection(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.up = dir;
        moveDirection = dir;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
    private void Destroy(){
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerScript player = other.gameObject.GetComponentInParent<PlayerScript>();
                if(player != null)
                {
                    if (moveDirection == null)
                        Debug.Log("moveDirection is null");
            
                    if (bossShotAnim == null)
                        Debug.Log("bossShotAnim is null");
                    
                    if (PlayerTakeDamageRangedSFX == null)
                        Debug.Log("PlayerTakeDamageRangedSFX is null");
                    
                    if (boss == null)
                        Debug.Log("boss is null");
                    transform.Translate(moveDirection * speed * 0);
                    bossShotAnim.Play("HitEffect");
                    PlayerTakeDamageRangedSFX.Play();
                    player.health.TakeDamage((int)boss.bulletDamage);
                    Debug.Log("damage is " + (int)boss.bulletDamage);
                }
        }
    }
    public void AnimationEvent_HitEffectFinished()
    {
        Invoke("Destroy", 0f);
    }
}
