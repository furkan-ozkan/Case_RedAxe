using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private PlayerBaseState currentState = new PlayerInGameState();

    private void Start()
    {
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    public PlayerBaseState InGameState()
    {
        return new PlayerInGameState();
    }
    public PlayerBaseState InUIState()
    {
        return new PlayerInUIState();
    }
    public PlayerBaseState InCarState()
    {
        return new PlayerInCarState();
    }
}