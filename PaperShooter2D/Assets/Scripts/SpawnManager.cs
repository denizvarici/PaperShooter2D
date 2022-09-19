using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;


    [SerializeField] private Transform leftWall, rightWall, upWall, bottomWall;
    [SerializeField] private float playerOffset_X;
    [SerializeField] private float playerOffset_Y;


    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private int enemyNumberOnScene;
    [SerializeField]
    private int coinNumberOnScene;


    //COIN SPAWN
    [SerializeField] private GameObject coinPrefab;

    void Start()
    {

    }


    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < enemyNumberOnScene)
        {
            EnemySpawner();
        }
        if (GameObject.FindGameObjectsWithTag("Coin").Length < coinNumberOnScene)
        {
            CoinSpawner();
        }
    }

    void InstantiateEnemy(float x, float y)
    {
        Debug.Log("Created Enemy");
        Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);

    }


    void EnemySpawner()
    {
        float spawnSide = Random.Range(1, 5);
        float x, y;
        switch (spawnSide)
        {
            //spawn upside player
            case 1:
                x = Random.Range(leftWall.position.x, rightWall.position.x);
                y = Random.Range(playerTransform.position.y + playerOffset_Y, upWall.position.y);
                InstantiateEnemy(x, y);
                
                
                break;
            //spawn under player
            case 2:
                x = Random.Range(leftWall.position.x, rightWall.position.x);
                y = Random.Range(bottomWall.position.y, playerTransform.position.y - playerOffset_Y);
                InstantiateEnemy(x, y);
                
                
                break;
            //spawn right to player
            case 3:
                x = Random.Range(playerTransform.position.x + playerOffset_X, rightWall.position.x);
                y = Random.Range(bottomWall.position.y, upWall.position.y);
                InstantiateEnemy(x, y);
                
                
                break;
            //spawn left to player
            case 4:
                x = Random.Range(leftWall.position.x, playerTransform.position.x - playerOffset_X);
                y = Random.Range(bottomWall.position.y, upWall.position.y);
                InstantiateEnemy(x, y);
                
                
                break;
        }
    }

    void CoinSpawner()
    {
        float spawnSide = Random.Range(1, 5);
        float x, y;
        switch (spawnSide)
        {
            //spawn upside player
            case 1:
                x = Random.Range(leftWall.position.x, rightWall.position.x);
                y = upWall.position.y - 1.5f;
                InstantiateCoin(x, y);
                break;
            //spawn under player
            case 2:
                x = Random.Range(leftWall.position.x, rightWall.position.x);
                y = bottomWall.position.y + 1.5f;


                InstantiateCoin(x, y);
                break;
            //spawn right to player
            case 3:
                x = rightWall.position.x - 1.5f;
                y = Random.Range(bottomWall.position.y, upWall.position.y);

                InstantiateCoin(x, y);
                break;
            //spawn left to player
            case 4:
                x = leftWall.position.x + 1.5f;
                y = Random.Range(bottomWall.position.y, upWall.position.y);

                InstantiateCoin(x, y);
                break;
        }
    }

    void InstantiateCoin(float x, float y)
    {
        Instantiate(coinPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
