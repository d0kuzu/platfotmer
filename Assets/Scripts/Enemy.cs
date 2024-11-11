using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Player playerCS;
    private Animator anim;
    private SpriteRenderer render;

    private float speed = 6;
    private int enemyDir = 1;

    private float attackKD = 3;
    private bool isAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        playerCS = player.GetComponent<Player>();
    }

    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (attackKD < 3) attackKD += Time.deltaTime;
        if (!isAttack && Vector2.Distance(player.transform.position, transform.position) <= 6 && Vector2.Distance(player.transform.position, transform.position) > 2)
        {
            enemyDir = transform.position.x < player.transform.position.x ? 1 : transform.position.x > player.transform.position.x ? -1 : enemyDir;
            render.flipX = enemyDir < 0 ? true : enemyDir > 0 ? false : render.flipX;

            Vector3 dir = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, dir, 4 * Time.deltaTime);

            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        if (Vector2.Distance(player.transform.position, transform.position) <= 2)
        {
            if (attackKD >= 3 && !isAttack)
            {
                isAttack = true;
                anim.SetTrigger("attack");
            }
        }
    }
    private void Attack()
    {
        if(playerCS.hp > 0)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(new Vector3(transform.position.x + (1.5f * enemyDir), transform.position.y + 0.4f, transform.position.z), 1f);
            if (colls.Length > 0)
            {
                foreach (Collider2D coll in colls)
                {
                    if (coll.tag == "Player")
                    {
                        playerCS.hp -= 10;
                    }
                }
            }
        }
        isAttack = false;
        attackKD = 0;
    }
}
