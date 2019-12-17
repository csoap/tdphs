using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptManager : MonoBehaviour {

	// Use this for initialization
    public static TranscriptManager _instance;
    public GameObject player;


    void Awake()
    {
        _instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
}
