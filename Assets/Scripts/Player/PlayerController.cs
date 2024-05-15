using UnityEngine;

public class PlayerController : MonoBehaviour,IDataPersistence
{
    #region Vars
    
    private CharacterController _controller;
    private PlayerControls _controls;
    private Vector3 _velocity;
    private Vector2 _move;

    [Header("Settings And Check")]
    [SerializeField] private bool _isActive = true;
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private bool _isGrounded;
    
    [Header("General")]
    public Transform ground;
    public float distanceToGround = .4f;
    public LayerMask groundMask;

    #endregion

    #region Unity Funcs.

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        Gravity();
        PlayerMovement();
        Jump();
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

    private void Gravity()
    {
        if (_isActive)
        {
            _isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }

    private void PlayerMovement()
    {
        if (_isActive)
        {
            _move = _controls.Player.Move.ReadValue<Vector2>();

            Vector3 movement = (_move.y * transform.forward) + (_move.x * transform.right);
            _controller.Move(movement * _moveSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (_isActive)
        {
            if (_controls.Player.Jump.triggered && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }
        }
    }

    #endregion

    #region Utils

    private void Init()
    {
        _controls = new PlayerControls();
        _controller = GetComponent<CharacterController>();
    }

    public void ActivateController()
    {
        _isActive = true;
        _controller.enabled = true;
    }
    public  void DeactivateController()
    {
        _isActive = false;
        _controller.enabled = false;
    }

    #endregion

    #region IDataPersistence

    public void LoadData(GameData data)
    {
        transform.position = data.playerPos;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = transform.position;
    }

    #endregion
}