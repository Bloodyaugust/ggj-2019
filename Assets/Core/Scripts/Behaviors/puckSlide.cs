using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckSlide : MonoBehaviour

{
    public float CollisionMultiplier = 1f;
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
                puckBody.AddForceAtPosition(contact.normal * CollisionMultiplier, contact.point, ForceMode2D.Impulse);
            }
        }
    }
}
