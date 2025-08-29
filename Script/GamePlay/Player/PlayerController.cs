using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJump;

    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;
    [SerializeField] KeyCode _jumpKey;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _airMultiplayer;
    [SerializeField] private bool _isJumping;

    [SerializeField] private float _jumpCooldown;

    




    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] LayerMask _groundLayer;


    
    public bool isInputLocked = true; // Başta hareket kapalı


    private StateControl _stateControl;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _stateControl = GetComponent<StateControl>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isInputLocked) return;
        PlayerInputs();
        States();
    }

    private void FixedUpdate()
    {
        if (isInputLocked) return;
        PlayerMovement();
    }

    private void PlayerInputs()
    {
        //inputlar çekildi
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKey(_jumpKey) && _isJumping && IsGrounded())
        {
            _isJumping = false;
            PlayerJump();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }


    }



    private void States()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        
        var currentState = _stateControl.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded  => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded => PlayerState.Sneaking,
           
            _ when !_isJumping && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateControl.SetState(newState);
        }
        Debug.Log(newState);

    }

    private void PlayerMovement()
    {
        _moveDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;


        float forceMultplayer = _stateControl.GetCurrentState() switch
        {

            PlayerState.Sneaking => 1f,
            
            PlayerState.Jump => _airMultiplayer,
            _ => 1f
        };
        _playerRigidbody.AddForce(_moveDirection.normalized * _movementSpeed * forceMultplayer, ForceMode.Force);
    }


    private void PlayerJump()
    {
        OnPlayerJump?.Invoke();
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);//hareketin y eksenini sıfırlama(zıplamadan önce)
        _playerRigidbody.AddForce(transform.up * _jumpSpeed, ForceMode.Impulse); //ani hareketiyle zıplama
    }

    private void ResetJump()
    {
        _isJumping = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _moveDirection.normalized;
    }
    
    public void SetGameOver(bool isGameOver)
{
    isInputLocked = isGameOver;
}


 


}