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
        stair1 = GameObject.Find("polysurface25");
    }

    // Update is called once per frame
    void update()
    {
        float distance = Vector3.Distance(stair1.transform.position, transform.position);
        climbFirstStairs(distance);
        float pozition= transform.position.y;
        Debug.Log(pozition);
        if (transform.position.y >= 10.00f)
        {
            float poz = 10.00f;
            transform.Translate(0, poz, 0);
            transform.position = new Vector3(-20.4f, 10.17f, -23.08f);
        }
    }

    public void climbFirstStairs(float distance)
    {
        if (distance > 0.05)
        {
            animatorObject.Play("idle");
        }
        else if (0.02 > distance)
        {
            animatorObject.Play("Upper");

        }
        //Debug.Log(distance);
    }
}
