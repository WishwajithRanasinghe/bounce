using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class BallCollision : MonoBehaviour
{
    [SerializeField] private float _powerTime = 2f;
    [SerializeField] private GameObject _winGameObject;
    [SerializeField] private Sprite _reSpawnSprite,_openGate;
    private float _startSeveTime;
    private BallMoveScript _mainScript;
    public bool needFlip;
    public bool isFast,isHighJump,isBig,isSmallRing,isBigRing,isWater; 
    public int score;
    public int helth = 5,ringsForWin = 5;
    public Vector2 spawnPos;   
    public bool lastCheckSize;

    
    private void Start()
    {
        _mainScript = GetComponent<BallMoveScript>();
        _startSeveTime = _powerTime;
       
    }//start
   
    
    private void Update()
    {
        if(isFast == true || isHighJump == true )
        {
            _powerTime -= Time.deltaTime;
        }
        if(_powerTime <= 0)
        {
            _powerTime = _startSeveTime;
            isFast = false;
            isHighJump = false;
        }
        winGate();
        _mainScript.BallSize(isBig);
    }
    private void winGate()
    {
        if(ringsForWin == 0)
        {
            _winGameObject.GetComponent<SpriteRenderer>().sprite = _openGate;
            _winGameObject.GetComponent<SpriteRenderer>().flipX = needFlip;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.transform.tag)
        {
           
            case "EXSpeed":
            isFast = true;

            break;

            case "AirPump":
            isBig = true;

            break;

            case "AirRelis":
            isBig = false;

            break;

            case "SmallRing":

            if(isBig == true){return;}
            score += 50;
            isSmallRing = true;
            ringsForWin --;
            if(collision.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if(collision.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            }
            
            
            
            break;
            
            case "BigRing":

            isBigRing = true;
            score += 50;
            ringsForWin --;
            if(collision.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            if(collision.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            }
            
            
            break;
            case "ReSpawner":
                spawnPos = collision.transform.position;

                if(collision.gameObject.GetComponent<CircleCollider2D>() != null)
                {
                    collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                }
                if(collision.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite = _reSpawnSprite;
                    collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                score += 10;
                
                lastCheckSize = isBig;
            break;
            case "winGate":
                if(ringsForWin <= 0)
                {
                    Debug.Log("WinThisLevel!");
                }
            break;
            case "Helth":
                helth ++;
                score += 100;
                Destroy(collision.gameObject);
                
            break;
            case "Next":
            if(ringsForWin <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                Debug.Log("Next");
            }
            break;
            case "Damage":
            if(helth > 0)
            {
                _mainScript.ReBornBall();
                isBig = lastCheckSize;
                helth--;
            }
            
            else
            Debug.Log ("gameover!");
            break;

            default:

            
            break;
        }

    }

}//class
