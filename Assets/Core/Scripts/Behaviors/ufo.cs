using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{
    public float minWaitTime = 7f;
    public float maxWaitTime = 12f;
    public int minItems = 1;
    public int maxItems = 3;
    public List<Transform> dropPoints = new List<Transform>();
    public List<Transform> startPoints = new List<Transform>();
    public float moveSpeed = 10;

    public Transform basicPuck;
    public Transform movingPuck;
    public Transform explodingPuck;

    public float movingChance = .3f;
    public float explodingChance = .1f;
    bool moving = false;
    bool entering = true;
    Vector2 targetPos = new Vector2(0f, 0f);
    int startPoint = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToDrop());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
            float distance = Vector2.Distance(transform.position, targetPos);
            if (distance < 1)
            {
                moving = false;
                if (entering)
                {
                    entering = false;
                    dropPucks();
                }
                else
                {
                    entering = true;
                    StartCoroutine(waitToDrop());
                }
            }
        }
    }

    IEnumerator waitToDrop()
    {
        Debug.Log("waiting");
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        moveToDropPoint();
    }

    IEnumerator waitToLeave()
    {
        yield return new WaitForSeconds(1);
        leaveDropPoint();
    }

    void dropPucks()
    {
        int puckCount = Random.Range(minItems, maxItems);
        for (int i = 0; i < puckCount; i++)
        {
            float randType = Random.value;
            Debug.Log(randType / 10);
            if (randType < movingChance)
            {
                Instantiate(movingPuck, transform.position + new Vector3(randType/10,randType/10,0), Quaternion.identity);
            }
            else if (randType < movingChance + explodingChance)
            {
                Instantiate(explodingPuck, transform.position + new Vector3(randType / 10, randType / 10, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(basicPuck, transform.position + new Vector3(randType / 10, randType / 10, 0), Quaternion.identity);
            }
        }
        StartCoroutine(waitToLeave());
    }

    void leaveDropPoint()
    {
        switch (startPoint)
        {
            case 0:
                targetPos = startPoints[2].position;
                break;
            case 1:
                targetPos = startPoints[3].position;
                break;
            case 2:
                targetPos = startPoints[0].position;
                break;
            case 3:
                targetPos = startPoints[1].position;
                break;
        }
        Debug.Log(targetPos);
        moving = true;
    }

    void moveToDropPoint()
    {
        Debug.Log("starting");
        int dropPoint = Random.Range(0, dropPoints.Count - 1);
        startPoint = Random.Range(0, startPoints.Count - 1);

        transform.position = startPoints[startPoint].position;
        targetPos = dropPoints[dropPoint].position;

        moving = true;
    }
}
