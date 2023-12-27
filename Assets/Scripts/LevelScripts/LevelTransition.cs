using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator _animat;
    [SerializeField] private TMP_Text _next;
    void Start()
    {
        
    }
    public void LoadNext()
    {
        StartCoroutine(LoadLevel());
    }
    private IEnumerator LoadLevel()
    {
        
        _animat.SetTrigger("End");
        
        yield return new WaitForSeconds(1f);
 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        _animat.SetTrigger("Start");
    }


}
