using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class SceneFade : MonoBehaviour {
  Animator _animator;
  int _currentScene;
  int _loadingLevel;
  Player _player;

  void Awake () {
    _animator = GetComponent<Animator>();
    _currentScene = SceneManager.GetActiveScene().buildIndex;
    _player = ReInput.players.GetPlayer(0);
  }

  void Update () {
    if (_currentScene == 0 && _player.GetAnyButtonDown()) {
      FadeToLevel(1);
    }
  }

  void FadeToLevel (int levelIndex) {
    _animator.SetTrigger("FadeOut");
    _loadingLevel = levelIndex;
  }

  public void OnFadeComplete () {
    SceneManager.LoadScene(_loadingLevel);
  }
}
