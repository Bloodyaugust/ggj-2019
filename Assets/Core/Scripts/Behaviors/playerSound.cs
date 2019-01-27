using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Com.LuisPedroFonseca.ProCamera2D;

public class playerSound : MonoBehaviour
{
    public AudioClip engine1;
    public AudioClip engine2;
    public AudioClip backup;
    public AudioClip honk;
    public AudioClip collisionSound;
    AudioSource playerAudioSource;
    public ParticleSystem smokeParticles;

    Player _player;
    PlayerController _playerController;

    int lastDirection = 0;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _player = ReInput.players.GetPlayer(_playerController.PlayerIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        //smokeParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerRot = transform.eulerAngles.z;

        if (_player.GetAxis("Move") > 0)
        {

            if (lastDirection != -1)
            {
                lastDirection = -1;
                playerAudioSource.clip = engine2;
                playerAudioSource.Play();

                var em = smokeParticles.emission;
                em.rateOverTime = 12f;


            }
        }
        else if (_player.GetAxis("Move") < 0)
        {

            if (lastDirection != 1)
            {
                lastDirection = 1;
                playerAudioSource.clip = backup;
                playerAudioSource.Play();

                var em = smokeParticles.emission;
                em.rateOverTime = 12f;

            }
        }
        else
        {
            if (lastDirection != 0)
            {
                lastDirection = 0;
                playerAudioSource.clip = engine1;
                playerAudioSource.Play();

                var em = smokeParticles.emission;
                em.rateOverTime = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.relativeVelocity.magnitude > 1)
            {
                playerAudioSource.PlayOneShot(collisionSound);
                ProCamera2DShake.Instance.Shake("CollisionShake");
            }
        }
    }
}
