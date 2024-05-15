using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

using Random = UnityEngine.Random;

public class Car : MonoBehaviour, IInteractable, IDataPersistence
{
    #region Guid

    public string id;
    
    [ContextMenu("Generate id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }
    
    [ContextMenu("Generate default car parts")]
    private void GenerateCarParts()
    {
        CreateDefaultCarParts();
        _rccCarController = GetComponent<RCC_CarControllerV3>();
        maxSpeed = (int)_rccCarController.maxspeed;
        torque = (int)_rccCarController.maxEngineTorque;
        kilometers = Random.Range(1000, 500000);
    }
    
    private void CreateDefaultCarParts()
    {
        carParts.Clear();
        DeleteDirectory("Assets/Data/Car/"+_carName);
        
        carParts.Add(CreateCarPart(CarPartsNameList.RIGHT_FRONT_FENDER));
        carParts.Add(CreateCarPart(CarPartsNameList.RIGHT_FRONT_DOOR));
        carParts.Add(CreateCarPart(CarPartsNameList.RIGHT_BACK_DOOR));
        carParts.Add(CreateCarPart(CarPartsNameList.RIGHT_BACK_FENDER));
        carParts.Add(CreateCarPart(CarPartsNameList.LEFT_FRONT_FENDER));
        carParts.Add(CreateCarPart(CarPartsNameList.LEFT_FRONT_DOOR));
        carParts.Add(CreateCarPart(CarPartsNameList.LEFT_BACK_DOOR));
        carParts.Add(CreateCarPart(CarPartsNameList.LEFT_BACK_FENDER));
        carParts.Add(CreateCarPart(CarPartsNameList.FRONT_HOOD));
        carParts.Add(CreateCarPart(CarPartsNameList.REAR_HOOD));
        carParts.Add(CreateCarPart(CarPartsNameList.ROOF));
        carParts.Add(CreateCarPart(CarPartsNameList.ENGINE));
        carParts.Add(CreateCarPart(CarPartsNameList.SUSPANSION));
        carParts.Add(CreateCarPart(CarPartsNameList.WHEEL_CAMBER_ANGLE));
        
    }

    private void DeleteDirectory(string targetDir)
    {
        string[] files = Directory.GetFiles(targetDir);
        string[] dirs = Directory.GetDirectories(targetDir);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }
        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }
        Directory.Delete(targetDir, false);
        
        AssetDatabase.Refresh();
    }
    
    private CarPart CreateCarPart(string partName)
    {
        string scriptableObjectPath = "Assets/Data/Car/"+_carName;
        string scriptableObjectName = "Assets/Data/Car/"+_carName+"/"+partName+".asset";
        if (!Directory.Exists(scriptableObjectPath))
        {
            Directory.CreateDirectory(scriptableObjectPath);
        }
        
        if (!AssetDatabase.LoadAssetAtPath(scriptableObjectName, typeof(CarPart)))
        {
            // Dosya yoksa yeni bir ScriptableObject oluşturun
            CarPart carPart = ScriptableObject.CreateInstance<CarPart>();

            carPart.id = Guid.NewGuid().ToString();
            carPart.part_Name = partName;
            
            // Oluşturulan ScriptableObject'u projeye kaydetme
            AssetDatabase.CreateAsset(carPart, scriptableObjectName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return carPart;
        }

        return null;
    }

    #endregion
    
    #region Vars

    [Header("Car State")]
    public CarBaseState currentState = new CarBuyState();

    [Header("Car Details")]
    [SerializeField] private List<CarPart> carParts = new List<CarPart>();
    public string _carName;
    public int maxSpeed;
    public int torque;
    public int kilometers;
    public int originalPrice;

    [Header("Car Controls")]
    public GameObject inOutLocation;
    public GameObject carCamera;
    private PlayerControls _controls;
    public RCC_CarControllerV3 _rccCarController;

    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        currentState.EnterState(_controls, this);
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    #endregion

    #region IInteractable
    
    public void Interact() => currentState.Interaction();

    #endregion

    #region IDataPersistence

    public void LoadData(GameData data)
    {
        string carTransformRaw;
        data.cars.TryGetValue(id, out carTransformRaw);
        if (carTransformRaw != null)
        {
            string[] carTransform = carTransformRaw.Split(":");
            string[] carPos = carTransform[0].Split(",");
            string[] carRot = carTransform[1].Split(",");
            transform.position = new Vector3(float.Parse(carPos[0]), float.Parse(carPos[1]), float.Parse(carPos[2]));
            transform.eulerAngles = new Vector3(float.Parse(carRot[0]), float.Parse(carRot[1]), float.Parse(carRot[2]));
        }
        
        foreach (var i in carParts)
        {
            string carAttributes;
            data.carParts.TryGetValue(i.id, out carAttributes);
            
            if (carAttributes?.Length > 0)
            {
                string[] carAttList;
                carAttList = carAttributes.Split(":");

                i.status = (BodyPartStatus)int.Parse(carAttList[0]);
                i.quality = float.Parse(carAttList[1]);
            }
            else
            {
                GenerateRandomValues(i);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.cars.ContainsKey(id))
        {
            data.cars.Remove(id);
        }

        string carLocation = transform.position.x + "," + transform.position.y + "," + transform.position.z + ":" +
                             transform.eulerAngles.x + "," + transform.eulerAngles.y + "," + transform.eulerAngles.z;
        data.cars.Add(id,carLocation);
        
        foreach (var i in carParts)
        {
            if (data.carParts.ContainsKey(i.id))
            {
                data.carParts.Remove(i.id);
            }
            string carAttributes = (int)i.status + ":" + i.quality;
            data.carParts.Add(i.id,carAttributes);
        }
    }

    #endregion

    #region Util

    public List<CarPart> GetCarParts()
    {
        return carParts;
    }
    private void GenerateRandomValues(CarPart carPart)
    {
        carPart.status = (BodyPartStatus)Random.Range(0, 3);
        
        if (carPart.part_Name.Equals(CarPartsNameList.ENGINE)) 
            carPart.quality = Random.Range(70f, 101f);
        if (kilometers.Equals(0))
            kilometers = Random.Range(0, 500000);
    }
    
    private void Init()
    {
        _controls = new PlayerControls();
        _rccCarController.KillEngine();
        DisableCarControl();
    }

    public void DisableCarControl() =>_rccCarController.canControl = false;
    public void EnableCarControl() =>_rccCarController.canControl = true;

    #endregion

    #region States
    
    public void SwitchState(CarBaseState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState(_controls,this);
    }

    #endregion
}