using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public Action WhenFinishLife;
    public Action<float> UpdatePerFrame;

    protected Transform Trans;

    [SerializeField] protected float Damage = 10f;
    [SerializeField] protected float Speed = 5f;
    [SerializeField] protected float TimeRevive = 2f;

    private float _timer;
    private bool _finishLife = false;

    #region Function system
    void Awake()
    {
        Trans = transform;
    }

    void OnEnable()
    {
        _timer = 0;
        _finishLife = false;
    }

    protected virtual void Start()
    {
        WhenFinishLife += FinishLife;
        UpdatePerFrame += TimeCount;
        UpdatePerFrame += MoveToward;
    }

    protected virtual void OnDestroy()
    {
        WhenFinishLife -= FinishLife;
        UpdatePerFrame -= TimeCount;
        UpdatePerFrame -= MoveToward;
    }

    protected virtual void Update()
    {
        var deltaTime = Time.deltaTime;

        if (UpdatePerFrame != null)
            UpdatePerFrame(deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D hit)
    {
        hit.GetComponent<EnemyBase>().TakeDamage(Damage);
        Destroy(gameObject);
    }
    #endregion


    #region Update per frame
    protected void TimeCount(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= TimeRevive)
        {
            if (!_finishLife)
            {
                _finishLife = true;

                if (WhenFinishLife != null)
                    WhenFinishLife();
            }
        }
    }

    protected void MoveToward(float deltaTime)
    {
        Trans.position = Vector3.MoveTowards(Trans.position, Trans.position + Trans.right * Speed, deltaTime * Speed);
    }


    protected virtual void FinishLife()
    {
        Destroy(gameObject);
    }

    #endregion

    public virtual void SetDataBullet(float damage, Transform target)
    {
        Damage = damage;
    }
}
