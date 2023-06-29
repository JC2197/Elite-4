using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBulletScript : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;
    public float bulletDamage = 10f;
    [SerializeField]

    private BossScript boss;
    private float startAngle = 0, endAngle = 330f;

    private Vector2 bulletMoveDirection;
    // Start is called before the first frame update
    
    void Start()
    {
        boss = GetComponentInParent<BossScript>();
        bulletDamage = boss.damage;
        Debug.Log("Boss damage is " + boss.damage);
        InvokeRepeating("Fire", 0f, 3f);
        InvokeRepeating("Fire", 0.3f, 3f);
        InvokeRepeating("Fire", .6f, 3f);
        InvokeRepeating("Fire", 0.9f, 3f);
    }

    private void Fire()
    {
        float angleStep = (endAngle-startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0 ; i <bulletsAmount + 1; i++ )
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulmoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulmoveVector - transform.position).normalized;

            GameObject bul = BossBulletPoolScript.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                
                bul.SetActive(true);
                bul.GetComponent<BossBulletScript>().Init(this);
                bul.GetComponent<BossBulletScript>().SetMoveDirection(bulDir);
            angle += angleStep;
        }
    }

}
