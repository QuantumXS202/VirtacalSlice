using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    public Animator animator;

    public float time = 0.0f;
    public float AddPointsPerSecond = 0.42f;

    public bool hit;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("time_passed", time);
        animator.SetBool("hit", hit);
        if (time >= 0.42f)
        {
            time = 0.0f;
        }
        time += AddPointsPerSecond * Time.deltaTime;  
    }

    
    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "e_b")
        {
            hit = true;
        }
        else
        {
            hit = false;
        }    
    }

}
