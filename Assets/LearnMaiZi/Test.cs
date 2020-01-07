using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Test : MonoBehaviour {
    
	void Start ()
    {
#if UNITY_EDITOR
        Material material = AssetDatabase.LoadAssetAtPath("Assets/LearnMaiZi/m1.mat", typeof(Material)) as Material;
        material.color = Color.red;
        EditorUtility.SetDirty(material);
        //AssetDatabase.SaveAssets();
        //Debug.Log(material.name);
        //Material m2 = Object.Instantiate<Material>(material);
        //AssetDatabase.CreateAsset(m2, "Assets/LearnMaiZi/m2.mat");
        //AssetDatabase.Refresh();
        //Resources.UnloadAsset(material);
#endif
        int num = Random.Range(1, 5);
        Debug.Log(num);
        int num2 = Random.Range(6, 11);
        Debug.Log(num2);
        Debug.Log(11);
    }
	
	void Update () {
		
	}
    

}
