using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayerMove : MonoBehaviour
{
    public Rigidbody2D selfBody;
    // Start is called before the first frame update
    void Start()
    {
        selfBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + .1f);
            //selfBody.AddForce(new Vector2(0, 10));
        }
        if (Input.GetKey("s"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - .1f);
        }
        if (Input.GetKey("a"))
        {
            transform.position = new Vector2(transform.position.x - .1f, transform.position.y);
        }
        if (Input.GetKey("d"))
        {
            transform.position = new Vector2(transform.position.x + .1f, transform.position.y);
        }
        
    }
}
