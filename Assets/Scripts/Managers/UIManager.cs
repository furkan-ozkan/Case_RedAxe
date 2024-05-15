using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Vars

    [Header("Panel Scripts")]
    // Panels
    public ExpertisePanel ExpertisePanel;
    public TradePanel TradePanel;
    
    [Header("Panels")]
    // Panel Gameobjects
    [SerializeField] private GameObject _inGameGO;
    [SerializeField] private GameObject _expertiseGO;
    [SerializeField] private GameObject _tradeGO;
    [SerializeField] private GameObject _inCarGO;
    


    #endregion

    #region Panel Controls

    public void OpenInGamePanel()
    {
        CloseAllUIPanels();
        _inGameGO.SetActive(true);
    }
    public void OpenExpertisePanel(Car car)
    {
        ExpertisePanel.InitializePanel(car);
        CloseAllUIPanels();
        _expertiseGO.SetActive(true);
    }
    public void OpenTradePanel(NPC npc)
    {
        TradePanel.InitializePanel(npc);
        CloseAllUIPanels();
        _tradeGO.SetActive(true);
    }
    public void OpenInCarPanel()
    {
        CloseAllUIPanels();
        _inCarGO.SetActive(true);
    }
    
    private void CloseAllUIPanels()
    {
        _inGameGO.SetActive(false);
        _expertiseGO.SetActive(false);
        _tradeGO.SetActive(false);
        _inCarGO.SetActive(false);
    }

    #endregion

}
