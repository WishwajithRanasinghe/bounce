using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rBody;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float _jumpForce = 10f,forceTipe;
    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            forceTipe =1;

        }
        else if(Input.GetButtonUp("Jump"))
        {
            forceTipe = 0;
        }
        
        _rBody.velocity = new Vector2(horizontalInput*moveSpeed*Time.deltaTime,_jumpForce * forceTipe);
    }
}
