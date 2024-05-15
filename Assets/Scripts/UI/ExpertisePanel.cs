using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpertisePanel : MonoBehaviour
{
    #region Vars
    [Header("Text Fields")]
    [SerializeField] private List<TextMeshProUGUI> _expertiseValueFields = new List<TextMeshProUGUI>();
    
    [Header("Buttons")]
    [SerializeField] private Button _btnClose;

    private Car _car;
    
    // Needed Managers
    private UIManager _uiManager;

    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }

    #endregion

    #region Main Funcs.

    public void InitializePanel(Car car)
    {
        _car = car;
        FillExpertiseValueFields();
    }
    private void FillExpertiseValueFields()
    {
        List<CarPart> parts = _car.GetCarParts();
        _expertiseValueFields[0].text = parts[0].status.ToString();
        _expertiseValueFields[1].text = parts[1].status.ToString();
        _expertiseValueFields[2].text = parts[2].status.ToString();
        _expertiseValueFields[3].text = parts[3].status.ToString();
        _expertiseValueFields[4].text = parts[4].status.ToString();
        _expertiseValueFields[5].text = parts[5].status.ToString();
        _expertiseValueFields[6].text = parts[6].status.ToString();
        _expertiseValueFields[7].text = parts[7].status.ToString();
        _expertiseValueFields[8].text = parts[8].status.ToString();
        _expertiseValueFields[9].text = parts[9].status.ToString();
        _expertiseValueFields[10].text = parts[10].status.ToString();
        _expertiseValueFields[11].text = parts[11].quality.ToString();
        _expertiseValueFields[12].text = _car.maxSpeed.ToString();
        _expertiseValueFields[13].text = _car.torque.ToString();
        _expertiseValueFields[14].text = _car.kilometers.ToString();
    }

    private void ClearFields()
    {
        foreach (var i in _expertiseValueFields)
        {
            i.text = "";
        }
    }

    #endregion

    #region Utils

    private void Init()
    {
        _uiManager = GameManager.instance.uiManager;
        _btnClose.onClick.AddListener(CloseButton);
    }

    #endregion
    
    #region Buttons

    private void CloseButton()
    {
        ClearFields();
        _uiManager.OpenInGamePanel();
        
        // Update Player State
        PlayerStateManager stateManager = GameManager.instance.playerStateManager;
        stateManager.SwitchState(stateManager.InGameState());
    }

    #endregion
}