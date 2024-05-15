using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RedAxe/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public int money;
    public List<Car> ownedCars = new List<Car>();
}