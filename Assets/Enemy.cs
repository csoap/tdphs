using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //受到攻击调用方法
    //0收到多少伤害
    //1浮空距离
    //2后退距离
    void TakeDamage(string args)
    {
        string[] proArray = args.Split(',');
    }
}
