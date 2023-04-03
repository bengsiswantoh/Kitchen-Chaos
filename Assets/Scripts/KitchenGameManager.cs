using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
  public static KitchenGameManager Instance { get; private set; }

  public event EventHandler OnStateChange;

  private enum State
  {
    WaitingToStart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  private State state;
  private float waitingToStartTimer = 1f;
  private float countdownToStartTimer = 3f;
  private float gamePlayingTimer;
  private float gamePlayingTimerMax = 10f;

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

  // Start is called before the first frame update
  private void Awake()
  {
    Instance = this;

    state = State.WaitingToStart;
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
          OnStateChange?.Invoke(this, EventArgs.Empty);
        }
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
