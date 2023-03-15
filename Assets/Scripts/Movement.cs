using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField]private float spinSpeed, thrust = 50f, defaultSpinSpeed = 120f;
    public GameObject Shadow;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb2D.velocity.x) < 1)
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        BallMove();
        SpinBall();
        ShadowMovement();
    }

    public void ShadowMovement()
    {
        Shadow.transform.position = new Vector3(transform.position.x, Shadow.transform.position.y, 10f);
    }

    private void BallMove()
    {
        if (GameManager.instance.BallMovement)
        {
            rb2D.isKinematic = false;
            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.instance.PerfectScore == false)
                {
                    GameManager.instance.PerfectScore = true;
                }
                if (GameManager.instance.isAudioPlaying)
                {
                    GameManager.instance.BounceAudioSource.Play();
                }
                var point = Input.mousePosition;
                if (point.x > Screen.width / 2)
                {
                    rb2D.velocity = new Vector2(-0.25f, 1) * thrust;
                }
                else if (point.x < Screen.width / 2)
                {
                    rb2D.velocity = new Vector2(0.25f, 1) * thrust;
                }
            }
        }
        else
        {
            rb2D.velocity = Vector2.zero;
            rb2D.isKinematic = true;
        }
    }

    private void SpinBall()
    {
        if (Mathf.Abs(rb2D.velocity.x) > 0.5f)
        {
            spinSpeed += Mathf.Sqrt(Mathf.Abs(rb2D.velocity.x)) * spinSpeed * Time.deltaTime;
            spinSpeed = Mathf.Clamp(spinSpeed, defaultSpinSpeed, 280);
            if (GameManager.instance.direction)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * spinSpeed);
            }
            else
            {
                transform.Rotate(Vector3.back * Time.deltaTime * spinSpeed);
            }
        }
        else
        {
            spinSpeed = defaultSpinSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BackNet")
        {
            GameManager.instance.PerfectScore = false;
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.name == "BackNet")
    //    {
    //        GameManager.instance.PerfectScore = false;
    //    }
    //}
}
