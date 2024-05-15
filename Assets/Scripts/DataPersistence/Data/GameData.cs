using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public Vector3 playerPos;
    public SerializableDictionary<string, string> cars;
    public SerializableDictionary<string, string> carParts;
    public SerializableDictionary<string, string> npcies;
    public List<string> ownedCars;

    public GameData()
    {
        money = 100000;
        cars = new SerializableDictionary<string, string>();
        carParts = new SerializableDictionary<string, string>();
        npcies = new SerializableDictionary<string, string>();
        ownedCars = new List<string>();
    }
}