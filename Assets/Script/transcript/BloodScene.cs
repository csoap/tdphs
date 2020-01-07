using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScene : MonoBehaviour {

	// Use this for initialization
    private static BloodScene _instance;
    public static BloodScene Instance
    {
        get { return _instance; }
    }

    private static UISprite uiSprite;
    private static TweenAlpha tweenAlpha;

    void Awake()
    {
        _instance = this;
        uiSprite = GetComponent<UISprite>();
        tweenAlpha = GetComponent<TweenAlpha>();
    }

    public void Show()
    {
        uiSprite.alpha = 1;
        tweenAlpha.ResetToBeginning();
        tweenAlpha.PlayForward();
    }
}
