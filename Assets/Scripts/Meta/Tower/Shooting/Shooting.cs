using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooting : MonoBehaviour
{
    internal virtual void Shoot(Bullet bulletPrefab, Transform targetTransform, float jumpPower, float time)
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.DOJump(targetTransform.position, jumpPower, 1, time);
    }
}
