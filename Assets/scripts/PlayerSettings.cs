using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    internal float moveSpeed = 10;
    [SerializeField]
    internal float jumpSpeed = 10;
    [SerializeField]
    internal float jumpTime = 0.5f;

    internal float jumpTimeCounter;
    private TextMeshProUGUI hpNum;

    [SerializeField]
    internal int hp = 3;


    private Rigidbody2D rigid;
    private Animator anim;

    void Start()
    {
        hpNum = GameObject.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }

    void Update()
    {
        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && IsOnGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (jumpTimeCounter > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)))
        {
            jumpTimeCounter = 0;
        }


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
            jumpTimeCounter = jumpTime;
            anim.SetBool("jumpUp", false);
            anim.SetBool("jumpDown", false);
        }
    }

    private void FixedUpdate()
    {
        hpNum.text = "X "+hp;
        if (hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
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
