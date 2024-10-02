using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : IEnemy
{
    public EnemyData enemyData{ get; set;}
    void IEnemy.GetHit(float Damage)
    {
        Debug.Log("damage:" + Damage);
    }
}
