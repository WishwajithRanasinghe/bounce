using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private float _xMax,_xMini,_yMax,_yMini;
    private Vector3 _position;
    
    
    private void Start()
    {
        _position = transform.position;
    }//Start

    private void Update()
    {
        _position = new Vector3(_ball.transform.position.x,_ball.transform.position.y,transform.position.z);
        _position.x = Mathf.Clamp(_position.x,_xMini,_xMax);
        _position.y = Mathf.Clamp(_position.y,_yMini,_yMax);
        transform.position = _position;
        
    }
}
