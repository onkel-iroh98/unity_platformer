using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float speed = 5;
    public float jump_height = 10;
    private bool isGrounded = false;
    private Vector3 rotation;
    private Rigidbody2D rb;
    private Animator anim;
    private Coin_Manager cm;

    public GameObject panel;
    public GameObject kamera;
    public GameObject daetheffect;
    private AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        cm = GameObject.FindGameObjectWithTag("text_coins").GetComponent<Coin_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            isGrounded = false; 
        }
        if (direction != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (direction < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -direction * Time.deltaTime);
        }
        if (direction > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        }
        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        kamera.transform.position = new Vector3(transform.position.x, 0, -10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if(collision.gameObject.tag == "enemy")
        {
            death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "coin_b")
        {
            Destroy(collision.gameObject);
            cm.addgold(1);
            am.Play("Coin");
        }
        if (collision.gameObject.tag == "coin_s")
        {
            Destroy(collision.gameObject);
            cm.addgold(3);
            am.Play("Coin");
        }
        if (collision.gameObject.tag == "coin_g")
        {
            Destroy(collision.gameObject);
            cm.addgold(5);
            am.Play("Coin");
        }
        if (collision.gameObject.tag == "spike")
        {
            death();
        }
        if (collision.gameObject.tag == "finish_flag")
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void death()
    {
        Instantiate(daetheffect, transform.position, Quaternion.identity);
        panel.SetActive(true);
        Destroy(gameObject);
    }
}
