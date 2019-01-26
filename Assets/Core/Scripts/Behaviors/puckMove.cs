using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckMove : MonoBehaviour
{
    public float moveDurationMin = 1f;
    public float moveDurationMax = 5f;
    public float speed = 1f;
    public float waitMin = 1f;
    public float waitMax = 5f;

    float moveDuration = 5f;
    float elapsedTime = 0f;
    float wait = 0f;
    float waitTime = 0f;

    float randX;
    float randY;

    bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        randX = Random.Range(-speed, speed);
        randY = Random.Range(-speed, speed);
        moveDuration = Random.Range(moveDurationMin, moveDurationMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < moveDuration && move)
        {
            transform.Translate(new Vector2(randX, randY) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        else if (move)
        {
            move = false;
            wait = Random.Range(waitMin, waitMax);
            waitTime = 0f;
        }

        if (waitTime < wait && !move)
        {
            waitTime += Time.deltaTime;
        }
        else if (!move)
        {
            move = true;
            elapsedTime = 0f;
            randX = Random.Range(-speed, speed);
            randY = Random.Range(-speed, speed);
            moveDuration = Random.Range(moveDurationMin, moveDurationMax);
        }
    }
}
