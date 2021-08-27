using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacklePrefabs;
    [SerializeField] Vector3 spawnPos = new Vector3(25,0,0);
    [SerializeField] float startDelay ;
    [SerializeField] float repeatRate ;

    int obstackleIndex;

    PlayerController playerControllerScript;

    private void Start()
    {
        InvokeRepeating("SpawnObstackles", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstackles()
    {
        if (playerControllerScript.gameOver == false)
        {
            obstackleIndex = Random.Range(0, obstacklePrefabs.Length);
            Instantiate(obstacklePrefabs[obstackleIndex], spawnPos, obstacklePrefabs[obstackleIndex].transform.rotation);
        }
    }

}
