using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShow : MonoBehaviour {

    void OnPress(bool isPress)
    {
        if (!isPress)
        {
            StartMenuController._instance.OnCharacterClick(transform.parent.gameObject);
        }
    }
}
