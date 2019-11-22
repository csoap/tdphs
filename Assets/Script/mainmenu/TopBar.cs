using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar : MonoBehaviour {

    private UILabel coinLabel;
    private UILabel diamondLabel;
    private UIButton coinPlusButton;
    private UIButton diamondPlusButton;

    void Awake()
    {
        coinLabel = transform.Find("CoinBG/Label").GetComponent<UILabel>();
        diamondLabel = transform.Find("DiamondBG/Label").GetComponent<UILabel>();
        coinPlusButton = transform.Find("CoinBG/PlusButton").GetComponent<UIButton>();
        diamondPlusButton = transform.Find("DiamondBG/PlusButton").GetComponent<UIButton>();
        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.Coin || type == InfoType.Diamond)
        {
            UpdateShow();
        }
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;

        coinLabel.text = info.Coin.ToString();
        diamondLabel.text = info.Diamond.ToString();
    }
}
