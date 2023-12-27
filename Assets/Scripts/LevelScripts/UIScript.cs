using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private TMP_Text _ringsText;
    [SerializeField] private TMP_Text _lifesText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private GameObject _gameOver;
    
    private  BallCollision _ballScript;
    private int _rings;
    private int _lifes;
    private int _score;
    private void Start()
    {
        _ballScript = _ball.GetComponent<BallCollision>();
        _gameOver.SetActive(false);
        Time.timeScale = 1;
        
    }
    

    private void Update()
    {
        _rings = _ballScript.ringsForWin;
        _lifes = _ballScript.helth;
        _score = _ballScript.score;
        UpdateText();
    }
    public void GameOver()
    {
        _gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    private void UpdateText()
    {
        _ringsText.text = "X " +_rings.ToString();
        _lifesText.text = "X " +_lifes.ToString();
        _scoreText.text = _score.ToString();
        _levelName.text = "Level = " + SceneManager.GetActiveScene().buildIndex.ToString(); 
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        _gameOver.SetActive(false);
        
    }//
    
}
