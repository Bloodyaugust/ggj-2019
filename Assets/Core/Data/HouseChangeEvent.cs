using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HouseChangeData {
  public int Level;
  public int PlayerIndex;

  public HouseChangeData (int level, int playerIndex) {
    Level = level;
    PlayerIndex = playerIndex;
  }
}

[System.Serializable]
public class HouseChangeEvent : UnityEvent<HouseChangeData> {}
