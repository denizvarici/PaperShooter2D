using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody2D bulletRigidBody;
    [SerializeField]
    private GameObject bulletDestroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();
        //FindObjectOfType<AudioManager>().Play("PlayerShoot");
        //Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {                       
            collision.GetComponent<EnemyManager>().EnemyRecoil(bulletRigidBody.velocity);

            DestroyBullet();
        }
        if (collision.tag == "Wall")
        {
            DestroyBullet();
        }
    }


    private void DestroyBullet()
    {
        Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
