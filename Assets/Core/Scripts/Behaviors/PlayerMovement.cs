using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using NDream.AirConsole;

public class PlayerMovement : MonoBehaviour {
  public float MovementSpeed;
  public float RotationSpeed;

  Player _player;
  PlayerController _playerController;
  Rigidbody2D _rigidbody;

  void Awake () {
    _playerController = GetComponent<PlayerController>();
    _player = ReInput.players.GetPlayer(_playerController.PlayerIndex);
  }

  // Start is called before the first frame update
  void Start() {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {
    _rigidbody.AddForce(transform.up * -1 * _player.GetAxis("Move") * MovementSpeed);
    _rigidbody.AddTorque(_player.GetAxis("Rotate") * RotationSpeed * -1);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Player") {
      if (collision.relativeVelocity.magnitude > 1) {
        _player.SetVibration(0, 1, 0.2f);
        _player.SetVibration(1, 1, 0.2f);
        
        #if UNITY_WEBGL
          AirConsole.instance.Broadcast("{\"action\": \"shake\", \"value\": 200}");
        #endif
      }
    }
  }
}
