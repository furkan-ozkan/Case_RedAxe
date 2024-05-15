using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TradePanel : MonoBehaviour
{
    #region Vars
    [Header("Text Fields")]
    [SerializeField] private List<TextMeshProUGUI> _expertiseValueFields = new List<TextMeshProUGUI>();

    [Header("General / NPC")]
    [SerializeField] private Car _car;
    [SerializeField] private NPC _npc;
    
    [Header("UI")]
    [SerializeField] private Button _btnClose;
    [SerializeField] private Button _btnBuy;
    [SerializeField] private Button _btnOffer;
    [SerializeField] private TMP_InputField _ifOffer;
    [SerializeField] private TextMeshProUGUI _info;

    private bool valueChangeEnabled;
    private UIManager _uiManager;
    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        StartCoroutine(ValueChangeEventEnable());

        IEnumerator ValueChangeEventEnable()
        {
            yield return new WaitForSeconds(.25f);
            valueChangeEnabled = true;
        }
    }

    private void OnDisable()
    {
        ClearFields();
        valueChangeEnabled = false;
    }

    #endregion

    #region Main Funcs.

    public void InitializePanel(NPC npc)
    {
        _npc = npc;
        _car = npc._car;
        
        ActivateBuyButton();
        FillExpertiseValueFields();
        UpdatePriceText(_npc._lastOffer);
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
        _expertiseValueFields[11].text = "%"+parts[11].quality;
        _expertiseValueFields[12].text = _car.maxSpeed.ToString();
        _expertiseValueFields[13].text = _car.torque.ToString();
        _expertiseValueFields[14].text = _car.kilometers.ToString();
    }
    private void ClearFields()
    {
        _info.text = "";
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
        
        _ifOffer.onValueChanged.AddListener(delegate {OnOfferChanged();});
        _btnClose.onClick.AddListener(CloseButton);
        _btnBuy.onClick.AddListener(BuyButton);
        _btnOffer.onClick.AddListener(OfferButton);
    }

    private void ResetPanel()
    {
        _npc = null;
        _car = null;
    }

    #endregion
    
    #region UI

    private void UpdateInfoText(string message, Color color)
    {
        _info.color = color;
        _info.text = message;
    }
    private void UpdatePriceText(float price)
    {
        _ifOffer.text = price.ToString();
    }
    private void OnOfferChanged()
    {
        if (valueChangeEnabled)
        {
            _btnOffer.interactable = true;
            _btnBuy.interactable = false;
        }
    }
    private void OfferButton()
    {
        switch (_npc._customerType)
        {
            case CustomerType.easy:
                if (int.Parse(_ifOffer.text) >= _npc._minOffer)
                {
                    ActivateBuyButton();
                    _npc._lastOffer = int.Parse(_ifOffer.text);
                }
                else
                {
                    _npc._lastOffer = Random.Range(_npc._lastOffer, _npc._minOffer - _npc._minOffer / 100 * 2);
                    _npc._lastOffer = Mathf.Max(_npc._lastOffer, _npc._minOffer);
                    _ifOffer.text = _npc._lastOffer.ToString();
                    if (_npc._lastOffer.Equals(_npc._minOffer))
                    {
                        UpdateInfoText("Last offer", Color.red);
                        ActivateBuyButton();
                    }
                    else
                    {
                        UpdateInfoText("What about this?", Color.green);
                    }
                }
                break;
            
            case CustomerType.mid:
                int acceptableOfferMid = _npc._minOffer + (_npc._maxOffer - _npc._minOffer) / 2;
                if (int.Parse(_ifOffer.text) >= acceptableOfferMid)
                {
                    ActivateBuyButton();
                    _npc._lastOffer = int.Parse(_ifOffer.text);
                }
                else
                {
                    _npc._lastOffer = Random.Range(_npc._lastOffer, acceptableOfferMid - acceptableOfferMid / 100 * 2);
                    _npc._lastOffer = Mathf.Max(_npc._lastOffer, acceptableOfferMid);
                    _ifOffer.text = _npc._lastOffer.ToString();
                    if (_npc._lastOffer.Equals(acceptableOfferMid))
                    {
                        UpdateInfoText("Last offer", Color.red);
                        ActivateBuyButton();
                    }
                    else
                    {
                        UpdateInfoText("What about this?", Color.green);
                    }
                }
                break;
            
            case CustomerType.hard:
                int acceptableOfferHard = _npc._minOffer + (_npc._maxOffer - _npc._minOffer) / 4 * 3;
                if (int.Parse(_ifOffer.text) >= acceptableOfferHard)
                {
                    ActivateBuyButton();
                    _npc._lastOffer = int.Parse(_ifOffer.text);
                }
                else
                {
                    _npc._lastOffer = Random.Range(_npc._lastOffer, acceptableOfferHard - acceptableOfferHard / 100 * 2);
                    _npc._lastOffer = Mathf.Max(_npc._lastOffer, acceptableOfferHard);
                    _ifOffer.text = _npc._lastOffer.ToString();
                    if (_npc._lastOffer.Equals(acceptableOfferHard))
                    {
                        UpdateInfoText("Last offer", Color.red);
                        ActivateBuyButton();
                    }
                    else
                    {
                        UpdateInfoText("What about this?", Color.green);
                    }
                }
                break;
        }
    }
    private void CloseButton()
    {
        _uiManager.OpenInGamePanel();
        ResetPanel();
        
        PlayerStateManager stateManager = GameManager.instance.playerStateManager;
        stateManager.SwitchState(stateManager.InGameState());
    }
    private void BuyButton()
    {
        if (GameManager.instance.playerManager.DecreaseMoney(_npc._lastOffer))
        {
            GameManager.instance.playerManager.AddToOwnedCars(_car);
            _car.SwitchState(new CarDrivableState());
            _npc.hasCar = false;
            _npc._car = null;
            GameManager.instance.popupManager.ShowPopup("You bought a "+_car._carName,GameManager.instance.popupManager.successColor);
            CloseButton();
        }
        else
        {
            GameManager.instance.popupManager.ShowPopup("Not enough money.",GameManager.instance.popupManager.warningColor);
        }
    }
    private void ActivateBuyButton()
    {
        _btnBuy.interactable = true;
        _btnOffer.interactable = false;
    }

    #endregion
}
