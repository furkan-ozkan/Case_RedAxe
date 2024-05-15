public class CarBuyState : CarBaseState
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
        
    }

    public override void Interaction()
    {
        GameManager.instance.uiManager.OpenExpertisePanel(car);
        
        PlayerStateManager stateManager = GameManager.instance.playerStateManager;
        stateManager.SwitchState(stateManager.InUIState());
    }
}