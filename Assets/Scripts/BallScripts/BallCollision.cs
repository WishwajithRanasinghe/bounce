using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallCollision : MonoBehaviour
{
    [SerializeField] private float _powerTime = 2f;
    [SerializeField] private GameObject _winGameObject;
    [SerializeField] private Sprite _reSpawnSprite,_openGate;
    [SerializeField] private GameObject _scenetransition;
    private float _startSeveTime;
    private BallMoveScript _mainScript;
    public bool needFlip;
    public bool isFast,isHighJump,isBig,isSmallRing,isBigRing,isWater; 
    public bool _big;
    public int score;
    public int helth = 5,ringsForWin = 5;
    public Vector2 spawnPos;   
    public bool lastCheckSize;

    private AudioScript _audio;
    private UIScript _uIScript;
    
    private void Start()
    {
        _mainScript = GetComponent<BallMoveScript>();
        _winGameObject.GetComponentInChildren<Light2D>().color = Color.red;
        _audio = GetComponent<AudioScript>();
        _startSeveTime = _powerTime;
        _uIScript = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        spawnPos = Vector2.zero;
        _scenetransition.SetActive(true);
        helth = DataPass._health;
        score = DataPass._score;

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
        DataPass._health = helth;
        DataPass._score = score;
        
    }
    private void winGate()
    {
        if(ringsForWin == 0)
        {
            _winGameObject.GetComponent<SpriteRenderer>().sprite = _openGate;
            _winGameObject.GetComponent<SpriteRenderer>().flipX = needFlip;
            _winGameObject.GetComponentInChildren<Light2D>().color = Color.blue;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.transform.tag)
        {
           
            case "EXSpeed":
            isFast = true;
            _audio.Life();

            break;

            case "AirPump":
            if(isBig != true)
            _audio.AirUp();
            isBig = true;
            

            break;

            case "AirRelis":
            if(isBig != false)
            _audio.AirDown();
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
                _audio.Collector();
                if(collision.gameObject.GetComponentInChildren<Light2D>() != null)
                {
                    collision.gameObject.GetComponentInChildren<Light2D>().enabled = false;
                }
            }
            if(collision.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                _audio.Collector();
                if(collision.gameObject.GetComponentInChildren<Light2D>() != null)
                {
                    collision.gameObject.GetComponentInChildren<Light2D>().enabled = false;
                }
            }
            
            
            
            break;
            
            case "BigRing":

            isBigRing = true;
            score += 50;
            ringsForWin --;
            if(collision.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                _audio.Collector();
                if(collision.gameObject.GetComponentInChildren<Light2D>() != null)
                {
                    collision.gameObject.GetComponentInChildren<Light2D>().enabled = false;
                }
            }
            if(collision.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
                _audio.Collector();
                if(collision.gameObject.GetComponentInChildren<Light2D>() != null)
                {
                    collision.gameObject.GetComponentInChildren<Light2D>().enabled = false;
                }
            }
            
            
            break;
            case "ReSpawner":
                spawnPos = collision.transform.position;
                _audio.Collector();

                if(collision.gameObject.GetComponent<CircleCollider2D>() != null)
                {
                    collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                    if(collision.gameObject.GetComponentInChildren<Light2D>() != null)
                    {
                        collision.gameObject.GetComponentInChildren<Light2D>().color = Color.green;
                    }
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
                _audio.Life();
                helth ++;
                score += 100;
                Destroy(collision.gameObject);
                
            break;
            case "Next":
            if(ringsForWin <= 0)
            {
                
                _audio.WinGame();
                _scenetransition = GameObject.FindGameObjectWithTag("Scene");
                _scenetransition.GetComponent<LevelTransition>().LoadNext();
                Debug.Log("Next");
            }
            break;
            case "Damage":
            if(helth > 0)
            {
                _mainScript.ReBornBall();
                _audio.ReSpawn();
                if(spawnPos == Vector2.zero)
                {
                    isBig = _big;
                }
                else
                {
                    isBig = lastCheckSize;
                }
                
                helth--;
            }
            
            else
            {
                _scenetransition.SetActive(false);
                Debug.Log ("gameover!");
                _audio.GameOver();
                _uIScript.GameOver();
            }
                


            break;

            default:

            
            break;
        }

    }

}//class
