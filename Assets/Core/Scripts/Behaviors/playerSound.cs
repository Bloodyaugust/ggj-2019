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
    public AudioClip collision;
    AudioSource playerAudioSource;

    Player _player;
    PlayerController _playerController;

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
        Debug.Log(_player.GetAxis("Move"));
    }
}
