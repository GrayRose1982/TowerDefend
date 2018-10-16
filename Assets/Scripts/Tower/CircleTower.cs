using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTower : BaseTower
{
    [SerializeField] private int _numberBullet = 6;
    [SerializeField] private float _diffAngle = 30f;

    protected override void Fire()
    {
        if (!CheckCanFire())
            return;

        var angleAdd = _diffAngle;
        var angleChange = _diffAngle * 2 / _numberBullet;

        for (int i = 0; i < _numberBullet; i++)
        {
            var bullet = Instantiate(Projectile, Head.position, Quaternion.identity);
            bullet.transform.SetDirection(Target.transform.position - Head.position, angleAdd);
            bullet.SetDataBullet(Damage, Target.transform);

            angleAdd -= angleChange;
        }
    }
}
