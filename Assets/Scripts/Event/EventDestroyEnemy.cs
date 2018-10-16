using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDestroyEnemy : MonoBehaviour
{
    public static Action<EnemyBase> EnemyDestroy;
    public static Action<EnemyBase> KillEnemy;

    public static void CallDie(EnemyBase enemyDie)
    {
        if (EnemyDestroy != null)
            EnemyDestroy(enemyDie);

        if (KillEnemy != null)
            KillEnemy(enemyDie);
    }
}
