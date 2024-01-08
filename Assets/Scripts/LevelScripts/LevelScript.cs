using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    [SerializeField] private GameObject[] _rings;
    [SerializeField] private bool _isNeedFlipX;
    [SerializeField] private bool _isBig;
    private BallCollision _ballScript;
    private BallMoveScript _moveScript;
    private void Start()
    {
        _ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallCollision>();
        _moveScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMoveScript>();
        _moveScript._isBig = _isBig;
        _ballScript.isBig = _isBig;
        _ballScript._big = _isBig;
        _ballScript.ringsForWin = _rings.Length;
        _ballScript.needFlip = _isNeedFlipX;
       
    }
}
