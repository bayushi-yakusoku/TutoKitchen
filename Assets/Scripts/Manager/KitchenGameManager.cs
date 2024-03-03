using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
    // Make it Singleton:
    public static KitchenGameManager Instance { get; private set; }

    public enum EnumState {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
        GamePause,
        GameOptions
    }

    private EnumState _state;
    private EnumState State {
        get => _state;
        set {
            _state = value;
            OnGameStateChange?.Invoke(this, new OnStateChangedEventArgs { state = this.State });

            Debug.Log(this + $": Game state is: {State}");
        }
    }

    private float waitingToStartTimer = 1f;
    private float coutdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;

    public class OnStateChangedEventArgs : EventArgs {
        public EnumState state;
    }

    public event EventHandler<OnStateChangedEventArgs> OnGameStateChange;

    private void Awake() {
        // Singleton simple implementation:
        if (Instance != null) {
            Debug.LogWarning(this + ": There is more than one KitchenGameManager instance... Destroying this one...");
            Destroy(this.gameObject);
        }

        Instance = this;

        State = EnumState.WaitingToStart;
    }

    private void Start() {
        GameInputManager.Instance.OnPauseAction += GameInputManager_OnPauseAction;
    }

    private void GameInputManager_OnPauseAction(object sender, EventArgs e) {
        TogglePause();
    }

    private void Update() {
        switch (State) {
            case EnumState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    State = EnumState.CountDownToStart;
                }

                break;

            case EnumState.CountDownToStart:
                coutdownToStartTimer -= Time.deltaTime;
                if (coutdownToStartTimer < 0f) {
                    State = EnumState.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                }

                break;

            case EnumState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    State = EnumState.GameOver;
                }

                break;

            case EnumState.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() {
        return State == EnumState.GamePlaying;
    }

    public bool IsCountDownToStartActive() {
        return State == EnumState.CountDownToStart;
    }

    public float GetCountDownToStartTimer() {
        return coutdownToStartTimer;
    }

    public float GetGamePlayingTimerNormalized() {
        if (State != EnumState.GamePlaying) {
            return 0f;
        }

        return 1 - (gamePlayingTimer / gamePlayingTimerMax);

    }

    public void PauseGame() {
        Time.timeScale = 0f;
        State = EnumState.GamePause;
    }

    public void PlayGame() {
        Time.timeScale = 1f;
        State = EnumState.GamePlaying;
    }

    public void TogglePause() {
        if (State == EnumState.GamePause) {
            PlayGame();
        }
        else if(State == EnumState.GamePlaying){
            PauseGame();
        }
    }

    public void DisplayOptions() {
        Time.timeScale = 0f;
        State = EnumState.GameOptions;
    }

    public void ToggleOptions() {
        switch (State) {
            case EnumState.GameOptions:
                PauseGame();

                break;

            case EnumState.GamePause:
                DisplayOptions();

                break;

            case EnumState.GamePlaying:
                DisplayOptions();

                break;
        }

    }
}
