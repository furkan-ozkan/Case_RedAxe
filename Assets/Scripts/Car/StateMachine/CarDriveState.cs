using UnityEngine;

public class CarDriveState : CarBaseState
{
    public override void EnterState(PlayerControls controls, Car car)
    {
        this.controls = controls;
        this.car = car;
        
        car.carCamera.SetActive(true);
        car.EnableCarControl();
    }

    public override void UpdateState()
    {
        
        if (controls.Car.Engine.triggered)
        {
            car._rccCarController.KillOrStartEngine();
        }

        if (controls.Car.GetInOut.triggered)
        {
            if (!car._rccCarController.engineRunning)
            {
                if (!Physics.CheckSphere(car.inOutLocation.transform.position, .2f))
                {
                    car.SwitchState(new CarDrivableState());
                    car.carCamera.SetActive(false);
                    car.DisableCarControl();
                }
                else
                {
                    GameManager.instance.popupManager.ShowPopup("There is no space for door.",GameManager.instance.popupManager.warningColor);
                }
            }
            else
            {
                GameManager.instance.popupManager.ShowPopup("Engine Still running.",GameManager.instance.popupManager.warningColor);
            }
            
        }
    }

    public override void ExitState()
    {
        PlayerStateManager stateManager = GameManager.instance.playerStateManager;
        stateManager.SwitchState(stateManager.InGameState());
        
        
        GameManager.instance.uiManager.OpenInGamePanel();
    }
    public override void Interaction()
    {
        
    }
}