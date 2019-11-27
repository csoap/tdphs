using UnityEngine;
using System.Collections;

public class PlayerVillageAnimation : MonoBehaviour
{

    private Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.05f)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }
}