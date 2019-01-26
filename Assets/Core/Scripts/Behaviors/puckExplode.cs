using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckExplode : MonoBehaviour
{
    public float countdown = 5f;
    public float radius = 10;
    public float force = 500f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explodePuck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explodePuck()
    {
        yield return new WaitForSeconds(countdown);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D col in colliders)
        {
            Rigidbody2D body = col.GetComponent<Rigidbody2D>();
            

            if (body != null)
            {
                var dir = body.transform.position - transform.position;
                float fade = 1 - (dir.magnitude / radius);
                Vector2 baseForce = dir.normalized * force * fade;
                body.AddForce(baseForce,ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }
}

