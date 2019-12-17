using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();

    public float distanceAttackForwad = 2; //向前攻击距离
    public float distanceAttackAround = 2; // 周围攻击距离
    
    //攻击范围
    public enum AttackRange
    {
        Forward,
        Around
    }



    void Start()
    {
        PlayerEffect[] peArray = GetComponentsInChildren<PlayerEffect>();
        foreach (PlayerEffect pe in peArray)
        {
            effectDict.Add(pe.gameObject.name, pe);
        }
    }
    /// <summary>
    /// args: normal,attack1,attack1,1,0     攻击  攻击特效1 ，音效,前进一步 ，上升0
    /// </summary>
    /// <param name="args"></param>
    void Attack(string args)
    {
        Debug.Log(args);
        string[] proArray = args.Split(',');
        //动画
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);

        //playsound
        string soundName = proArray[2];
        SoundManager._instance.Play(soundName);
        
        //前进步数
        float moveForward = float.Parse(proArray[3]);
        if (moveForward > 0.1f)
        {
            iTween.MoveBy(gameObject,Vector3.forward * moveForward, 0.3f);
        }

        string posType = proArray[0];
        if (posType == "normal")
        {
            ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Forward);
            foreach (GameObject go in arrayList)
            {
                go.SendMessage("TakeDamage"); //TODO 参数一会再写 enemy
            }
        }
        else
        {

        }

        //上升 敌人击飞
        float moveUp = float.Parse(proArray[4]);
        if (moveUp > 0.1f)
        {
            //iTween.MoveBy(gameObject, Vector3.up * moveUp, 0.3f);
        }
    }

    void ShowPlayerEffect(string effectName)
    {
        PlayerEffect pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {
            pe.Show();
        }
    }



    //得到攻击范围的敌人
    private ArrayList GetEnemyInAttackRange( AttackRange attackRange)
    {
        ArrayList arrayList = new ArrayList();
        if (attackRange == AttackRange.Forward)
        {
            foreach (GameObject go in TranscriptManager._instance.enemyList)
            {
                Vector3 pos = transform.InverseTransformPoint(go.transform.position); //将敌人世界坐标转换为主角内的局部坐标
                if (pos.z > -0.5f )
                {
                    float distance = Vector3.Distance(Vector3.zero, pos);
                    if (distance < distanceAttackForwad)
                    {
                        arrayList.Add(go);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject go in TranscriptManager._instance.enemyList)
            {
                Vector3 pos = transform.InverseTransformPoint(go.transform.position); //将敌人世界坐标转换为主角内的局部坐标
                float distance = Vector3.Distance(Vector3.zero, pos);
                if (distance < distanceAttackAround)
                {
                    arrayList.Add(go);
                }
            }
        }

        return arrayList;
    }

}
