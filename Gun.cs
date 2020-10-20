using Audio;
using CameraEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Gun : MonoBehaviour
    {
        public float amount = 1;
        public float fireRate = 0.3f;
        public float damage;
        public float bulletSpeed;
        public float spreadAmount;
        public float recoil;

        public bool ready = true;

        public GameObject bullet;
        public GameObject enemyBullet;

        public Transform gunTip;

        public void Shoot()
        {
            for (int i = 0; i < amount; i++)
                SpawnBullet();

            ready = false;

            Invoke("GetReady", fireRate);
        }

        private void GetReady()
        {
            ready = true;
        }

        private void SpawnBullet()
        {
            GameObject newBullet;

            Vector3 spread = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount));

            if (transform.parent.name == "WeaponHolder")
            {
                newBullet = Instantiate(bullet, gunTip.position, transform.rotation);
                AudioManager.Play("PlayerShoot");
                CameraShaker.ShakeOnce(0.15f, 0.15f);
            }
            else
            {
                newBullet = Instantiate(enemyBullet, gunTip.position, transform.rotation);
                AudioManager.Play("EnemyShoot");
            }

            newBullet.transform.localScale *= (1 + (damage / 35));

            newBullet.GetComponent<Rigidbody2D>().velocity = (transform.up * bulletSpeed) + spread;

            newBullet.GetComponent<Bullet>().SetDamage(damage);
        }
    }
}
