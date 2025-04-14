using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float timeUntilFire;
    void Update()
    {
        if (target == null || !CheckTargetIsInRange())
        {
            FindTarget();
        }

        if (target == null || !CheckTargetIsInRange())
        {
            return;
        }

        RotateTowardsTarget();

        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bps)
        {
            Shoot();
            timeUntilFire = 0f;
        }
    }

    void Shoot()
    {
        GameObject bulletObj = BulletPoolManager.Instance.GetBullet();
        bulletObj.transform.position = firingPoint.position;
        bulletObj.transform.rotation = Quaternion.identity;

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        bulletScript.OnDeactivate -= BulletPoolManager.Instance.ReturnBullet;
        bulletScript.OnDeactivate += BulletPoolManager.Instance.ReturnBullet;
    }


    bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        Transform bestTarget = null;
        int highestPathIndex = -1;

        foreach (var hit in hits)
        {
            EnemyMovement enemy = hit.transform.GetComponent<EnemyMovement>();
            if (enemy != null && enemy.GetPathIndex() > highestPathIndex)
            {
                highestPathIndex = enemy.GetPathIndex();
                bestTarget = hit.transform;
            }
        }

        target = bestTarget;
    }

    void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
