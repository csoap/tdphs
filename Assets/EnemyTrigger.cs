using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPosArray;
    public float time = 0;//表示多少秒之后开始生成
    public float repeateRate = 0;
    private bool isSpawned = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && isSpawned == false)
        {
            isSpawned = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject go in enemyPrefabs)
        {
            foreach (Transform pos in spawnPosArray)
            {
                GameObject.Instantiate(go, pos.position, Quaternion.identity); //Quaternion.identity 表示无旋转
            }
        }
        yield return new WaitForSeconds(repeateRate);
    }
}
