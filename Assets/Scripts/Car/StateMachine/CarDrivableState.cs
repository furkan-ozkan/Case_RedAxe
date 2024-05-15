using UnityEngine;

public class CarDrivableState : CarBaseState
{
    public override void EnterState(PlayerControls controls, Car car)
    {
        this.controls = controls;
        this.car = car;
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        PlayerStateManager stateManager = GameManager.instance.playerStateManager;
        PlayerManager playerManager = GameManager.instance.playerManager;
        stateManager.SwitchState(stateManager.InCarState());
        playerManager.playerInCar = car;
        
        GameManager.instance.uiManager.OpenInCarPanel();
    }
    public override void Interaction()
    {
        car.SwitchState(new CarDriveState());
    }
}