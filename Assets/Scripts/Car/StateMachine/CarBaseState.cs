public abstract class CarBaseState
{
    public PlayerControls controls;
    public Car car;
    public abstract void EnterState(PlayerControls controls, Car car);
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void Interaction();
}