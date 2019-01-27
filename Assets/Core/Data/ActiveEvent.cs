using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActiveData {
  public int PlayerIndex;

  public ActiveData (int playerIndex) {
    PlayerIndex = playerIndex;
  }
}

[System.Serializable]
public class ActiveEvent : UnityEvent<ActiveData> {}
