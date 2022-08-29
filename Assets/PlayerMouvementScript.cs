using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvementScript : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] Transform _root;
    
    [SerializeField] Rigidbody2D _rb;
    


    [SerializeField] float  _jumpForce;
    [SerializeField] float _speed;
    [SerializeField] float _movingTreshold;

    [Header("Animator")]
    [SerializeField] Animator _animator;


    [Header("IsGrounded")]
    [SerializeField] float _rayCastLength;
    [SerializeField] Transform _footPoint;
    bool _isGrounded = true;

    Vector2 _playerMouvement;

    private void Start()
    {
        //Movement
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += EndMove;

        //Jump
        _jumpInput.action.started += StartJump;
    }

    private void FixedUpdate()
    {
        //Mouvement
        Vector2 _direction = new Vector2(_playerMouvement.x, 0);
        _root.transform.Translate(_direction * Time.fixedDeltaTime * _speed, Space.World);

        //Animator
        if(_direction.magnitude > _movingTreshold)
        {
           _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        //Orientation Player
        if(_direction.x > 0)  //Côté droit
        {
            _root.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(_direction.x < 0)                  //Côté gauche
        {
            _root.rotation = Quaternion.Euler(0, 180, 0);
        }

        //Détection du sol

        Debug.DrawRay(_footPoint.position, Vector2.down * _rayCastLength, Color.green, 1f);
        RaycastHit2D hit = Physics2D.Raycast(_footPoint.position, Vector2.down, _rayCastLength);

        if(hit.collider == null)
        {
            Debug.Log("Plouf dans l'eau");
            _isGrounded = false;
        }
        else
        {
            Debug.Log("Touché");
        }
    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        _playerMouvement = obj.ReadValue<Vector2>();
        //_animator.SetBool("isWalking", true);
        Debug.Log($"Touche enfoncée {_playerMouvement}");

    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        Debug.Log($"Joystick Update {_playerMouvement}");
    }

    private void EndMove(InputAction.CallbackContext obj)
    {
        _playerMouvement = new Vector2(0, 0);
        //_animator.SetBool("isWalking", false);
        Debug.Log("Joystick Neutre");
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        if(_isGrounded)
        {
            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        
    }

}
