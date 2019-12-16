using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{

    public float velocity = 5;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 NowVel = GetComponent<Rigidbody>().velocity;
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            anim.SetBool("Move", true);
            //没有技能释放 才会控制朝向
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity * h, NowVel.y, velocity * v);
                transform.LookAt(new Vector3(h, 0, v) + transform.position);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, NowVel.y, 0);
            }
        }
        else
        {
            anim.SetBool("Move",false);
            GetComponent<Rigidbody>().velocity = new Vector3(0 ,NowVel.y, 0);
        }
    }
}
