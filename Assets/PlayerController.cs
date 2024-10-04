using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] int speed = 0;
    bool grounded = false;
    bool tely = false;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed*movementVector.x,rb.velocity.y);
    }

    void OnMove(InputValue value)
    {
        //float x = movementVector.x;
        //float y = movementVector.y;
        
        movementVector = value.Get<Vector2>();
        


    }

    void OnTP()
    {
        if (tely)
        {
            Debug.Log("tp");
            rb.transform.position = new Vector2(9.8f, -2.56f);
        }
    }

    void OnDash()
    {
        if (movementVector.x == 1 && movementVector.y == 0)
        {
            rb.AddForce(new Vector2(2500, 0));
            ps.Play();
        }else if (movementVector.x == -1 && movementVector.y == 0)
        {
            rb.AddForce(new Vector2(-2500, 0));
            ps.Play();
        }
        


    }

    void OnJump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector2(0, 320));
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")){
            grounded = true;
        }
        //rb.AddForce(new Vector2(0, 500));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TP"))
        {
            tely = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TP"))
        {
            tely = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
        else if (collision.gameObject.CompareTag("TP"))
        {
            tely = false;
        }
    }
}
