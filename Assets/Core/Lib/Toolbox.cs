using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;
using TMPro;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

	enum GameState { WAITING, PLAYING, SCORE };

	bool[] _playersReady;
	GameState _currentState;

	public ActiveEvent PlayerActive;
	public GameObject GameOverText;
	public IntEvent HouseLevelChange;
	public IntEvent GameEnd;
	public ScoreEvent Score;
	public UnityEvent GameStart;
	public UnityEvent HouseDowngrade;
	public UnityEvent HouseUpgrade;
    public List<AudioClip> oneShotClips;
    public List<AudioClip> loopingClips;
    AudioSource cameraAudioSource;


	void Awake () {
		_currentState = GameState.WAITING;
		_playersReady = new bool[4];

		HouseLevelChange = new IntEvent();
		GameEnd = new IntEvent();
		GameStart = new UnityEvent();
		Score = new ScoreEvent();
		PlayerActive = new ActiveEvent();

		GameEnd.AddListener(OnGameEnd);
		GameStart.AddListener(OnGameStart);
		PlayerActive.AddListener(OnPlayerActive);

    cameraAudioSource = GetComponent<AudioSource>();
    cameraAudioSource.clip = loopingClips[0];
    cameraAudioSource.Play();

    SceneManager.sceneLoaded += OnSceneLoaded;

		if (GameOverText) {
			GameOverText.SetActive(true);

			string newGameOverText = $"All players press any button to play";
			GameOverText.GetComponent<TextMeshProUGUI>().text = newGameOverText;
		}
	}

	void OnGameEnd (int winningPlayerIndex) {
		if (GameOverText) {
			GameOverText.SetActive(true);

			string newGameOverText = $"Player {winningPlayerIndex} wins! \r\n All players press any button to play again.";
			GameOverText.GetComponent<TextMeshProUGUI>().text = newGameOverText;
		}

		GameObject[] pucks = GameObject.FindGameObjectsWithTag("Puck");
		for (int i = 0; i < pucks.Length; i++) {
			Destroy(pucks[i]);
		}
	}

	void OnGameStart () {
		if (GameOverText) {
			GameOverText.SetActive(false);
		}
	}

	void OnPlayerActive (ActiveData data) {
		_playersReady[data.PlayerIndex] = true;

		bool ready = true;
		for (int i = 0; i < _playersReady.Length; i++) {
			if (!_playersReady[i]) {
				ready = false;
				break;
			}
		}

		if (ready) {
			_currentState = GameState.PLAYING;
            cameraAudioSource.clip = loopingClips[1];
            cameraAudioSource.Play();

            for (int i = 0; i < _playersReady.Length; i++) {
				_playersReady[i] = false;
			}

			GameStart.Invoke();
		}
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {

	}

	static public T RegisterComponent<T> () where T: Component {
		return Instance.GetOrAddComponent<T>();
	}

    public void playOneShotClip(int clipNum)
    {
        cameraAudioSource.PlayOneShot(oneShotClips[clipNum]);
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<int> {}
