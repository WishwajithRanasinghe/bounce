using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    [SerializeField] private float _maxDistance,_miniDistance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isHorizontal;
    private bool _isMini;
    private void Start()
    {
        
    }


    private void Update()
    {
        if(_isHorizontal == true)
        {
            MoveX();
        }
        else
        {
            MoveY();
        }
        
    }
    private void MoveX()
    {
        Vector3 _position = transform.position;
        
        
        if(_position.x <= _miniDistance)
        {
            _isMini = false;
        }
        if(_position.x >= _maxDistance)
        {
            _isMini = true;
        }
        if(_isMini == true)
        {
            _position += new Vector3(-_moveSpeed*Time.deltaTime,0,0);
        }
        else
        {
            _position += new Vector3(_moveSpeed*Time.deltaTime,0,0);
        }
        transform.position = _position;

    }
    private void MoveY()
    {
        Vector3 _position = transform.position;
        
        
        if(_position.y <= _miniDistance)
        {
            _isMini = false;
        }
        if(_position.y >= _maxDistance)
        {
            _isMini = true;
        }
        if(_isMini == true)
        {
            _position += new Vector3(0,-_moveSpeed*Time.deltaTime,0);
        }
        else
        {
            _position += new Vector3(0,_moveSpeed*Time.deltaTime,0);
        }
        transform.position = _position;

    }
}
