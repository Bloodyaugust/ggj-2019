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
    public AudioClip ufoSound;

    bool _enabled = false;
    bool entering = true;
    Vector2 targetPos = new Vector2(0f, 0f);
    int startPoint = 1;

    TrailRenderer ufoTrail;

    float startTime;
    float waitTime;
    //int curStage = 0;

    public enum UFOState
    {
        Idle,
        MovingToDrop,
        Dropping,
        MovingToIdle
    }

    UFOState _curState = UFOState.Idle;

    Toolbox _toolbox;

    void Awake () {
      _toolbox = Toolbox.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        ufoTrail = gameObject.GetComponent<TrailRenderer>();

        _toolbox = Toolbox.Instance;

        _toolbox.GameEnd.AddListener(OnGameEnd);
        _toolbox.GameStart.AddListener(OnGameStart);
    }

    void OnGameEnd (int winningPlayerIndex) {
      _enabled = false;
    }

    void OnGameStart () {
        _enabled = true;
        _curState = UFOState.Idle;
        startTime = Time.time;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled)
        {
            switch (_curState)
            {
                case UFOState.Idle: //wait to drop
                    if (Time.time - startTime >= waitTime)
                        moveToDropPoint();
                    break;
                case UFOState.MovingToDrop: //move to point and drop pucks
                    move();
                    break;
                case UFOState.Dropping: //wait to leave drop point
                    if (Time.time - startTime >= waitTime)
                        leaveDropPoint();
                    break;
                case UFOState.MovingToIdle://move away from drop point
                    move();
                    break;
            }

        }
        
    }

    void move()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        float distance = Vector2.Distance(transform.position, targetPos);
        if (distance < 1)
        {
            if (entering)
            {
                entering = false;
                dropPucks();
            }
            else
            {
                entering = true;
                waitTime = Random.Range(minWaitTime, maxWaitTime);
                _curState = UFOState.Idle;
            }
        }
    }


    void dropPucks()
    {
        int puckCount = Random.Range(minItems, maxItems);
        for (int i = 0; i < puckCount; i++)
        {
            float randType = Random.value;
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

        waitTime = 1;
        startTime = Time.time;
        _curState = UFOState.Dropping;
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

        entering = false;
        _curState = UFOState.MovingToIdle;
    }

    void moveToDropPoint()
    {
        ufoTrail.enabled = false;

        GetComponent<AudioSource>().PlayOneShot(ufoSound);

        int dropPoint = Random.Range(0, dropPoints.Count - 1);
        startPoint = Random.Range(0, startPoints.Count - 1);

        transform.position = startPoints[startPoint].position;
        targetPos = dropPoints[dropPoint].position;

        ufoTrail.enabled = true;

        entering = true;

        _curState = UFOState.MovingToDrop;
    }
}
