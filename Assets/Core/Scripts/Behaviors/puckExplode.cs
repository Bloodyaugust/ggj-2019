using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class puckExplode : MonoBehaviour
{
    public float countdown = 5f;
    public float radius = 10;
    public float force = 500f;
    Toolbox toolbox;
    ParticleSystem particles;
    public List<Sprite> images = new List<Sprite>();

    private void Awake()
    {
        toolbox = Toolbox.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explodePuck());
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explodePuck()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(countdown/10);
        renderer.sprite = images[1];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[0];
        yield return new WaitForSeconds(countdown /10);
        renderer.sprite = images[1];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[0];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[1];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[0];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[1];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[0];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[1];
        yield return new WaitForSeconds(countdown / 10);
        renderer.sprite = images[0];


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

        toolbox.playOneShotClip(3);
        particles.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        ProCamera2DShake.Instance.Shake("BombShake");

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


}

