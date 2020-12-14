using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{

    private bool hit;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        hit = true;
        Invoke("DestroyIt", 0.5f);
    }

    public void DestroyIt()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("hit", hit);
    }
}
