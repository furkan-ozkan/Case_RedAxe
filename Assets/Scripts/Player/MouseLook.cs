using UnityEngine;
public class MouseLook : MonoBehaviour
{
    #region Vars
    
    private PlayerControls _controls;
    private Vector2 _mouseLook;
    private float _xRotation = 0f;
    private Transform _playerBody;
    
    [SerializeField] private bool _isMouseLookActive = true;
    [SerializeField] private float _mouseSensitivity = 100f;

    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        Look();
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

    private void Look()
    {
        if (_isMouseLookActive)
        {
            _mouseLook = _controls.Player.Look.ReadValue<Vector2>();

            float mouseX = _mouseLook.x * _mouseSensitivity * Time.deltaTime;
            float mouseY = _mouseLook.y * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation,-45,90);
        
            transform.localRotation = Quaternion.Euler(_xRotation,0,0);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    #endregion

    #region Utils

    private void Init()
    {
        _playerBody = transform.parent;
        _controls = new PlayerControls();
        HideAndLockCursor();
    }
    
    public void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void VisibleAndFreeCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableMouseMove() => _isMouseLookActive = false;
    public void EnableMouseMove() => _isMouseLookActive = true;

    #endregion

}