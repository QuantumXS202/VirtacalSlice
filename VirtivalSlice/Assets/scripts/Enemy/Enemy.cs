using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState//checked
{
    Wander,

    Follow,

    Die
};

public class Enemy : MonoBehaviour
{


    GameObject player;//checked
    public EnemyState currState = EnemyState.Wander;//checked

    public float range;//checked
    public float speed;//checked
    private bool chooseDir = false;//checked
    private bool dead = false;//checked
    private Vector3 randomDir;//checked


    // public float moveSpeed = 2f;
    public Rigidbody2D _rb;


    public Animator animator;

    public float time = 0.0f;
    public float AddPointsPerSecond = 0.42f;

    public bool hit;

    Vector2 movement;

    

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");//checked
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // _rb.MovePosition(_rb.position + movement * speed * Time.fixedDeltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {



        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");
        movement.x = _rb.velocity.x;
        movement.y = _rb.velocity.y;

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

        
        Debug.Log(currState);
        

        switch(currState)
        {
            case(EnemyState.Wander):
                Wander();
            break;
            case(EnemyState.Follow):
                Follow();
            break;
            case(EnemyState.Die):
                
            break;
        }//checked

        if(IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }//checked
    }

    

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }//checked

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        // randomDir = new Vector3(0, 0, Random.Range(0, 360));
        // Quaternion nextRotation = Quaternion.Euler(randomDir);
        // transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }//checked

    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        
        if(IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        
    }

    void Follow()
    {
        // Vector3 target = player.transform.position - transform.position;
        // float angle = Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg;
        // transform.position += -transform.right * speed * Time.deltaTime;
        // transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        _rb.velocity = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }//checked

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "p_b")
        {
            hit = true;
        }
        else
        {
            hit = false;
        }
    }
    
}
