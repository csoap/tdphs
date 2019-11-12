using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerProperty : MonoBehaviour
{
    public string ip;
    private string _name;
    public string name
    {
        set
        {
            transform.Find("Label").GetComponent<UILabel>().text = value;
            _name = value;
        }
        get { return _name; }
    }
    public int count;

    public void OnPress(bool isPress)
    {
        if (isPress == false)
        {
            //选择了当前的服务器
            transform.root.SendMessage("OnServerSelect", this.gameObject);
        }
    }
}
