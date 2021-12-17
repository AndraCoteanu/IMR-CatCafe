using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim1;
    // Start is called before the first frame update
    void Start()
    {
        anim1 = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Animation>()["Scene"].wrapMode = WrapMode.Loop;
       
        //anim.Looped = true;
        //anim.Play("Scene");a
        //anim["Scene"].wrapMode = WrapMode.Loop;
        // anim.Play("Scene");
    }
}
