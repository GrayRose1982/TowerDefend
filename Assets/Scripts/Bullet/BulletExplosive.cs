using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletExplosive : BaseBullet
{
    [SerializeField] private float _radiusExplosive;
    [SerializeField] private float _timeBlash;
    [SerializeField] private SpriteRenderer _rendder;

    [SerializeField] private Color _colorGizmos;
    private List<EnemyBase> _enemiesTakedDamage;

    private bool _isBlashing = false;

    void OnEnable()
    {
        _isBlashing = false;
        _enemiesTakedDamage = new List<EnemyBase>();
    }

    protected override void Update()
    {
        if (!_isBlashing)
            base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D hit)
    {
        //base.OnTriggerEnter2D(hit);
        var enemyBase = hit.GetComponent<EnemyBase>();

        if (!enemyBase)
            return;

        WhenFinishLife();

        if (!_enemiesTakedDamage.Contains(enemyBase))
        {
            _enemiesTakedDamage.Add(enemyBase);
            enemyBase.TakeDamage(Damage);
        }
    }

    protected override void FinishLife()
    {
        if (_isBlashing)
            return;

        _isBlashing = true;

        var colorEnd = Color.white;
        colorEnd.a = 0;
        _rendder.DOColor(colorEnd, _timeBlash);

        transform.DOScale(_radiusExplosive * 2 * Vector2.one, _timeBlash).OnComplete(base.FinishLife);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = _colorGizmos;

        Gizmos.DrawWireSphere(transform.position, _radiusExplosive);
    }
#endif
}
