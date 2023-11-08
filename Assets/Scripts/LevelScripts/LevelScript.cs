using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    [SerializeField] private GameObject[] _rings;
    [SerializeField] private bool _isNeedFlipX;
    [SerializeField] private bool _isBig;
    private BallCollision _ballScript;
    private void Start()
    {
        _ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallCollision>();
        _ballScript.ringsForWin = _rings.Length;
        _ballScript.needFlip = _isNeedFlipX;
        _ballScript.isBig = _isBig;
    }
}
