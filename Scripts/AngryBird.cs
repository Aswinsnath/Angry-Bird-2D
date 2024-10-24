using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBird : MonoBehaviour
{

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool hasBeenLanunged;
    private bool shouldFaceVellocityDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        rb.isKinematic = true;
        circleCollider.enabled = false;
    }


    private void FixedUpdate()
    {
        if (!hasBeenLanunged && shouldFaceVellocityDirection)
        {
            transform.right = rb.velocity;
        }
       
    }

    public void Launchbird(Vector2 direction,float force)
    {
        rb.isKinematic=false;
        circleCollider.enabled=true;

        //appling the  force


        rb.AddForce(direction*force,ForceMode2D.Impulse);
        hasBeenLanunged = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVellocityDirection = false;
    }
}
