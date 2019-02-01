using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using Rewired;

class AirConsoleMessage {
  public int FromID { get; set; }
  public JToken Data { get; set; }
}

public class AirConsoleInputController : MonoBehaviour {
  AirConsole _airConsole;
  CustomController[] _airconsoleControllers;
  Dictionary<string, string> _actionMap;
  List<AirConsoleMessage> _messages;
  List<int> _airconsoleControllerIDs;
  Player[] _players;

  void AddNewPlayer (int deviceID) {
    if (_airconsoleControllerIDs.IndexOf(deviceID) < 0) {
      _airconsoleControllerIDs.Add(deviceID);
    }
  }

  void Awake () {
    _airConsole = GetComponent<AirConsole>();
    #if UNITY_WEBGL
      _airConsole.enabled = true;
    #endif

    _actionMap = new Dictionary<string, string>(){
      { "Move", "LeftStickY" },
      { "Rotate", "RightStickX" }
    };
    _airconsoleControllerIDs = new List<int>();
    _players = ReInput.players.GetPlayers().Cast<Player>().ToArray();
    _messages = new List<AirConsoleMessage>();

    _airconsoleControllers = new CustomController[_players.Length];
    for (int i = 0; i < _players.Length; i++) {
      _airconsoleControllers[i] = (CustomController)_players[i].controllers.GetControllerWithTag(ControllerType.Custom, "AirConsoleController");
    }

    ReInput.InputSourceUpdateEvent += OnInputUpdate;
    AirConsole.instance.onMessage += OnMessage;
    AirConsole.instance.onReady += OnReady;
    AirConsole.instance.onConnect += OnConnect;
  }

  void OnConnect (int deviceID) {
    AddNewPlayer(deviceID);
  }

  void OnDisconnect (int deviceID) {
    _airconsoleControllerIDs.Remove(deviceID);
    Debug.Log("Removing device" + deviceID);
  }

  void OnInputUpdate () {
    foreach (AirConsoleMessage message in _messages) {
      string action = message.Data["action"].ToString();
      float value = float.Parse(message.Data["value"].ToString());

      _airconsoleControllers[_airconsoleControllerIDs.IndexOf(message.FromID)].SetAxisValue(_actionMap[action], value);
    }

    _messages.Clear();
  }

  void OnMessage (int from, JToken data) {
    _messages.Add(new AirConsoleMessage { FromID=from, Data=data });
  }

  void OnReady (string code) {
    List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
    foreach (int deviceID in connectedDevices) {
      AddNewPlayer(deviceID);
    }
  }
}
