using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public event EventHandler OnStateChanged;
	public event EventHandler OnGamePaused;
	public event EventHandler OnGameUnpaused;



	public static GameManager Instance { get; private set; }



	private enum State
	{
		WaitingToStart,
		CountdownToStart,
		GamePlaying,
		GameOver,
	}

	private State state;
	private float waitingToStartTimer = 1f;
	private float countdownToStartTimer = 3f;
	private float gamePlayingTimer;
	private float gamePlayingTimerMax = 30f;
	private bool isGamePaused = false;


	private void Awake()
	{
		state = State.WaitingToStart;
		Instance = this;
	}

	private void Start()
	{
		GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
	}

	private void GameInput_OnPauseAction(object sender, EventArgs e)
	{
		PauseGame();
	}

	public void TogglePause()
	{
		PauseGame();
	}
	private void PauseGame()
	{
		isGamePaused = !isGamePaused;
		if (isGamePaused)
		{
			Time.timeScale = 0f;
			OnGamePaused?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			Time.timeScale = 1f;
			OnGameUnpaused?.Invoke(this, EventArgs.Empty);
		}
	}

	private void Update()
	{
		switch (state)
		{
			case State.WaitingToStart:
				waitingToStartTimer -= Time.deltaTime;
				if (waitingToStartTimer < 0f)
				{
					state = State.CountdownToStart;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;

			case State.CountdownToStart:
				countdownToStartTimer -= Time.deltaTime;
				if (countdownToStartTimer < 0f)
				{
					state = State.GamePlaying;
					gamePlayingTimer = gamePlayingTimerMax;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;

			case State.GamePlaying:
				gamePlayingTimer -= Time.deltaTime;
				if (gamePlayingTimer < 0f)
				{
					state = State.GameOver;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case State.GameOver:
				break;
		}
	}

	public bool IsGamePlaying()
	{
		return state == State.GamePlaying;
	}
	public bool IsCountdownToStartActive()
	{
		return state == State.CountdownToStart;
	}
	public bool IsGameOver()
	{
		return state == State.GameOver;
	}

	public float GetCountdownToStartTimer()
	{
		return countdownToStartTimer;
	}

	public float GetPlayingTimerNormalized()
	{
		return 1 - (gamePlayingTimer / gamePlayingTimerMax);
	}
}