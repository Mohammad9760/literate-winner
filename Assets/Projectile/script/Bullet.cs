using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 25;
    public float lifeTime = 3;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // var enemy = collision.collider.GetComponent<EnemyDeath>();
        // if (enemy)
        // {
        //     enemy.TackeHit(1);
        // }
        print(collision.gameObject.name);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(Instantiate(destroyEffect, transform.position, Quaternion.identity), lifeTime);
        Destroy(gameObject);
    }
}
