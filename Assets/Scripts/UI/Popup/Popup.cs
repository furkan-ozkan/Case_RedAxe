using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private Image _background;

    public void ShowPopup(string message, Color color)
    {
        transform.DOScale(Vector3.one, .25f);
        _message.text = message;
        _background.color = color;
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1.75f);
        transform.DOScale(Vector3.zero, .25f);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}