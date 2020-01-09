using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject damageEffectPrefab;
    private Transform bloodPoint; //喷血的位置
    private CharacterController cc;

    public int hp = 200;
    public float speed = 2; //移动速度
    public float downSpped = 1f;//移到地下速度
    private float downDistance = 0; //移到地下距离
    public int attackRate = 2; //攻击速率 多少秒攻击一次
    private float attackTimer = 0;//攻击计时
    private float distance; //与主角的距离
    public float attackDistance = 2; //攻击距离
    public int damage = 20; //攻击力
    private GameObject HpBarGameObject; //血条
    private UISlider hpBarSlider;
    private GameObject hudTextGameObject;
    private HUDText hudText; //造成伤害文本
    private int hpTotal = 0;

    void Start()
    {
        TranscriptManager._instance.enemyList.Add(gameObject);
        hpTotal = hp;
        bloodPoint = transform.Find("BloodPoint");
        cc = GetComponent<CharacterController>();
        InvokeRepeating("CalcDistance", 0, 0.1f); //获取和主角的距离，每隔0.1s获取一次，不需要再update获取，节约性能
        Transform HpBarPoint = transform.Find("HpPoint");
        
        HpBarGameObject = HpBarManager._instance.GetHpBar(HpBarPoint.gameObject);
        hpBarSlider = HpBarGameObject.transform.Find("Bg").GetComponent<UISlider>();
        hudTextGameObject = HpBarManager._instance.GetHudText(HpBarPoint.gameObject);
        hudText = hudTextGameObject.GetComponent<HUDText>();
    }

    void Update()
    {
        if (hp <= 0)
        {
            //移到地下
            downDistance += downSpped * Time.deltaTime;
            transform.Translate(-transform.up * downSpped * Time.deltaTime);
            if (downDistance > 4)
            {
                Destroy(gameObject);
            }
            return;
        }
        Animation anim = GetComponent<Animation>();
        if (distance < attackDistance)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                //面向主角
                Transform player = TranscriptManager._instance.player.transform;
                Vector3 targetPos = player.position;
                targetPos.y = transform.position.y;
                transform.LookAt(targetPos);
                //攻击
                anim.Play("attack01");

                attackTimer = 0;
            }

            if (!anim.IsPlaying("attack01"))
            {
                anim.CrossFade("idle");
            }
        }
        else
        {
            anim.Play("walk");
            Move();
        }

    }

    void Move()
    {
        Transform player = TranscriptManager._instance.player.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward * speed);
    }

    void CalcDistance()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);
    }

    //受到攻击调用方法
    //0收到多少伤害
    //1浮空距离
    //2后退距离
    void TakeDamage(string args)
    {
        if (hp <= 0) return;
        Combo._instance.ComboPlus();
        string[] proArray = args.Split(',');
        //减去伤害值
        int damage = int.Parse(proArray[0]);
        hp -= damage;
        hpBarSlider.value = (float) hp / hpTotal;
        //受到攻击德动画
        hudText.Add("-" + args[0], Color.red,0.3f);
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

    void Attack()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void Dead()
    {
        //第一种死亡方式是播放动画
        //第二种死亡方式播放破碎
        Destroy(HpBarGameObject);
        Destroy(hudTextGameObject);
        GetComponent<CharacterController>().enabled = false; //碰撞关闭
        TranscriptManager._instance.enemyList.Remove(this.gameObject); //在敌人列表移除
        int random = Random.Range(0, 10);
        if (random < 7)
        {
            GetComponent<Animation>().Play("die");
        }
        else
        {
            GetComponentInChildren<MeshExploder>().Explode();//播放破碎
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false; //皮肤渲染隐藏
        }
    }
}
