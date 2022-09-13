using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region  EnemyAfterHitVariables
    private SpriteRenderer enemySpriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration;

    [SerializeField] private GameObject bloodEffect;

    #endregion


    //Enemy Components
    private Rigidbody2D enemyRigidbody;
    [SerializeField] private float enemyRecoilSpeed;
    private GameObject playerObject;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float enemyMoveSpeed;


    void Start()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = enemySpriteRenderer.material;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        
    }

    
    void FixedUpdate()
    {
        TargetAndMove();      
    }


    #region EnemyAfterHit
    private IEnumerator FlashRoutine()
    {
        enemySpriteRenderer.material = flashMaterial;
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        yield return new WaitForSeconds(flashDuration);
        enemySpriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    public void Flash()
    {
        if (flashRoutine !=null)
        {
            StopCoroutine(FlashRoutine());
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public void BloodSpread()
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

    public void EnemyRecoil(Vector2 forceDirection)
    {
        enemyRigidbody.AddForce(forceDirection * enemyRecoilSpeed);
    }
    #endregion



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
