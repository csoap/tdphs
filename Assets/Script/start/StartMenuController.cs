using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public static StartMenuController  _instance;

    public TweenScale startTweenScale;
    public TweenScale loginTweenScale;
    public TweenScale registerTweenScale;
    public TweenScale serverTweenScale;
    public TweenPosition startTweenPosition;
    public TweenPosition characterselectTweenPosition;
    public TweenPosition charactershowselectTweenPosition;

    public UIInput usernameInput;
    public UIInput passwordInput;

    public UILabel usernameLabelStart;
    public UILabel servernameLabelStart;

    public static string username;
    public static string password;
    public static ServerProperty sp;

    public UIInput usernameRegisterInput;
    public UIInput passwordRegisterInput;
    public UIInput repasswordRegisterInput;

    public UIGrid serverListGrid;
    public GameObject serverItemRed;
    public GameObject serverItemGreen;
    private bool haveInitServerList = false;

    public GameObject serverSelectedGo;

    public GameObject[] characterArray;
    public GameObject[] characterSelectArray;

    private GameObject characterSelect;//当前选中的角色
    public UIInput characternameInput;
    public Transform characterSelectedParent;

    public UILabel nameLabelCharacterselect;
    public UILabel lvLabelCharacterselect;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitServerList();
    }

    public void OnUsernameClick()
    {
        startTweenScale.PlayForward();
        StartCoroutine(HidePanel(startTweenScale.gameObject));
        loginTweenScale.gameObject.SetActive(true);
        loginTweenScale.PlayForward();
    }

    public void OnServerClick()
    {
        startTweenScale.PlayForward();
        StartCoroutine(HidePanel(startTweenScale.gameObject));
        serverTweenScale.gameObject.SetActive(true);
        serverTweenScale.PlayForward();

        InitServerList();//初始化服务器列表
    }

    public void InitServerList()
    {
        if (haveInitServerList) return;

        //连接服务器，取得游戏服务器列表信息
        //添加服务器列表
        for (int i = 0; i < 20; i++)
        {
            string ip = "127.0.0.1:9080";
            string areaName = (i + 1) + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject go = null;
            go = NGUITools.AddChild(serverListGrid.gameObject, count > 50 ? serverItemRed : serverItemGreen);

            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = areaName;
            sp.count = count;
        }

        serverListGrid.Reposition();
        haveInitServerList = true;

    }

    public void OnEnterGameClick(int param)
    {
        //连接服务器，验证用户名和服务器

        //进入角色选择界面
        startTweenPosition.PlayForward();
        StartCoroutine(HidePanel(startTweenScale.gameObject));
        characterselectTweenPosition.gameObject.SetActive(true);
        characterselectTweenPosition.PlayForward();
        
    }

    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    public void OnLoginClick()
    {
        //检验用户数据
        username = usernameInput.value;
        password = passwordInput.value;
        //返回用户界面
        loginTweenScale.PlayReverse();  //倒放动画
        StartCoroutine(HidePanel(loginTweenScale.gameObject));
        startTweenScale.gameObject.SetActive(true);
        startTweenScale.PlayReverse();
        usernameLabelStart.text = username; //start界面用户名更新
    }

    public void OnRegisterClick()
    {
        loginTweenScale.PlayReverse();
        StartCoroutine(HidePanel(loginTweenScale.gameObject));
        registerTweenScale.gameObject.SetActive(true);
        registerTweenScale.PlayForward();
    }

    public void OnLoginCloseClick()
    {
        //返回用户界面
        loginTweenScale.PlayReverse();  //倒放动画
        StartCoroutine(HidePanel(loginTweenScale.gameObject));
        startTweenScale.gameObject.SetActive(true);
        startTweenScale.PlayReverse();
    }

    public void OnCancelClick()
    {
        registerTweenScale.PlayReverse();  //倒放动画
        StartCoroutine(HidePanel(registerTweenScale.gameObject));
        loginTweenScale.gameObject.SetActive(true);
        loginTweenScale.PlayForward();
    }

    public void OnRegisterCloseClick()
    {
        OnCancelClick();
    }

    public void OnRegisterAndLoginClick()
    {
        username = usernameRegisterInput.value;
        password = passwordRegisterInput.value;
        registerTweenScale.PlayReverse();
        StartCoroutine(HidePanel(registerTweenScale.gameObject));
        startTweenScale.gameObject.SetActive(true);
        startTweenScale.PlayReverse();
    }

    public void OnServerSelect(GameObject serverGo)
    {
        sp = serverGo.GetComponent<ServerProperty>();
        serverSelectedGo.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().text = sp.name;
        servernameLabelStart.text = sp.name;
    }
    /// <summary>
    /// 回退到登录界面
    /// </summary>
    public void OnServerPanelClose()
    {
        serverTweenScale.PlayForward();
        StartCoroutine(HidePanel(serverTweenScale.gameObject));
        startTweenScale.gameObject.SetActive(true);
        startTweenScale.PlayReverse();
    }

    public void OnCharacterClick(GameObject go)
    {

        if (characterSelect != null)
        {
            if (characterSelect == go) return;
            iTween.ScaleTo(characterSelect, new Vector3(1,1,1), 0.5f);
        }
        iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        characterSelect = go;
    }

    public void OnChooseCharacterClick()
    {
        characterselectTweenPosition.PlayReverse();
        StartCoroutine(HidePanel(characterselectTweenPosition.gameObject));
        charactershowselectTweenPosition.gameObject.SetActive(true);
        charactershowselectTweenPosition.PlayForward();
    }

    public void OnChooseCharacterBackClick()
    {
        charactershowselectTweenPosition.PlayReverse();
        StartCoroutine(HidePanel(charactershowselectTweenPosition.gameObject));
        characterselectTweenPosition.gameObject.SetActive(true);
        characterselectTweenPosition.PlayForward();
    }

    public void OnChooseCharacterSureClick()
    {
        //TODO 判断姓名是否输入正确 
        //TODO 判断是否选择角色

        int index = -1;
        for (int i = 0; i < characterArray.Length; i++)
        {
            if (characterSelect == characterArray[i])
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return;
        }
        //characterSelectedParent.DestroyChildren();
        Destroy(characterSelectedParent.GetComponentInChildren<Animation>().gameObject);

        //更新角色的名字和等级

        nameLabelCharacterselect.text = characternameInput.value;
        lvLabelCharacterselect.text = "LV.1";

        GameObject go = GameObject.Instantiate(characterSelectArray[index], Vector3.zero, Quaternion.identity);
        go.transform.SetParent(characterSelectedParent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        charactershowselectTweenPosition.PlayReverse();
        StartCoroutine(HidePanel(charactershowselectTweenPosition.gameObject));
        characterselectTweenPosition.gameObject.SetActive(true);
        characterselectTweenPosition.PlayForward();
    }
}
