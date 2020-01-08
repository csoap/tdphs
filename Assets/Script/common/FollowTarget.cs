using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    Vector3 Dir;
    public GameObject m_Player;
    public float smoothing = 2.3f;
    void Start()
    {
        //获取到摄像机于要跟随物体之间的距离
        Dir = m_Player.transform.position - transform.position;
    }
    void LateUpdate()
    {
        //摄像机的位置
        //transform.position = m_Player.transform.position - Dir;
        Vector3 targetPos = m_Player.transform.position - Dir;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime); //缓慢移动
    }
}