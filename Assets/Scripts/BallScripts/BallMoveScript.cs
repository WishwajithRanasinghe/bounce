using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BallMoveScript : MonoBehaviour
{ 
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private  Transform groundCheck;
    [SerializeField] private  LayerMask groundLayer;
    [SerializeField] private  LayerMask waterLayer;
    private Vector2 _ballScale;
    private Vector2 _startPos;

    private Rigidbody2D _rbody;
    private bool _isGrounded,_isWater;
    private float _horizontalInput;
    private float _startGravityScale;
    private BallCollision _collisionScript;
    private AudioScript _audio;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _collisionScript = GetComponent<BallCollision>();
        _audio = GetComponent<AudioScript>();
        _startGravityScale = _rbody.gravityScale;
        _ballScale = transform.localScale;
        _startPos = _rbody.position;
    }
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _rbody.velocity = new Vector2(_horizontalInput*_moveSpeed * Time.deltaTime,_rbody.velocity.y);
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        _isWater = Physics2D.OverlapCircle(groundCheck.position, -1f, waterLayer);
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(_collisionScript.isBig == true && _isWater == true)
        {
            _rbody.gravityScale = -_startGravityScale;
        }
        else
        {
            _rbody.gravityScale = _startGravityScale;
        }
    }

    private void Jump()
    {
        _audio.Jump();
        _rbody.velocity = new Vector2(_rbody.velocity.x * _horizontalInput * Time.deltaTime, 0f);
        _rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }//Jump
    private void Water()
    {
        if(_collisionScript.isBig == true)
        {

        }
    }
    public void ReBornBall()
    {
        if(_collisionScript.spawnPos == null)
        {
            _rbody.position = _startPos;
        }
        else
        {
            _rbody.position = _collisionScript.spawnPos;
            
        }
  
    }//ReBornBall
    public void BallSize(bool isBig)
    {
        if(isBig == true)
        {
            transform.localScale = _ballScale * 2;
           
        }
        else
        {
            transform.localScale = _ballScale * 1;
            
        }
        
      
    }//BallSize
}//Class
