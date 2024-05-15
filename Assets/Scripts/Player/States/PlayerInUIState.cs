public class PlayerInUIState : PlayerBaseState
{
    public override void EnterState()
    {
        PlayerManager playerManager = GameManager.instance.playerManager;
        
        playerManager.playerController.DeactivateController();
        playerManager.playerInteraction.DeactivateInteraction();
        playerManager.mouseLook.DisableMouseMove();
        playerManager.mouseLook.VisibleAndFreeCursor();
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}