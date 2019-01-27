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
  Toolbox _toolbox;

  void EvaluateScore () {
    _score = Mathf.Clamp(_score, 0, ScorePerLevel * MaxLevel);
    _level = (int)Mathf.Floor(_score / ScorePerLevel);

    //TODO: Set house sprite
    Debug.Log(_level);
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
    }
  }

  void OnGameEnd () {
    _isActive = false;
    _isPlaying = false;
  }

  void OnGameStart () {
    _isPlaying = true;
    _score = 0;
  }

  void OnScore (ScoreData scoreData) {
    if (PlayerIndex == scoreData.PlayerIndex) {
      _score += scoreData.Amount;
      EvaluateScore();
    }
  }
}
