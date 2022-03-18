using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private int poolCount = 20;
    [SerializeField] private bool autoExpand = false;
    [SerializeField] private Bullet bulletPrefab;

    private PoolMono<Bullet> pool;

    private void Start()
    {
        pool = new PoolMono<Bullet>(bulletPrefab, poolCount, this.transform);
        pool.autoExpand = this.autoExpand;
    }

    public Bullet CreateBullet(Vector3 position)
    {
        var bullet = pool.GetFreeElement();
        bullet.transform.position = position;
        return bullet;
    }
}
