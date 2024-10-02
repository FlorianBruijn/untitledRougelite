using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    EnemyData enemyData{ get; set; }
    public void GetHit(float Damage);
}
