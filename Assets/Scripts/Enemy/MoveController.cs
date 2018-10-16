using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] Vector2[] _pathMove;
    private Transform _trans;
    private bool _canMove;
    [SerializeField] private float _speed;

    [SerializeField] private int _indexInPath;

    public Vector2[] PathMove
    {
        get { return _pathMove; }
        set
        {
            _pathMove = value;
            _canMove = true;
            IndexInPath = 0;
            _trans.position = PathMove[_indexInPath];
        }
    }

    public int IndexInPath
    {
        set
        {
            _indexInPath = value;
            if (_indexInPath >= _pathMove.Length - 1)
            {
                _canMove = false;
                EventDestroyEnemy.CallDie(GetComponent<EnemyBase>());

                Destroy(gameObject);
            }
        }
        get { return _indexInPath; }
    }

    void Awake()
    {
        _trans = transform;
    }

    void Update()
    {
        if (!_canMove)
            return;

        Move();
        CheckPositionIndexInPath();
    }

    private void CheckPositionIndexInPath()
    {

        if (_indexInPath + 1 >= _pathMove.Length)
            return;

        if (Vector2.Distance(_trans.position, _pathMove[_indexInPath + 1]) <= _speed * Time.deltaTime)
            IndexInPath++;
    }

    private void Move()
    {
        if (_indexInPath + 1 >= _pathMove.Length)
            return;

        var dir = _pathMove[_indexInPath + 1] - (Vector2)_trans.position;
        dir.Normalize();

        _trans.Translate(dir * _speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        _canMove = false;
    }
}
