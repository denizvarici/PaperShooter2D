using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Enemy Components
    private Rigidbody2D enemyRigidbody;
    [SerializeField] private float enemyRecoilSpeed;
    private GameObject playerObject;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float enemyMoveSpeed;


    void Start()
    {              
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        
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

    

   

}
