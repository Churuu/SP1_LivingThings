﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{//Påbörjad av Jonas Thunberg 2019-01-31
 //redigering 2019-02-11
    private Rigidbody2D rb2D;
    private Collider2D coll2D;


    [SerializeField] private string horizontalMoment = "Horizontal";
    [SerializeField] private string jumpAxis = "wJump";
    [Space]
    [SerializeField] private float speedVelocityHorizontal = 400f;
    [SerializeField] private float speedVelocityHorizontalJump = 150f;
    [SerializeField] private float jumpAddForce = 500f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float gizmoRange = 1f;

    private float horizontalInput;
    private Vector3 side;



    void Start()
    {
        coll2D = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        side = new Vector3(coll2D.bounds.size.x * 0.5f, 0f, 0f);
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis(horizontalMoment); //Höger Vänster styrning 
        VerticalMovmenent();
        JumpMovment();
    }


    private void FixedUpdate()
    {
        HorizontalMovmenent();
    }

    private bool Grounded()
    {

        RaycastHit2D hitMid = Physics2D.Raycast(transform.position, Vector2.down, gizmoRange);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - side, Vector2.down, gizmoRange);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + side, Vector2.down, gizmoRange);

        if (hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null)
            return true;

        return false;

    }


    private void JumpMovment()
    {
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton(jumpAxis))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //AddForce För att hoppa
    private void VerticalMovmenent()
    {
        if (Input.GetButtonDown(jumpAxis))
        {
            if (Grounded())
            {
                rb2D.AddForce(Vector2.up * jumpAddForce);
            }
        }
    }

    private void HorizontalMovmenent()
    {
        Vector2 movement = new Vector2(horizontalInput * (Grounded() ? speedVelocityHorizontal : speedVelocityHorizontalJump) * Time.deltaTime, rb2D.velocity.y);
        rb2D.velocity = movement;
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.down * gizmoRange);
    }
}
