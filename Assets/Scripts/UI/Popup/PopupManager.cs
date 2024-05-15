using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public Transform popupParent;
    public GameObject popupPrefab;
    public Color warningColor;
    public Color successColor;

    public void ShowPopup(string message, Color color)
    {
        Popup instantPopup = Instantiate(popupPrefab,popupParent).GetComponent<Popup>();
        instantPopup.ShowPopup(message, color);
    }
}