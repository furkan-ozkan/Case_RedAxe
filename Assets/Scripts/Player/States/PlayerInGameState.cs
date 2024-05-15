using UnityEngine;

public class PlayerInGameState : PlayerBaseState
{
    public override void EnterState()
    {
        PlayerManager playerManager = GameManager.instance.playerManager;
        
        playerManager.playerController.ActivateController();
        playerManager.playerInteraction.ActivateInteraction();
        playerManager.mouseLook.EnableMouseMove();
        playerManager.mouseLook.HideAndLockCursor();
        playerManager.ShowPlayerMesh();
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}