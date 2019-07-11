using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    internal float moveSpeed = 300;
    [SerializeField]
    internal float jumpSpeed = 300;
    [SerializeField]
    internal float jumpTime = 0.5f;

    internal float jumpTimeCounter;
    private TextMeshProUGUI hpNum;
    private TextMeshProUGUI ptNum;

    [SerializeField]
    internal int hp = 3;
    [SerializeField]
    internal int points = 0;
    private Vector3 holdPos;

    private Rigidbody2D rigid;
    private Animator anim;

    void Start()
    {
        ptNum = GameObject.Find("points").GetComponent<TextMeshProUGUI>();
        hpNum = GameObject.Find("life").GetComponent<TextMeshProUGUI>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        holdPos = transform.position;
    }

    void Update()
    {
        if (holdPos.x + 1000 < transform.position.x && moveSpeed < 600)
        {
            holdPos = transform.position;

            Debug.Log("speed  " + moveSpeed);
            moveSpeed += 10;
            jumpSpeed += 5;
            jumpTime -= 0.00003f;
            rigid.gravityScale += 5;

        }

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
        ptNum.text = "points " + points;
        hpNum.text = "X " + hp;
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
