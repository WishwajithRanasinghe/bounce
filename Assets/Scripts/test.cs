using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody2D _rBody;
    [SerializeField]private float _moveSpeed = 10f;
    [SerializeField]private float _jumpforce = 10f ;
    private Vector2 _direction;
    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rBody.AddForce(Vector2.up*_jumpforce* Time.deltaTime);
        }
        
    }
    private void Movement()
    {
        _direction.x = Input.GetAxis("Horizontal")*_moveSpeed;
        _rBody.MovePosition(new Vector3(_direction.x,transform.position.y,0f));
    }
}
