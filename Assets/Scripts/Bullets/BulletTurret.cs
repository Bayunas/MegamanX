using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D body;

    [SerializeField] float speed;
    [SerializeField] float direction;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(direction * speed, body.velocity.y);
    }
}
