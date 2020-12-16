using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;


    public Animator animator;

    public bool left;
    public bool right;
    public bool down;
    public bool up;

    


    private float lastFire;

    public float fireDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        animator.SetBool("Left", left);
        animator.SetBool("Right", right);
        animator.SetBool("Down", down);
        animator.SetBool("Up", up);

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            down = true;
        }
        else
        {
            down = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right = true;
        }
        else
        {
            right = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;
        }
        else
        {
            left = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if(right == true)
        {
            Debug.Log("poopoo");
        }
    }
    
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
        (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed, 0);
    }
}
