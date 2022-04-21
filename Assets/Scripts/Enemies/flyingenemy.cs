using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class flyingenemy : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject player;
    AIPath myPath;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myPath = GetComponent<AIPath>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        chaseplayer();
    }

    void chaseplayer()
    {
        /* Alternativa 1: vector2.distance
        float d = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log("Distancia con jugador: " + d);
        if (d < 8)
        {

        }
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        */
        // Alternativa 2: overlapcircle

        Collider2D col = Physics2D.OverlapCircle(transform.position, 5f, LayerMask.GetMask("Player"));
        
        if (col != null)
        {
            myPath.isStopped = false;
        }
        else
        {
            myPath.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(3))
        {
            health -= 1;
            
        }
        if(health == 0)
        {
           
            StartCoroutine("dead");
           
        }
        
    }
    IEnumerator dead()
    {
        gameObject.layer = 9;
        anim.SetBool("Kill", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }
}
