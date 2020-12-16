using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Wander,

    Follow,

    Die
};

public class Enemy : MonoBehaviour
{


    GameObject player;
    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float speed;
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomDir;


    // public float moveSpeed = 2f;
    public Rigidbody2D rb;
    public Animator animator;

    public float time = 0.0f;
    public float AddPointsPerSecond = 0.42f;

    public bool hit;

    Vector2 movement;

    

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
         rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        if(IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(range)&& currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

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
    }

    void Follow()
    {
        Vector3 target = player.transform.position - transform.position;
        float angle = Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg;
        transform.position += -transform.right * speed * Time.deltaTime;

    }
}
