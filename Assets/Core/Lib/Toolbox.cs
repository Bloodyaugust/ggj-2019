using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

	enum GameState { WAITING, PLAYING, SCORE };

	bool[] _playersReady;
	GameState _currentState;

	public ActiveEvent PlayerActive;
	public ScoreEvent Score;
	public UnityEvent GameEnd;
	public UnityEvent GameStart;
	public UnityEvent HouseDowngrade;
	public UnityEvent HouseUpgrade;
    public List<AudioClip> oneShotClips;
    public List<AudioClip> loopingClips;
    AudioSource cameraAudioSource;

	void Awake () {
		_currentState = GameState.WAITING;
		_playersReady = new bool[4];

		HouseDowngrade = new UnityEvent();
		HouseUpgrade = new UnityEvent();
		GameEnd = new UnityEvent();
		GameStart = new UnityEvent();
		Score = new ScoreEvent();
		PlayerActive = new ActiveEvent();

		PlayerActive.AddListener(OnPlayerActive);

        cameraAudioSource = GetComponent<AudioSource>();
        cameraAudioSource.clip = loopingClips[0];
        cameraAudioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
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
