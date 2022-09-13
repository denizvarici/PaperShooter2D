using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody2D bulletRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        FindObjectOfType<AudioManager>().Play("PlayerShoot");
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyManager>().Flash();
            collision.GetComponent<EnemyManager>().BloodSpread();
            Vector2 forceEnemyDirection = bulletRigidBody.velocity;
            collision.GetComponent<EnemyManager>().EnemyRecoil(forceEnemyDirection);
            
            Destroy(this.gameObject);
        }
    }
}
