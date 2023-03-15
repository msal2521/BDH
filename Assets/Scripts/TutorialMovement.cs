using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField]private float spinSpeed, thrust = 50f, defaultSpinSpeed = 120f;
    public GameObject Shadow, TutorialPanelLeft, TutorialPanelRight, TapLText, TapRText, FinishedTxt, TapAnyWhereTxt;
    public bool Left = true, Right;
    public int i, j;
    private bool direction;

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
        //SpinBall();
        ShadowMovement();
    }

    public void ShadowMovement()
    {
        Shadow.transform.position = new Vector3(transform.position.x, Shadow.transform.position.y, 10f);
    }

    private void BallMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //GameManager.instance.BounceAudioSource.Play();
            var point = Input.mousePosition;
            if (point.x < Screen.width / 2 && Left)
            {
                rb2D.velocity = new Vector2(0.25f, 1) * thrust;
                i++;
                if (i == 1)
                {
                    TapLText.GetComponent<Text>().text = "Tap One More time !!";
                    TutorialPanelRight.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    TutorialPanelLeft.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
                if (i == 2)
                {
                    TapLText.SetActive(false);
                    TapRText.SetActive(true);
                    Left = false;
                    Right = true;
                    TutorialPanelRight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    TutorialPanelLeft.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
            }
            else if (point.x > Screen.width / 2 && Right)
            {
                rb2D.velocity = new Vector2(-0.25f, 1) * thrust;
                j++;
                if (j == 1)
                {
                    TapRText.GetComponent<Text>().text = "Tap One More time !!";
                    TutorialPanelRight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    TutorialPanelLeft.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
                if (j == 2)
                {
                    Right = false;
                    //Left = true;
                    TapRText.SetActive(false);
                    //TapLText.SetActive(true);
                    TutorialPanelLeft.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    TutorialPanelRight.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
                    TapAnyWhereTxt.SetActive(true);
                    //j = 0;
                }
            }
            else if(!Left && !Right)
            {
                TapAnyWhereTxt.SetActive(false);
                FinishedTxt.SetActive(true);
                rb2D.velocity = Vector2.zero;
                rb2D.isKinematic = true;
            }
        }
    }

    //private void SpinBall()
    //{
    //    if (Mathf.Abs(rb2D.velocity.x) > 0.5f)
    //    {
    //        spinSpeed += Mathf.Sqrt(Mathf.Abs(rb2D.velocity.x)) * spinSpeed * Time.deltaTime;
    //        spinSpeed = Mathf.Clamp(spinSpeed, defaultSpinSpeed, 280);
    //        if (direction)
    //        {
    //            transform.Rotate(Vector3.forward * Time.deltaTime * spinSpeed);
    //        }
    //        else
    //        {
    //            transform.Rotate(Vector3.back * Time.deltaTime * spinSpeed);
    //        }
    //    }
    //    else
    //    {
    //        spinSpeed = defaultSpinSpeed;
    //    }
    //}
}
