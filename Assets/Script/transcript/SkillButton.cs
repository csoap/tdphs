using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{

    public PosType posType = PosType.Basic; //技能类型
    public float coldTime = 4; //冷却时间
    private float coldTimer = 0; //剩余冷却时间时间
    private UISprite maskSprite;
    private PlayerAnimation playerAnimation;
    private UIButton btn;

    void Start()
    {
        playerAnimation = TranscriptManager._instance.player.GetComponent<PlayerAnimation>();
        if (transform.Find("Mask"))
        {
            maskSprite = transform.Find("Mask").GetComponent<UISprite>();
        }

        btn = GetComponent<UIButton>();
    }

    void Update()
    {
        if (maskSprite == null) return;
        if (coldTimer  > 0 )
        {
            coldTimer -= Time.deltaTime;
            maskSprite.fillAmount = coldTimer / coldTime;
        }
        else
        {
            maskSprite.fillAmount = 0;
            Enable();
        }
    }
    void OnPress(bool isPress)
    {
        playerAnimation.OnAttackButtonClick(isPress, posType);

        if (isPress && maskSprite != null)
        {
            coldTimer = coldTime;
            Disable();
        }
    }

    void Disable()
    {
        GetComponent<Collider>().enabled = false;
        btn.SetState(UIButtonColor.State.Normal, true);
    }

    void Enable()
    {
        GetComponent<Collider>().enabled = true;
    }
}
