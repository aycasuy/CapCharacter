using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;
    private StateControl _stateControl;

    private void Awake()
    {
        _playerController=GetComponent<PlayerController>();
        _stateControl=GetComponent<StateControl>();
    }

    private void Start()
    {
        _playerController.OnPlayerJump +=PlayerController_OnPlayerJump;
    }

   
    private void Update()
    {
        SetPlayerAnimation();
    }

    private void PlayerController_OnPlayerJump()
    {
        _animator.SetBool("isJumping",true);
        Invoke(nameof(ResetJumping),0.5f);
    }

    private void ResetJumping()
    {
         _animator.SetBool("isJumping",false);
    }

    private void SetPlayerAnimation()
    {
        var currentState=_stateControl.GetCurrentState();
        switch (currentState)
        {
            case PlayerState.Idle:
                _animator.SetBool("isMoving", false);
                

                break;

            case PlayerState.Sneaking:
                _animator.SetBool("isMoving", true);
                

                break;

            case PlayerState.Die:
              _animator.SetTrigger("Die"); 
               break;

               

        }
    }

    
}
