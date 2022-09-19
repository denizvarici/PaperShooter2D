using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    /// <summary>
    /// SAÐ TIKLAYINCA BOMBA ATSIN
    /// </summary>
    //Enemy Components
    private Rigidbody2D enemyRigidbody;
    [SerializeField] private float enemyRecoilSpeed;
    private GameObject playerObject;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float enemyMoveSpeed;

    //Health system
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;


    void Start()
    {              
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        
    }

    
    void FixedUpdate()
    {
        TargetAndMove();      
    }


     
    public void EnemyRecoil(Vector2 forceDirection)
    {
        enemyRigidbody.AddForce(forceDirection * enemyRecoilSpeed);
    }
    



    void TargetAndMove()
    {
        //Rotate
        var playerObj = playerObject.transform;
        var thisObj = transform;

        var direction = (playerObj.position - thisObj.position).normalized;

        thisObj.right = Vector3.Slerp(thisObj.right, direction, rotationSpeed);

        //Move
        enemyRigidbody.velocity = thisObj.right * enemyMoveSpeed * Time.fixedDeltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>());
        }
    }





}
