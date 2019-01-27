using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreData {
  public int Amount;
  public int PlayerIndex;

  public ScoreData (int amount, int playerIndex) {
    Amount = amount;
    PlayerIndex = playerIndex;
  }
}

[System.Serializable]
public class ScoreEvent : UnityEvent<ScoreData> {}
