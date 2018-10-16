using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private MoveController _moveController;

    private float _hp = 3f;

    public void SetPath(Vector2[] path)
    {   
        _moveController.PathMove = path;
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp < 0)
        {
            EventDestroyEnemy.CallDie(this);

            Destroy(gameObject);
        }
    }
}
