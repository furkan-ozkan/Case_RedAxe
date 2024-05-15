using UnityEngine;

public class CarManager : MonoBehaviour
{
    #region Vars

    [SerializeField] private Car[] cars;

    #endregion

    #region Unity Funcs.

    private void Start()
    {
        Init();
    }

    #endregion

    #region Main Funcs

    public Car FindCarById(string id)
    {
        foreach (var i in cars)
            if (i.id.Equals(id))
                return i;
        return null;
    }

    #endregion

    #region Car Price Calculators

    public float CalculateCarMaxPrice(Car car)
    {
        float totalMaxDiscount = 0;
        foreach (var i in car.GetCarParts())
        {
            if (i.is_Effective_On_Price)
            {
                if (!i.part_Name.Equals(CarPartsNameList.ENGINE))
                {
                    switch (i.status)
                    {
                        case BodyPartStatus.Painted:
                            totalMaxDiscount += i.price_Discount_Effect_Min;
                            break;
                        case BodyPartStatus.Changed:
                            totalMaxDiscount += i.price_Discount_Effect_Min + i.price_Discount_Effect_Increase;
                            break;
                    }
                }
                else
                {
                    if (i.quality > 85 && i.quality < 97)
                    {
                        totalMaxDiscount += i.price_Discount_Effect_Min;
                    }
                    else if (i.quality > 70 && i.quality < 85)
                    {
                        totalMaxDiscount += i.price_Discount_Effect_Min + i.price_Discount_Effect_Increase;
                    }
                }
            }
        }
        return car.originalPrice-(car.originalPrice / 100 * totalMaxDiscount);
    }
    public float CalculateCarMinPrice(Car car)
    {
        float totalMinDiscount = 0;
        foreach (var i in car.GetCarParts())
        {
            if (i.is_Effective_On_Price)
            {
                if (!i.part_Name.Equals(CarPartsNameList.ENGINE))
                {
                    switch (i.status)
                    {
                        case BodyPartStatus.Painted:
                            totalMinDiscount += i.price_Discount_Effect_Max;
                            break;
                        case BodyPartStatus.Changed:
                            totalMinDiscount += i.price_Discount_Effect_Max + i.price_Discount_Effect_Increase;
                            break;
                    }
                }
                else
                {
                    if (i.quality > 85 && i.quality < 97)
                    {
                        totalMinDiscount += i.price_Discount_Effect_Max;
                    }
                    else if (i.quality > 70 && i.quality < 85)
                    {
                        totalMinDiscount += i.price_Discount_Effect_Max + i.price_Discount_Effect_Increase;
                    }
                }
            }
        }
        return car.originalPrice-(car.originalPrice / 100 * totalMinDiscount);
    }

    #endregion
    
    #region Util

    private void Init()
    {
        cars = FindObjectsOfType<Car>();
    }

    #endregion
}