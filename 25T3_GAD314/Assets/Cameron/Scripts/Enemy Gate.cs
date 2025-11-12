using UnityEngine;
using System.Collections.Generic;

public class EnemyGate : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }

            if (enemyList.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
