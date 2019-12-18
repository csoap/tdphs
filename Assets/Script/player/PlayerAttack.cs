using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();
    public PlayerEffect[] effectArray; //敌人特效
    public float distanceAttackForwad = 100; //向前攻击距离
    public float distanceAttackAround = 200; // 周围攻击距离
    public int[] damageArray = new int[]{20,30,30,30};
    //攻击范围
    public enum AttackRange
    {
        Forward,
        Around
    }



    void Start()
    {
        PlayerEffect[] peArray = GetComponentsInChildren<PlayerEffect>(); //人物特效
        foreach (PlayerEffect pe in peArray)
        {
            effectDict.Add(pe.gameObject.name, pe);
        }

        foreach (PlayerEffect pe in effectArray)
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
        string[] proArray = args.Split(',');
        //动画
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);

        //playsound
        string soundName = proArray[2];
        PlaySound(soundName);
        
        //前进步数
        float moveForward = float.Parse(proArray[3]);
        if (moveForward > 0.1f)
        {
            iTween.MoveBy(gameObject,Vector3.forward * moveForward, 0.3f);
        }

        string posType = proArray[0];
        if (posType == "normal")
        {
            ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Forward); //获取enemy列表
            foreach (GameObject go in arrayList)
            {
                go.SendMessage("TakeDamage", damageArray[0] + "," + proArray[3] + "," + proArray[4]); // 参数 受到的伤害、后退距离、上升距离
            }
        }
        //else if (posType == "Skill1")
        //{
        //    ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Around); //获取enemy列表
        //    foreach (GameObject go in arrayList)
        //    {
        //        go.SendMessage("TakeDamage", damageArray[1] + "," + proArray[3] + "," + proArray[4]);
        //    }
        //}
        //else if (posType == "Skill2")
        //{
        //    ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Around); //获取enemy列表
        //    foreach (GameObject go in arrayList)
        //    {
        //        go.SendMessage("TakeDamage", damageArray[2] + "," + proArray[3] + "," + proArray[4]);
        //    }
        //}

        //上升 敌人击飞
        float moveUp = float.Parse(proArray[4]);
        if (moveUp > 0.1f)
        {
            //iTween.MoveBy(gameObject, Vector3.up * moveUp, 0.3f);
        }
    }

    void PlaySound(string soundName)
    {
        SoundManager._instance.Play(soundName);
    }

    /// <summary>
    /// 技能攻击 args: normal,attack1,attack1,1,0     攻击  攻击特效1 ，音效,前进一步 ，上升0
    /// </summary>
    /// <param name="args"></param>
    void SkillAttack(string args)
    {
        string[] proArray = args.Split(',');
        string posType = proArray[0];
        if (posType == "Skill1")
        {
            ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Around); //获取enemy列表
            foreach (GameObject go in arrayList)
            {
                go.SendMessage("TakeDamage", damageArray[1] + "," + proArray[1] + "," + proArray[2]);
            }
        }
        else if (posType == "Skill2")
        {
            ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Around); //获取enemy列表
            foreach (GameObject go in arrayList)
            {
                go.SendMessage("TakeDamage", damageArray[2] + "," + proArray[1] + "," + proArray[2]);
            }
        }
        else if (posType == "Skill3")
        {
            ArrayList arrayList = GetEnemyInAttackRange(AttackRange.Forward); //获取enemy列表
            foreach (GameObject go in arrayList)
            {
                go.SendMessage("TakeDamage", damageArray[3] + "," + proArray[1] + "," + proArray[2]);
            }
        }
    }
    /// <summary>
    /// 主角自身特效
    /// </summary>
    /// <param name="effectName"></param>
    void ShowPlayerEffect(string effectName)
    {
        PlayerEffect pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {
            pe.Show();
        }
    }
    /// <summary>
    /// 释放恶魔之手 普攻最后一下
    /// </summary>
    void ShowEffectDevilHand()
    {
        string effectName = "DevilHandMobile";
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        if (pe == null) return;
        ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
        foreach (GameObject go in array)
        {
            RaycastHit hit;
            bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground")); //怪物坐标发起射线向下获得在地板的坐标 敌人地面所在位置
            if (collider)
            {
                GameObject.Instantiate(pe, hit.point, Quaternion.identity); //在地板坐标生成恶魔之手
            }
        }
    }
    /// <summary>
    /// 技能一  飞鸟 飞翔敌人位置
    /// </summary>
    /// <param name="effectName"></param>
    void ShowEffectSelfToTarget(string effectName)
    {
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array)
        {
            GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
            //goEffect.transform.parent = transform;
            //goEffect.transform.localPosition = Vector3.zero + Vector3.up;
            goEffect.transform.position = Vector3.zero + Vector3.up;
            goEffect.GetComponent<EffectSettings>().Target = go;
        }
    }

    void ShowEffectToTarget(string effectName)
    {
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array)
        {

            RaycastHit hit;
            bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground")); //怪物坐标发起射线向下获得在地板的坐标 敌人地面所在位置
            if (collider)
            {
                GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
                //goEffect.transform.parent = transform;
                goEffect.transform.position = hit.point;
            }
            
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
                //Debug.Log(go.name + "pos.z:  " + pos.z);
                if (pos.z > -0.5f )
                {
                    float distance = Vector3.Distance(Vector3.zero, pos);
                    Debug.Log("distance :" + distance);
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
