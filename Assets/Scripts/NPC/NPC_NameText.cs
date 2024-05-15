using TMPro;
using UnityEngine;

public class NPC_NameText : MonoBehaviour
{
    #region Vars

    private Camera _camera;

    #endregion

    #region Unity Funcs.

    private void Start()
    {
        _camera = Camera.main;
        GetComponent<TextMeshProUGUI>().text = GetComponentInParent<NPC>()._name;
    }

    private void Update()
    {
        transform.LookAt(_camera.transform.position);
    }

    #endregion
}