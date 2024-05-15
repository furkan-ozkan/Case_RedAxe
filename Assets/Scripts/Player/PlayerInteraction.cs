using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    #region Vars

    private PlayerControls _controls;
    private Camera mainCamera;
    private Vector3 cameraPosition;
    private Vector3 cameraForward;

    [Header("Settings")]
    [SerializeField] private bool _isActive = true;
    [SerializeField] private float _interactionDistance = 100f;

    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }
    void Update()
    {
        Interaction();
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
    
    #region Main Funcs.

    private void Interaction()
    {
        if (_isActive && _controls.Player.Interaction.triggered)
        {
            cameraPosition = mainCamera.transform.position;
            cameraForward = mainCamera.transform.forward;
        
            RaycastHit hit;
            if (Physics.Raycast(cameraPosition, cameraForward, out hit, _interactionDistance))
            {
                hit.transform?.gameObject.GetComponent<IInteractable>()?.Interact();
            }
        }
    }

    #endregion
    
    #region Utils

    private void Init()
    {
        _controls = new PlayerControls();
        mainCamera = Camera.main;
    }

    public void ActivateInteraction() => _isActive = true;
    public  void DeactivateInteraction() => _isActive = false;

    #endregion
}