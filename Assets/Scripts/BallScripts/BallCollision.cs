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
    [SerializeField] private LayerMask _whatLayer;
    [SerializeField] private Sprite _reSpawnSprite,_openGate;
    
    [SerializeField] private float  _ypos,_yBigPos;
    private float _startSeveTime;
    private Vector2 _size;
    private BallScript _mainScript;
    public bool needFlip;

    private Collider2D[] _checkLayer;
    public bool isGrounded,isFast,isHighJump,isBig,isSmallRing,isBigRing,isBounce; 
    public int score;
    public int helth = 5,ringsForWin = 5;
    public Vector2 spawnPos;   
    public bool lastCheckSize;

    
    private void Start()
    {
        _mainScript = GetComponent<BallScript>();
        _startSeveTime = _powerTime;
       
    }//start
    private void CkeckGround()
    {
        _size = new Vector2( GetComponent<CircleCollider2D>().radius/2,GetComponent<CircleCollider2D>().radius+ 0.01f);
        if(isBig == true)
        {
            _checkLayer = Physics2D.OverlapBoxAll(new Vector2(transform.position.x,transform.position.y -_yBigPos),_size,0f,_whatLayer);
        }
        else
        {
            _checkLayer = Physics2D.OverlapBoxAll(new Vector2(transform.position.x,transform.position.y -_ypos),_size,0f,_whatLayer); 
        }
        //Collider2D[] _checkLayer = Physics2D.OverlapBoxAll(new Vector2(transform.position.x,transform.position.y -_ypos),_size,0f,_whatLayer);

        for (int i = 0; i < _checkLayer.Length; i++)
        {
            if(_checkLayer[i].transform.tag == "Ground" /*&& _checkLayer[i].transform.position.y < transform.position.y*/)
            {
                isGrounded = true;
               Debug.Log("ela ela"); 
            }
            else if(_checkLayer[i].transform.tag == "Bounce")
            {
                isBounce = true;
            }    
        }
    }
    
    private void Update()
    {
        CkeckGround();
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
    }
    private void winGate()
    {
        if(ringsForWin == 0)
        {
            _winGameObject.GetComponent<SpriteRenderer>().sprite = _openGate;
            _winGameObject.GetComponent<SpriteRenderer>().flipX = needFlip;

        }

    }
    private void OnDrawGizmosSelected()
    {
        
        if(isBig == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y - _yBigPos),new Vector3( GetComponent<CircleCollider2D>().radius/2,GetComponent<CircleCollider2D>().radius + 0.1f,1f));
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y - _ypos),new Vector3( GetComponent<CircleCollider2D>().radius/2,GetComponent<CircleCollider2D>().radius + 0.1f,1f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.transform.tag)
        {
            case "Ground":
            
            
            break;

            case "EXJump":
            isHighJump = true;
            isGrounded = true;
            break;

            case "EXSpeed":
            isFast = true;
            isGrounded = true;
            break;

            case "AirPump":
            isBig = true;
            isGrounded = true;
            break;

            case "AirRelis":
            isBig = false;
            isGrounded = true;
            break;

            case "SmallRing":
            isGrounded = true;
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
            isGrounded = true;
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
                
                Debug.Log("Next");
            }
            break;
            default:
            if(helth > 0)
            {
                _mainScript.ReBornBall();
                isBig = lastCheckSize;
                helth--;
            }
            
            else
            Debug.Log ("gameover!");
            break;
        }
        
         

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isGrounded = false;
        }
        if(collision.transform.tag == "Bounce")
        {
            isBounce = false;
            isGrounded = true;
            isGrounded = false;
        }

    }
    public void JumpPad()
    {
        isGrounded = true;
        Invoke(nameof(RelesJump),0.01f);

    }
    public void RelesJump()
    {
        isGrounded = false;
    } 


}//class