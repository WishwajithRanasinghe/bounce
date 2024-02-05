using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private int levelNo;
    [SerializeField] private GameObject _gameOveMenu;
    private void Start()
    {
        _gameOveMenu.SetActive(false);
    }
    private void Update()
    {
        levelNo = DataPass._levelNO;
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt("LevelNO",levelNo);
            DataPass._levelNO = SceneManager.GetActiveScene().buildIndex;
        }
        
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
        levelNo = 1;
        
    }
    public void Continev()
    {
        SceneManager.LoadScene(levelNo);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

}
