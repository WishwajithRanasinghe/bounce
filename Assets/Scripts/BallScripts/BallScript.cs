using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float _jumpforce = 5f,_extraJumpForce = 10f;
    [SerializeField] private float _moveSpeed = 10f,_extraSpeed = 50f;
    private float _gravityScale;
    private Vector2 _startPos;
    
    
    private Rigidbody2D _rBody;
    private Vector2 _direction,_ballScale;
    private BallCollision _collisionScript;

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _collisionScript = GetComponent<BallCollision>();
        _ballScale = transform.localScale;
    }
    private void Update()
    {
        GetInput();
        BallSize(_collisionScript.isBig);
    }//Update
    private void FixedUpdate()
    {
        
        Movement();

    }//FixedUpdate
    private void GetInput()
    {
        if(_collisionScript.isFast == true)
        {
            _direction.x = Input.GetAxis("Horizontal")* _extraSpeed;
        }
        else
        {
            _direction.x = Input.GetAxis("Horizontal")*_moveSpeed;
        }
        
        
        if(_collisionScript.isGrounded == true)
        {
            _direction.y = Mathf.Max(_direction.y,-1);
        }
        else if(_collisionScript.isBounce == true)
            _direction.y = Mathf.Max(_direction.y, -10f);
        else
        {
            _direction += Physics2D.gravity * Time.deltaTime * _gravityScale;
        }
        if(Input.GetKeyDown(KeyCode.Space) && _collisionScript.isGrounded == true || _collisionScript.isBounce == true)
        {
            Jump();
            //Jump2();
        }

    }//GetInput
    private void Movement()
    {
        _rBody.MovePosition(_rBody.position+_direction* Time.fixedDeltaTime);
    }
    
    private void Jump()
    {
        if(_collisionScript.isHighJump == true)
        {
            _direction = Vector2.up * _extraJumpForce;
            _collisionScript.isGrounded = false;
        }
        else
        {
            _direction = Vector2.up * _jumpforce;
            _collisionScript.isGrounded = false;
        }
        

    }
    
    public void ReBornBall()
    {
        if(_collisionScript.spawnPos == null)
        {
            _rBody.position = _startPos;
        }
        else
        {
            _rBody.position = _collisionScript.spawnPos;
           // BallSize(_collisionScript.lastCheckSize);
            _collisionScript.isGrounded = false;
            
        }
  
    }
    private void Jump2()
    {
        _rBody.velocity = Vector2.up * _jumpforce;
    }
    public void BallSize(bool isBig)
    {
        if(isBig == true)
        {
            transform.localScale = _ballScale * 2;
            _gravityScale = 4f;
        }
        else
        {
            transform.localScale = _ballScale * 1;
            _gravityScale = 5;
        }
        
      
    }//BallSize

    
}//Class
