using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandeler : MonoBehaviour
{
    public IEnemy enemy;
    void Start()
    {
        enemy = new test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
