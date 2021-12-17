using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalkController : MonoBehaviour
{

    private Animator anim;
    public float speed = 2.0f;
    public float rotationSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.Play("Scene");
    }

    // Update is called once per frame
    void Update()
    {
        float translation = transform.position.y*speed;
        float rotation = transform.rotation.y *rotationSpeed;
        translation *= Time.deltaTime;
        rotationSpeed *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotationSpeed, 0);
        rotateToMoveBack(transform.position.x);
       
        if (translation != 0)
        {
            anim.SetBool("Walk", true);
        }

    }
    public void rotateToMoveBack(float xposition)
    {
        if (xposition>= 13.26f && xposition <= 20.00f)
        {
            transform.Rotate(0, -92.411f, 0);
        }
        else
        {
               if (xposition >= -16.00f && xposition <= -15.55f)
                {
                    transform.Rotate(0, 92.498f, 0);
                    }

        }

    }
}
