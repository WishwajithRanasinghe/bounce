using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private TMP_Text _ringsText;
    [SerializeField] private TMP_Text _lifesText;
    [SerializeField] private TMP_Text _scoreText;
    private  BallCollision _ballScript;
    private int _rings;
    private int _lifes;
    private int _score;
    private void Start()
    {
        _ballScript = _ball.GetComponent<BallCollision>();
        
    }

    private void Update()
    {
        _rings = _ballScript.ringsForWin;
        _lifes = _ballScript.helth;
        _score = _ballScript.score;
        UpdateText();
    }
    private void UpdateText()
    {
        _ringsText.text = "X " +_rings.ToString();
        _lifesText.text = "X " +_lifes.ToString();
        _scoreText.text = _score.ToString();
    }
}
