using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreData {
  public int Amount;
  public int PlayerIndex;
}

[System.Serializable]
public class ScoreEvent : UnityEvent<ScoreData> {}
