using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;

    [SerializeField]
    public Rigidbody2D rb;

    private float moveSpeed = 2f;
    private float bounceSpeed = 4f;
    private Button flapButton;
    private bool didFlap;

    public bool isAlive;
    public GameObject explosion, impact;
    public AudioSource audioSource;
    public AudioClip scoreClip;
    public GameObject[] scaryPics;
    
    int randScore;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        isAlive = true;
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => flapPlayer());
        randScore = Random.Range(20, 800);
    }

    public void flapPlayer(){
        didFlap = true;
    }

    void FixedUpdate(){
        if (isAlive){
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
            if(didFlap){
                didFlap = false;
                rb.velocity = new Vector2(0, bounceSpeed);
            }
        }
        if(transform.position.y >= 5.6f){
            GameManager.instance.GameOver();
        }
    }

    void CameraX(){
        CameraMove.offSetX = (Camera.main.transform.position.x - transform.position.x);
    }

    public float GetPositionX(){
        return transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Destruction"){
            Destroy(this.gameObject);
            GameManager.instance.GameOver();
        }
        if (collision.tag == "Ring"){
            audioSource.PlayOneShot(scoreClip);
            GameObject imp = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            Destroy(imp, 1f);
            ScoreCount.instance.CountScore(1);
            if(ScoreCount.instance.countScore >= randScore){
                ScaryPictures();
            }
        }
        if(collision.tag == "Death"){
            Destroy(this.gameObject);
            GameManager.instance.GameOver();
        }
        if(collision.tag == "Ground"){
            Destroy(this.gameObject);
            GameObject explode = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explode, 0.8f);
            GameManager.instance.GameOver();
        }
        if(collision.tag == "RingGroup"){
            Destroy(collision.gameObject);
        }
    }

    public void ScaryPictures(){
        int rand = Random.Range(0, 5);
        Vector3 picsPosition = new Vector3(transform.position.x, transform.position.y);
        GameObject pics = (GameObject)Instantiate(scaryPics[rand], picsPosition, transform.rotation);
        Destroy(pics, 2.5f);
    }
}
