using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) { }

        instance = this;

        playerManager = FindObjectOfType<PlayerManager>();
        playerStateManager = FindObjectOfType<PlayerStateManager>();
        uiManager = FindObjectOfType<UIManager>();
        carManager = FindObjectOfType<CarManager>();
        popupManager = FindObjectOfType<PopupManager>();

    }

    #endregion

    #region Vars

    public PlayerManager playerManager { get; private set; }
    public PlayerStateManager playerStateManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public CarManager carManager { get; private set; }
    public PopupManager popupManager { get; private set; }

    #endregion

}