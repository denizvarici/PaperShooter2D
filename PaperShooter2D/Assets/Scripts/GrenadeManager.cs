using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject destroySpace;


    void Start()
    {

    }


    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall")
        {
            Debug.Log("Geri sayým baþlatýldý");
            StartCoroutine(ExplodeBomb());
        }

    }




    IEnumerator ExplodeBomb()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        destroySpace.SetActive(true);

    }
}
