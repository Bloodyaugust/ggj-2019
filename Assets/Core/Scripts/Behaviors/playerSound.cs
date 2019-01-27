using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class playerSound : MonoBehaviour
{
    public AudioClip engine1;
    public AudioClip engine2;
    public AudioClip backup;
    public AudioClip honk;
    public AudioClip collisionSound;
    AudioSource playerAudioSource;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetAxis("Move") > 0)
        {
            if (lastDirection != -1)
            {
                lastDirection = -1;
                playerAudioSource.clip = engine2;
                playerAudioSource.Play();
            }
        }
        else if (_player.GetAxis("Move") < 0)
        {
            if (lastDirection != 1)
            {
                lastDirection = 1;
                playerAudioSource.clip = backup;
                playerAudioSource.Play();
            }
        }
        else
        {
            if (lastDirection != 0)
            {
                lastDirection = 0;
                playerAudioSource.clip = engine1;
                playerAudioSource.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerAudioSource.volume = 2;
            playerAudioSource.PlayOneShot(collisionSound);
        }
    }
}
