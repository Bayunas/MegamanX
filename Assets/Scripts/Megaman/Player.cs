using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int jumpforce;
    [SerializeField] float delta=1;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    [SerializeField] AudioClip shotA;
    [SerializeField] AudioClip killed;
    Rigidbody2D myBody;
    private bool die;
    private float disparar = 0f;
    private new AudioSource audio;
    Animator myAnim;
    bool isGrounded = true; 
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        //StartCoroutine(miCorutina());
    }
    IEnumerator miCorutina()
    {
        while (true)
        {
            Debug.Log("Esperando 4 segundos");
            yield return new WaitForSeconds(4);
            Debug.Log("Pasaron 4 segundos");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.3f,Color.red);
        isGrounded = (ray.collider != null);
        Jump();
        Fire();
        FinishingRun();
    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > disparar)
        {
            
            BulletA();
            StartCoroutine("Shoot");
            if (this.transform.localScale.x == 1)
            {
                disparar = Time.time + delta;
                Instantiate(bullet, transform.position, transform.rotation);
            }
            if (this.transform.localScale.x == -1)
            {
                
                disparar = Time.time + delta;
                Instantiate(bullet2, transform.position, transform.rotation);
            }
        }
        
    }

    IEnumerator Shoot()
    {
        myAnim.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(0.8f);
        myAnim.SetLayerWeight(1, 0);
    }

    void FinishingRun()
    {
        Debug.Log("Termina animacion de correr");
    }

    void Jump()
    {
        if (isGrounded)
        {
          
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Saltando!");
                myBody.AddForce(new Vector2(0, jumpforce),ForceMode2D.Impulse);
               
            }
        }
        if(myBody.velocity.y != 0 && !isGrounded)
        {
            myAnim.SetBool("IsJumping", true);
        } else
        {
            myAnim.SetBool("IsJumping", false);
        }

    }

    private void FixedUpdate()
    {
        death();
    }

    void death()
    {
        if(die == false)
        {
            float dirH = Input.GetAxis("Horizontal");

            if (dirH != 0)
            {
                myAnim.SetBool("ItsRunning", true);
                if (dirH < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                myAnim.SetBool("ItsRunning", false);
            }
            myBody.velocity = new Vector2(dirH * speed, myBody.velocity.y);
        }
        else
        {
            gameObject.layer = 8;
            myBody.constraints = (RigidbodyConstraints2D)RigidbodyConstraints.FreezePosition;
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 10 )
        {

            StartCoroutine("Restart");
          
          
          
        }
    }

    public void BulletA()
    {
        audio.Play();
    }

    public void DeathA()
    {
        audio.PlayOneShot(killed);
    }

    IEnumerator Restart()
    {
        myAnim.SetBool("Death", true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        DeathA();
        yield return new WaitForSecondsRealtime(1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;


    }

   


}


