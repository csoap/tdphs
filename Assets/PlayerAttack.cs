using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();

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

}
