using PT.Utils;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{ 
    public class Gun : MonoBehaviour
    {
        public void ShootTarget(Transform target)
        {
            if (!canShoot)
                return;

            GameObject go = Instantiate(bulletPrefabT);
            go.transform.position = gunPointT.position;
            Bullet b = go.GetComponent<Bullet>();
            Vector3 dir = target.position - go.transform.position;
            b.GetShot(dir);
            canShoot = false;
            TimeManager.Instance.DoWithDelay(coolDown, () =>
            {
                canShoot = true;
            });
        }

        [Button]
        public void Shoot()
        {
            if (!canShoot)
                return;

            GameObject go = Instantiate(bulletPrefabT);
            go.transform.position = gunPointT.position;
            go.transform.forward = transform.forward;
            Bullet b = go.GetComponent<Bullet>();
            b.GetShot(transform.forward);
            canShoot = false;
            TimeManager.Instance.DoWithDelay(coolDown, () =>
            {
                canShoot = true;
            });
        }

        [SerializeField] private Transform gunPointT;
        [SerializeField] private GameObject bulletPrefabT;
        [SerializeField] private float coolDown = 0.5f;
        [SerializeField] private bool canShoot = true;
    }
}