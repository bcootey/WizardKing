using System;
using UnityEngine;

public enum GameState
{
    Loading,
    Playing,
    Paused,
    Cutscene
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    public GameState State { get; private set; } = GameState.Loading;
    
    //reasons that the game shouldnt take inputs. locks are the amount of items calling it so it can support being the the save menu and pausing the game
    public int pauseLocks = 0;
    public int loadingLocks = 0;
    
    public static bool CanAcceptGameplayInput =>
        instance != null && instance.State == GameState.Playing;
    
    public event Action<GameState> OnStateChanged;

    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        RecomputeState();
    }
    
    //pausing api. add a pause lock to pause the game and it automatically recomputes the state the game is in.
    public void AddPauseLock()
    {
        pauseLocks++;
        RecomputeState();
    }

    public void RemovePauseLock()
    {
        pauseLocks = Mathf.Max(0, pauseLocks - 1); // makes it never go below zero
        RecomputeState();
    }
    //loading api
    public void AddLoadingLock()
    {
        loadingLocks++;
        RecomputeState();
    }

    public void RemoveLoadingLock()
    {
        loadingLocks = Mathf.Max(0, loadingLocks - 1);
        RecomputeState();
    }

    private void RecomputeState()
    {
        var old = State;

        if (loadingLocks > 0) State = GameState.Loading;
        else if (pauseLocks > 0) State = GameState.Paused;
        else State = GameState.Playing;

        // apply side effects only when state changes. so the pause or loading effects doesnt get activated twice.
        if (State != old)
        {
            ApplySideEffects(State);
            OnStateChanged?.Invoke(State);
        }
    }

    private void ApplySideEffects(GameState state)
    {
        switch (state)
        {
            case GameState.Paused:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case GameState.Loading:
                Time.timeScale = 1f;
                Debug.Log("loading state");
                break;
        }
    }
}
