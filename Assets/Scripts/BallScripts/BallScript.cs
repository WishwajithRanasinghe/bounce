using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float _jumpforce = 5f,_extraJumpForce = 10f;
    [SerializeField] private float _moveSpeed = 10f,_extraSpeed = 50f;
    
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private float _gravityScale;
    private Vector2 _startPos;
    
    
    private Rigidbody2D _rBody;
    private Vector2 _direction,_ballScale;
    private BallCollision _collisionScript;
    private bool isGrounded = false;

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
       
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }//GetInput
    private void Movement()
    {
        _rBody.MovePosition(_rBody.position+_direction* Time.fixedDeltaTime);
    }
    
    private void Jump()
    {
        _rBody.velocity = new Vector2(_rBody.velocity.x,0f);
        _rBody.AddForce(new Vector2(0f,_jumpforce),ForceMode2D.Impulse);
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
            //_collisionScript.isGrounded = false;
            
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
