using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal float moveSpeed = 10;
    [SerializeField]
    internal float jumpSpeed = 10;

    private Rigidbody2D rigid;
    private Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
        if (!IsOnGround)
        {
            if (rigid.velocity.y < 0)
            {
                anim.SetBool("jumpUp", false);
                anim.SetBool("jumpDown", true);
            }
            else if (rigid.velocity.y > 0)
            {
                anim.SetBool("jumpUp", true);
                anim.SetBool("jumpDown", false);
            }
        }
        else
        {
            anim.SetBool("jumpUp", false);
            anim.SetBool("jumpDown", false);
        }
    }

    private void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButton(0)) && IsOnGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
        }
    }

    public bool IsOnGround
    {
        get
        {
            Vector2 gizmoPos = new Vector2(transform.position.x, transform.position.y - 20f);
            Collider2D col = Physics2D.OverlapCircle(gizmoPos, 2f, LayerMask.GetMask("ground"));

            return (col != null);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 gizmoPos = new Vector2(transform.position.x, transform.position.y - 20f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmoPos, 2f);
    }
}
