using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckSlide : MonoBehaviour

{
    public int Value = 5; //positive for good pucks, negative for bad
    private Rigidbody2D puckBody;

    // Start is called before the first frame update
    void Start()
    {
        puckBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //Give the puck an impulse to slide around a bit. Change the friction with the drag settings in the rigidbody
            if (contact.collider.tag == "Player")
            {
                puckBody.AddForceAtPosition(contact.normal, contact.point, ForceMode2D.Impulse);
            }
        }
    }
}
