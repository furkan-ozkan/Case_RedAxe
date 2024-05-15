using UnityEngine;

public class PlayerInCarState : PlayerBaseState
{
    public override void EnterState()
    {
        PlayerManager playerManager = GameManager.instance.playerManager;
        
        playerManager.playerController.DeactivateController();
        playerManager.playerInteraction.DeactivateInteraction();
        playerManager.mouseLook.DisableMouseMove();
        playerManager.mouseLook.HideAndLockCursor();
        playerManager.HidePlayerMesh();
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        PlayerManager manager = GameManager.instance.playerManager;
        manager.player.transform.position = manager.playerInCar.inOutLocation.transform.position;
        manager.playerInCar = null;
    }
}