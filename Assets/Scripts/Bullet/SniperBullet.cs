using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BaseBullet
{
    [SerializeField] protected Transform Target;

    protected override void Start()
    {
        WhenFinishLife += FinishLife;
        UpdatePerFrame += TimeCount;
        UpdatePerFrame += MoveToTarget;
    }

    protected override void OnDestroy()
    {
        WhenFinishLife -= FinishLife;
        UpdatePerFrame -= TimeCount;
        UpdatePerFrame -= MoveToTarget;
    }

    protected override void OnTriggerEnter2D(Collider2D hit)
    {
    }   

    public override void SetDataBullet(float damage, Transform target)
    {
        base.SetDataBullet(damage, target);
        Target = target;
    }

    public void MoveToTarget(float deltaTime)
    {
        if (Target)
        {
            Trans.position = Vector3.MoveTowards(Trans.position, Target.position, deltaTime * Speed);

            if (Vector2.Distance(Trans.position, Target.position) <= .5f)
            {
                WhenFinishLife();
                Target.GetComponent<EnemyBase>().TakeDamage(Damage);
            }
        }
    }
}
