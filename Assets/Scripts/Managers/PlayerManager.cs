using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public PlayerData playerData;
    public GameObject player;
    public GameObject playerMesh;
    
    public PlayerController playerController;
    public PlayerInteraction playerInteraction;
    public MouseLook mouseLook;
    public Car playerInCar { get; set; }

    public void HidePlayerMesh() => playerMesh.SetActive(false);
    public void ShowPlayerMesh() => playerMesh.SetActive(true);
    
    #region Player Data Funcs.

    public void IncreaseMoney(int increaseAmount)
    {
        playerData.money += increaseAmount;
    }
    
    public bool DecreaseMoney(int decreaseAmount)
    {
        if (playerData.money >= decreaseAmount)
        {
            playerData.money -= decreaseAmount;
            return true;
        }
        return false;
    }
    
    public void UpdateOwnedCarsStates()
    {
        foreach (var i in playerData.ownedCars)
        {
            i.SwitchState(new CarDrivableState());
        }
    }

    public void AddToOwnedCars(Car car)
    {
        playerData.ownedCars.Add(car);
    }
    #endregion
    
    #region IDataPersistence

    public void LoadData(GameData data)
    {
        playerData.ownedCars.Clear();
        CarManager carManager = GameManager.instance.carManager;
        
        playerData.money = data.money;
        foreach (var i in data.ownedCars)
        {
            playerData.ownedCars.Add(carManager.FindCarById(i));
        }
        UpdateOwnedCarsStates();
    }

    public void SaveData(ref GameData data)
    {
        data.money = playerData.money;
        foreach (var i in playerData.ownedCars)
        {
            if (data.ownedCars.Contains(i.id))
            {
                data.ownedCars.Remove(i.id);
            }
            data.ownedCars.Add(i.id);
        }
    }

    #endregion
}