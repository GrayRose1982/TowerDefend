using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower: MonoBehaviour {
    [Header("Base information")]
    [SerializeField]
    private TowerIngame _data;

    [Header("Current information")]
    public float Price;

    public float Damage = 10;
    public float FireRate = .2f;
    public float Radius = 2.4f;
    public float SpeedRotate = 90;

    [SerializeField] protected float AngleDiffCanFire = 15f;

    [Header("Tower object")]
    [SerializeField] protected CircleCollider2D RadiusCheck;
    [SerializeField] protected Transform Head;

    [SerializeField] protected BaseBullet Projectile;

    [SerializeField] protected bool CanFire;

    protected List<EnemyBase> EnemiesInSide;
    protected EnemyBase Target;

    private float _timer;

    public TowerIngame Data
    {
        get { return _data; }
        set { _data = value; }
    }

     
    void Reset() {
        RadiusCheck = GetComponent<CircleCollider2D>();
        Head = transform.Find("Head");
    }

    void Awake() {
        EventDestroyEnemy.KillEnemy += EnemyKill;
        EventDestroyEnemy.EnemyDestroy += EnemyKill;
    }

    void OnEnable() {
        RadiusCheck.radius = Radius;

        if (EnemiesInSide == null)
            EnemiesInSide = new List<EnemyBase>();
        else
            EnemiesInSide.Clear();
    }

    void OnDestroy() {
        EventDestroyEnemy.KillEnemy -= EnemyKill;
        EventDestroyEnemy.EnemyDestroy -= EnemyKill;
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        RadiusCheck.radius = Radius;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
#endif

    public void SetupDataTower(TowerData tower) {
        Data = new TowerIngame(tower);
    }

    public void EnemyKill(EnemyBase enemyKilled) {
        if (enemyKilled == Target)
            Target = null;

        if (EnemiesInSide.Contains(enemyKilled))
            EnemiesInSide.Remove(enemyKilled);
    }

    protected void Update() {
        if (!Target)
            FindTarget();

        if (!Target)
            return;

        RotateToTarget();
        Fire();
    }

    protected virtual void Fire() {
        if (!CheckCanFire())
            return;

        var bullet = Instantiate(Projectile, Head.position, Quaternion.identity);
        bullet.transform.SetDirection(Target.transform.position - Head.position);
        bullet.SetDataBullet(Damage, Target.transform);
    }

    protected bool CheckCanFire() {
        if (!CanFire)
            return false;

        if (_timer + FireRate >= Time.time)
            return false;

        _timer = Time.time;

        return true;
    }

    protected virtual void RotateToTarget() {
        Vector2 dir = Target.transform.position - Head.position;
        float angleTo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angleFrom = Head.rotation.eulerAngles.z;

        CanFire = AngleDiffCanFire > Mathf.Abs(Mathf.DeltaAngle(angleTo, angleFrom));

        float angleTranslate = Mathf.MoveTowardsAngle(angleFrom, angleTo, Time.deltaTime * SpeedRotate);

        Head.eulerAngles = Vector3.forward * angleTranslate;
    }

    void FindTarget() {
        if (EnemiesInSide.Count > 0)
            Target = EnemiesInSide[0];
    }

    void OnTriggerEnter2D(Collider2D hit) {
        EnemiesInSide.Add(hit.GetComponent<EnemyBase>());
    }

    void OnTriggerExit2D(Collider2D hit) {
        var enemyBase = hit.GetComponent<EnemyBase>();
        EnemiesInSide.Remove(enemyBase);
        if (Target == enemyBase)
            Target = null;
    }
}
