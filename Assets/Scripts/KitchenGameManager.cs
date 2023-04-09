using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
  public static KitchenGameManager Instance { get; private set; }

  public event EventHandler OnStateChange;
  public event EventHandler OnGamePaused;
  public event EventHandler OnGameUnpaused;

  private enum State
  {
    WaitingToStart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  private State state;
  private float countdownToStartTimer = 3f;
  private float gamePlayingTimer;
  private float gamePlayingTimerMax = 1000f;
  private bool isGamePause = false;

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

  public float GetGamePlayingTimerNormalize()
  {
    return 1 - (gamePlayingTimer / gamePlayingTimerMax);
  }

  public void TogglePauseGame()
  {
    isGamePause = !isGamePause;

    if (isGamePause)
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

  private void Awake()
  {
    Instance = this;

    state = State.WaitingToStart;
  }

  private void Start()
  {
    GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
  }

  private void GameInput_OnPauseAction(object sender, EventArgs e)
  {
    TogglePauseGame();
  }

  private void GameInput_OnInteractAction(object sender, EventArgs e)
  {
    if (state == State.WaitingToStart)
    {
      state = State.CountdownToStart;
      OnStateChange?.Invoke(this, EventArgs.Empty);
    }
  }

  private void Update()
  {
    switch (state)
    {
      case State.WaitingToStart:
        break;
      case State.CountdownToStart:
        countdownToStartTimer -= Time.deltaTime;
        if (countdownToStartTimer < 0f)
        {
          state = State.GamePlaying;
          gamePlayingTimer = gamePlayingTimerMax;
          OnStateChange?.Invoke(this, EventArgs.Empty);
        }
        break;
      case State.GamePlaying:
        gamePlayingTimer -= Time.deltaTime;
        if (gamePlayingTimer < 0f)
        {
          state = State.GameOver;
          OnStateChange?.Invoke(this, EventArgs.Empty);
        }
        break;
      case State.GameOver:

        break;
    }
  }
}
