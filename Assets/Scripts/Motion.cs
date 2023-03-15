using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Vector3 pos;
    public float height, speed;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.score >= 5f && GameManager.instance.score < 10f)
        {
            StartCoroutine(dunkSwitch(0.6f));
        }
        else if (GameManager.instance.score >= 15f && GameManager.instance.score < 20f)
        {
            StartCoroutine(dunkSwitch(0.6f));
            //VerticalMotion();
        }
    }

    public void VerticalMotion()
    {
        if (GameManager.instance.BallMovement)
        {
            float newY = Mathf.Sin(Time.time * speed) * height + pos.y / 2f;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }        
    }

    //public void HorizontalMotion()
    //{
    //    float newX = Mathf.Sin(Time.time * speed) * height;
    //    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("dunk");
        GameManager.instance.Score();
        GameManager.instance.PerfectScored();
    }

    public IEnumerator dunkSwitch(float timer)
    {
        yield return new WaitForSeconds(timer);
        VerticalMotion();
    }

    public void switchRing()
    {
        GameManager.instance.SwitchOnScoring();
    }
}