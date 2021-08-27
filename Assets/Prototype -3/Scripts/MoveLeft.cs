using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float moveLeft;
    float leftBound = -9;

    PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Movement();
        Bound();
    }

    void Movement()
    {
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (moveLeft * 2));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveLeft);
            }
        }
    }

    void Bound()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstackle"))
        {
            Destroy(gameObject);
        }
    }

}
