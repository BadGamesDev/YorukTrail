using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public int distanceLeft;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #region
    public enum PauseState
    {
        Running,
        Paused,
    }

    public PauseState currentState = PauseState.Running;

    public static event System.Action OnPause;
    public static event System.Action OnResume;

    public void PauseGame()
    {
        currentState = PauseState.Paused;
        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        currentState = PauseState.Running;
        OnResume?.Invoke();
    }
    #endregion
}