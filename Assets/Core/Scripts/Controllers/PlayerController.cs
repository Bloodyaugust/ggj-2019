using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {
  public int PlayerIndex;
  public int ScorePerLevel;
  public int MaxLevel;

  bool _isActive;
  bool _isPlaying;
  int _score;
  int _level;
  Player _player;
  PlayerMovement _playerMovement;
  Toolbox _toolbox;

  void EvaluateScore () {
    _playerMovement = GetComponent<PlayerMovement>();
    _score = Mathf.Clamp(_score, 0, ScorePerLevel * MaxLevel);

    int oldLevel = _level;
    _level = (int)Mathf.Floor(_score / ScorePerLevel);

    if (oldLevel != _level) {
      _toolbox.HouseLevelChange.Invoke(new HouseChangeData(_level, PlayerIndex));
    }
  }

  // Start is called before the first frame update
  void Start () {
    _isActive = false;
    _isPlaying = false;
    _player = ReInput.players.GetPlayer(PlayerIndex);
    _score = 0;
    _toolbox = Toolbox.Instance;

    _toolbox.GameEnd.AddListener(OnGameEnd);
    _toolbox.GameStart.AddListener(OnGameStart);
    _toolbox.Score.AddListener(OnScore);

    EvaluateScore();
  }

  // Update is called once per frame
  void Update () {
    if (!_isActive && _player.GetAnyButtonDown()) {
      _isActive = true;
      _toolbox.PlayerActive.Invoke(new ActiveData(PlayerIndex));
      _toolbox.PlayerActive.Invoke(new ActiveData(1));
      _toolbox.PlayerActive.Invoke(new ActiveData(2));
      _toolbox.PlayerActive.Invoke(new ActiveData(3));
    }
  }

  void OnGameEnd (int winningPlayerIndex) {
    _isActive = false;
    _isPlaying = false;
    _playerMovement.enabled = false;
  }

  void OnGameStart () {
    _isPlaying = true;
    _score = 0;
    _playerMovement.enabled = true;
  }

  void OnScore (ScoreData scoreData) {
    if (PlayerIndex == scoreData.PlayerIndex) {
      _score += scoreData.Amount;
      Debug.Log(scoreData.Amount);
      EvaluateScore();
    }
  }
}
