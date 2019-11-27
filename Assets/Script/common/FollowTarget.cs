using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

    //public Vector3 offset;
    //private Transform playerBip;
    //public float smoothing = 1;

    //// Use this for initialization
    //void Start()
    //{
    //    playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    //transform.position = playerBip.position + offset;
    //    Vector3 targetPos = playerBip.position + offset;
    //    transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    //}
    Vector3 Dir;
    public GameObject m_Player;
    void Start()
    {
        //获取到摄像机于要跟随物体之间的距离
        Dir = m_Player.transform.position - transform.position;
    }
    void LateUpdate()
    {
        //摄像机的位置
        transform.position = m_Player.transform.position - Dir;
    }
}