using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float damage;

    [SerializeField] float bulletLiveTime = 0.5f;

    private void Start()
    {
        StartCoroutine(HideBulletAfterShot());
    }

    public float Damage
    {
        get { return damage; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HideBullet();
        }
    }

    IEnumerator HideBulletAfterShot()
    {
        yield return new WaitForSeconds(bulletLiveTime);
        HideBullet();
    }

    private void HideBullet()
    {
        this.gameObject.SetActive(false);
    }


}
