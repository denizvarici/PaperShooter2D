using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region  EnemyFlashAfterHitVariables
    private SpriteRenderer enemySpriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;


    [SerializeField]private Material flashMaterial;
    [SerializeField]private float flashDuration;
    #endregion
    [SerializeField] private GameObject bloodEffect;


    void Start()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = enemySpriteRenderer.material;
    }

    
    void Update()
    {
        
    }


    #region EnemyFlashAfterHit
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
    #endregion

    public void BloodSpread()
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

}
