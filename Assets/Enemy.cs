using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject damageEffectPrefab;
    private Transform bloodPoint;

    public int hp = 200;

    void Start()
    {
        bloodPoint = transform.Find("BloodPoint");
    }

    //受到攻击调用方法
    //0收到多少伤害
    //1浮空距离
    //2后退距离
    void TakeDamage(string args)
    {
        if (hp <= 0) return;

        string[] proArray = args.Split(',');
        //减去伤害值
        int damage = int.Parse(proArray[0]);
        hp -= damage;
        //受到攻击德动画
        GetComponent<Animation>().Play("takedamage");
        //浮空 后退
        float backDistance = float.Parse(proArray[1]);
        float jumpHeight = float.Parse(proArray[2]);
        Vector3 dic = transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward); //人物的反方向
        iTween.MoveBy(gameObject, dic * backDistance + Vector3.up * jumpHeight, 0.3f);
        //出血
        GameObject.Instantiate(damageEffectPrefab, bloodPoint.position, Quaternion.identity);
        if (hp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {

    }
}
