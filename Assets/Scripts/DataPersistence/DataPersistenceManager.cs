using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string _fileName;
    [SerializeField] private bool _useEncryption;
    
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistencesObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) { }

        instance = this;
    }
    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
        this._dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this._gameData = new GameData();
    }
    public void LoadGame()
    {
        this._gameData = _dataHandler.Load();
        if (this._gameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistence i in _dataPersistencesObjects)
        {
            i.LoadData(_gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence i in _dataPersistencesObjects)
        {
            i.SaveData(ref _gameData);
        }
        _dataHandler.Save(_gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}
