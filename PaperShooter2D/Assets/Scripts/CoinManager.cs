using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private int coinWorth;

    [SerializeField]
    private GameObject coinCollectEffect;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerManager>().AddCoin(coinWorth);
            Instantiate(coinCollectEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
