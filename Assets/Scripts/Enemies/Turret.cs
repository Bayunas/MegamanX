using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] float delta;
    [SerializeField] GameObject Turretbul;
    [SerializeField] float health;
    [SerializeField] AudioClip death;
   

    private Animator anim;
    private bool Shoot;
    float tmp = 0f;
    private new AudioSource audio;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray();
        Shooting();
    }

    void Shooting()
    {
       

        if (Shoot ==  true)
        {
            anim.SetBool("Shoot", true);
            if (Time.time > tmp)
            {
                tmp = Time.time + delta;
                Instantiate(Turretbul, transform.position, transform.rotation);
            }

        }
        else 
        {
            anim.SetBool("Shoot", false);
        }
    }

    void Ray()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, 13f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 15f, Color.blue);
        Shoot = (ray.collider != null);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(3))
        {
            health -= 1;

        }
        if (health == 0)
        {
            DeathSound();
            StartCoroutine("dead");

        }

    }
    public void DeathSound()
    {
        audio.PlayOneShot(death);
    }

    IEnumerator dead()
    {
        gameObject.layer = 9;
        anim.SetBool("Kill", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }

   
}
