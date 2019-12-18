using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{

    private Renderer[] rendererArray;
    private NcCurveAnimation[] curveAnimArray;
    private GameObject EffectOffest;

    void Start()
    {
        rendererArray = this.GetComponentsInChildren<Renderer>();
        curveAnimArray = this.GetComponentsInChildren<NcCurveAnimation>();
        if (transform.Find("EffectOffset"))
        {
            EffectOffest = transform.Find("EffectOffset").gameObject;
        }
    }

    /// <summary>
    /// 渲染特效
    /// </summary>
    public void Show()
    {
        if (EffectOffest != null)
        {
            EffectOffest.SetActive(false);
            EffectOffest.SetActive(true); //先隐藏再现实，粒子都会重新播放
        }
        else
        {
            foreach (Renderer renderer in rendererArray)
            {
                renderer.enabled = true;
            }

            foreach (NcCurveAnimation anim in curveAnimArray)
            {
                anim.ResetAnimation();
            }
        }

    }
    
}
