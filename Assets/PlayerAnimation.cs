using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public void OnAttackButtonClick(bool isPress, PosType posType)
    {
        Debug.Log("ispress:" + isPress + "posType:" + posType);
    }
}
