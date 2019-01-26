using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActiveData {
  public int PlayerIndex;
}

[System.Serializable]
public class ActiveEvent : UnityEvent<ActiveData> {}
