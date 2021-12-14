using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSimpleClimb : MonoBehaviour
{
    private bool flag = false;
    public float speed = 2.0f;
    private Animator animatorObject;
    private GameObject stair1;
    // Start is called before the first frame update
    void Start()
    {
        animatorObject = GetComponent<Animator>();
        stair1 = GameObject.Find("stair1");
    }

    // Update is called once per frame
    void Update()
    { 
        float distance = Vector3.Distance(stair1.transform.position, transform.position);
        if (flag == false)
        {
            climbFirstStairs(distance);
            flag = true;
        }
        else
        {//try to walk to second stair
            float positionx = transform.position.y;
            if (positionx >= 4.60f && positionx <= 6.44f)
            {

                transform.Rotate(0, -187.633f, 0);
                float translation = transform.position.z* speed;
                translation *= Time.deltaTime;
                transform.Translate(0, 0, translation);
                animatorObject.SetBool("Walk", true);
            }
        }
       
    }

    public void climbFirstStairs(float distance)
    {
        if (distance > 0.2)
        {
            animatorObject.Play("idle");
        }
        else if (0.02 > distance)
        {
            animatorObject.Play("Upper");

        }
        Debug.Log(distance);
    }
    /*    public void walkToSecondStair(float positionx)
    {
        if (positionx >= -25.42815f && positionx <= -25.00f) { 

            transform.Rotate(0, -187.633f, 0);
            float translation = transform.position.x * speed;
            translation *= Time.deltaTime;
            transform.Translate(0, 0, translation); }
    }*/
}
