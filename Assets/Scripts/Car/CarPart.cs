using UnityEngine;

[CreateAssetMenu(menuName = "RedAxe/Car/CarPart", fileName = "CarPart")]
public class CarPart : ScriptableObject
{
    #region Vars

    [Header("General")]
    public string id;
    public string part_Name;
    public bool is_Effective_On_Price;
    
    [Header("%")]
    public float price_Discount_Effect_Min;
    public float price_Discount_Effect_Max;
    public float price_Discount_Effect_Increase;

    [Header("Only For Body Parts")]
    public BodyPartStatus status;
    
    [Header("Only For Engine")]
    public float quality;

    #endregion
}

public enum BodyPartStatus
{
    Original,
    Painted,
    Changed
}