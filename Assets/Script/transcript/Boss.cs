using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float viewAngle = 50; //可视范围度数
    public float rotateSpeed = 1; // 旋转速度
    private Transform player;
    public float moveSpeed = 2; //移动速度
    public float attackDistance = 3; //攻击距离
    public float timeInterval = 1; //攻击间距时间
    private float timer = 0;
    private bool isAttacking = false;
    private int attackIndex = 0; //攻击动作索引

	// Use this for initialization
	void Start ()
    {
        player = TranscriptManager._instance.player.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y; //计算时候主角y轴与boss一致
        float angle = Vector3.Angle(playerPos - transform.position, transform.forward);
        if (angle < viewAngle /2)
        {
            //在攻击视野之内
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < attackDistance)
            {
                //进行攻击
                if (isAttacking == false)
                {
                    GetComponent<Animation>().CrossFade("idle");
                    timer += Time.deltaTime;
                    if (timer >= timeInterval)
                    {
                        timer = 0;
                        Attack();
                    }
                }
            }
            else
            {
                //进行追击
                GetComponent<Animation>().CrossFade("walk");
                GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
            }

        }
        else
        {
            //在攻击视野之外，进行转向
            GetComponent<Animation>().CrossFade("walk");
            Quaternion targetRotation = Quaternion.LookRotation(playerPos - transform.position);
            transform.rotation =  Quaternion.Lerp(transform.rotation, targetRotation,1 * Time.deltaTime);
        }
    }

    void Attack()
    {
        isAttacking = true;
        attackIndex++;
        if (attackIndex == 4) attackIndex = 1;
        GetComponent<Animation>().CrossFade("attack0" + attackIndex);
    }
    //结束攻击
    void BackToStand()
    {
        isAttacking = false;
    }
}
