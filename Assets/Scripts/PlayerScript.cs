using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;

    [SerializeField]
    public Rigidbody2D rb;

    public bool isAlive;
    public GameObject explosion, impact;
    public AudioSource audioSource;
    public AudioClip scoreClip;
    public GameObject[] scaryPics;
    public bool shieldActive = false;
    public GameObject shieldVisual;
    public float invincibleTime = 2f;
    public float flashSpeed = 0.1f;
    public Transform respawnPoint;
    public GameObject spawnRings;

    private float moveSpeed = 2f;
    private float bounceSpeed = 4f;
    private Button flapButton;
    private bool didFlap;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    
    int randScore;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        isAlive = true;
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => flapPlayer());
        randScore = Random.Range(20, 800);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
        spawnRings = GameObject.Find("SpawnRings");
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
                rb.linearVelocity = new Vector2(0, bounceSpeed);
            }
        }
        if(transform.position.y >= 5.6f){
            TakeDamage();
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
            TakeDamage();
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
            TakeDamage();
        }
        if(collision.tag == "Ground"){
	    rb.linearVelocity = new Vector2(0, bounceSpeed);
            if (isInvincible)
            {
                rb.linearVelocity = new Vector2(0, bounceSpeed);
            }
            GameObject explode = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explode, 0.8f);
            TakeDamage();
        }
        if(collision.tag == "RingGroup"){
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage()
    {
        if (isInvincible || GameManager.instance.isDead)
        {
            return;
        }
        if (shieldActive)
        {
            DeactivateShield();
            StartCoroutine(Invincibility());
            return;
        }
        GameManager.instance.currentLives--;
        if (GameManager.instance.currentLives > 0){
            transform.position = respawnPoint.position + new Vector3(transform.position.x, 0, transform.position.z);
            Time.timeScale = 0f;
            GameManager.instance.tapToStart.gameObject.SetActive(true);
            GameManager.instance.pauseButton.gameObject.SetActive(false);
            StartCoroutine(Invincibility());
        }
        else
        {
            Destroy(spawnRings);
            Destroy(this.gameObject);
            GameManager.instance.pauseButton.gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
        GameManager.instance.UpdateUI();
    }

    public void ActivateShield()
    {
        if (GameManager.instance.currentShields <= 0)
        {
            return;
        }
        GameManager.instance.currentShields--;
        shieldActive = true;
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(true);
        }
        GameManager.instance.UpdateUI();
    }

    void DeactivateShield()
    {
        shieldActive = false;
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashSpeed);
            timer += flashSpeed;
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    public void ScaryPictures(){
        int rand = Random.Range(0, 5);
        Vector3 picsPosition = new Vector3(transform.position.x, transform.position.y);
        GameObject pics = (GameObject)Instantiate(scaryPics[rand], picsPosition, transform.rotation);
        Destroy(pics, 2.5f);
    }
}
