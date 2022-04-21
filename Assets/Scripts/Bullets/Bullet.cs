using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator myanim;
    [SerializeField] float speedX;
    [SerializeField] float direction;

    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
           
            StartCoroutine("Dest");
           

        }

        

    }
    IEnumerator Dest()
    {
        myanim.SetBool("destroy", true);
        body.constraints = (RigidbodyConstraints2D)RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        
            body.velocity = new Vector2(direction * speedX, body.velocity.y);
        
        
    }
    
   
}
