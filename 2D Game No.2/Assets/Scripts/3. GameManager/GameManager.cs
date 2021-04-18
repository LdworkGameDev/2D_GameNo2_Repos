using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Running,
    Pause,
}

public class GameManager : Manager<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        InitializeStartGameState(GameState.Running);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerManager.isInteractingWithUI)
        {
            ChangeGameState((gameState == GameState.Running) ? GameState.Pause : GameState.Running);
        }
    }

    private void InitializeStartGameState(GameState state)
    {
        gameState = state;
        PlayerManager.canReceivePlayerActionInput = true;
        PlayerManager.canReceiveAttackInput = true;
        PlayerManager.isInteractingWithUI = false;
    }

    private void ChangeGameState(GameState newGameState)
    {
        GameState previousState = gameState;
        gameState = newGameState;

        switch (gameState)
        {
            case GameState.Running:
                PlayerManager.canReceivePlayerActionInput = true;
                Time.timeScale = 1f;
                break;

            case GameState.Pause:
                PlayerManager.canReceivePlayerActionInput = false;
                Time.timeScale = 0f;
                break;

            default:
                break;
        }
    }
}
