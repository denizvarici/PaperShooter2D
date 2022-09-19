using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpaceManager : MonoBehaviour
{
    [SerializeField] private GameObject parentBomb;
    [SerializeField] private int bombDamage;
    [SerializeField] private GameObject explodeEffect;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Düþmanlar içimde");
            collision.GetComponent<EnemyManager>().TakeDamage(bombDamage);
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(parentBomb);
        }
        else if (collision.tag == "Wall")
        {
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(parentBomb);

        }
        
    }

    
}
