using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour, IInteractable, IDataPersistence
{
    #region Guid

    [SerializeField] private string id;
    
    [ContextMenu("Generate id")]
    private void GenerateCarParts()
    {
        id = Guid.NewGuid().ToString();
    }

    #endregion

    #region Vars

    [Header("NPC Details")]
    public CustomerType _customerType;
    public string _name;
    public Car _car;
    public bool hasCar=true;
    
    [Header("NPC Offer & Rate")]
    public int _lastOffer;
    public int _minOffer;
    public int _maxOffer;
    public int _randomIncreaseRate;
    
    // Needed Managers
    private CarManager _carManager;
    private UIManager _uiManager;

    #endregion

    #region Unity Funcs.

    private void Start()
    {
        Init();
    }

    #endregion

    #region Utils

    private void Init()
    {
        _carManager = GameManager.instance.carManager;
        _uiManager = GameManager.instance.uiManager;
        
        FirstOffer();
    }

    #endregion

    #region IInteractable

    public void Interact()
    {
        if (_car)
        {
            // Open Trade Panel
            _uiManager.OpenTradePanel(this);
            
            // Update Player State
            PlayerStateManager stateManager = GameManager.instance.playerStateManager;
            stateManager.SwitchState(stateManager.InUIState());
        }
    }

    #endregion

    #region Offer

    private void FirstOffer()
    {
        if (_lastOffer.Equals(0))
        {
            _lastOffer = (int)_carManager.CalculateCarMaxPrice(_car);
            _lastOffer += _lastOffer / 100 * _randomIncreaseRate;
        }
    }

    #endregion

    #region IDataPersistence

    public void LoadData(GameData data)
    {
        string npcValues = "";
        data.npcies.TryGetValue(id, out npcValues);
            
        if (npcValues?.Length > 0)
        {
            string[] npcAttList;
            npcAttList = npcValues.Split(":");
            
            _customerType = (CustomerType)int.Parse(npcAttList[0]);
            _randomIncreaseRate = int.Parse(npcAttList[1]);
            _minOffer = int.Parse(npcAttList[2]);
            _maxOffer = int.Parse(npcAttList[3]);
            _lastOffer = int.Parse(npcAttList[4]);
            hasCar = bool.Parse(npcAttList[5]);
        }
        else
        {
            _customerType = (CustomerType)Random.Range(0, 3);
            _randomIncreaseRate = Random.Range(5, 16);
            _minOffer = (int)_carManager.CalculateCarMinPrice(_car);
            _maxOffer = (int)_carManager.CalculateCarMaxPrice(_car);
        }

        if (!hasCar)
        {
            _car = null;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.npcies.ContainsKey(id))
        {
            data.npcies.Remove(id);
        }
        string npcValues = (int)_customerType+":"+_randomIncreaseRate+":"+_minOffer+":"+_maxOffer+":"+_lastOffer+":"+hasCar;
        data.npcies.Add(id,npcValues);
    }

    #endregion
}
public enum CustomerType
{
    easy,
    mid,
    hard
}