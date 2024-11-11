using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer render;
    private Rigidbody2D rb;
    public Animator anim;

    private float speed = 10;
    private int playerDir = 1;
    public bool isJumping;

    public int hp = 100;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CameraMove();
        PLayerMove();
        if (!isJumping && Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetButtonDown("Fire1")) anim.SetTrigger("attack");
        CheckHP();
    }
    private void CheckHP()
    {
        if (hp <= 0)
        {
            
        }
    }
    private void PLayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        playerDir = x < 0 ? -1 : x > 0 ? 1 : playerDir;
        render.flipX = playerDir < 0 ? true : playerDir > 0 ? false : render.flipX;
        anim.SetBool("run", x != 0);

        Vector3 dir = new Vector3(transform.position.x + (x * speed), transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, dir, 6 * Time.deltaTime);
    }
    private void CameraMove()
    {
        Vector3 dir = new Vector3(transform.position.x + (2.5f * playerDir), Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, dir, 4 * Time.deltaTime);
    }
    private void Jump()
    {
        rb.AddForce(transform.up*400);
        isJumping = !isJumping;
        anim.SetTrigger("jump");
        anim.ResetTrigger("grounded");
    }
    private void Attack()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(new Vector3(transform.position.x + (1.2f * playerDir), transform.position.y + 0.4f, transform.position.z), 1f);
        if (colls.Length > 0)
        {
            foreach (Collider2D coll in colls)
            {
                if(coll.tag == "enemy")
                {
                    Debug.Log("Attack");
                }
            }
        }
    }
}
