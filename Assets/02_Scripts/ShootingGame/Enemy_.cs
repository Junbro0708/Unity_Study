using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private GameObject explosionFactory;

    private Vector3 dir;

    private void Start()
    {
        int randValue = UnityEngine.Random.Range(0, 10);

        if(randValue < 3)
        {
            if (GameObject.Find("Player"))
            {
                GameObject target = GameObject.Find("Player");
                dir = target.transform.position - transform.position;
                dir.Normalize();
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ScoreManager.Instance.Score++;

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = collision.transform.position;

        if (collision.gameObject.name.Contains("Bullet"))
        {
            collision.gameObject.SetActive(false);

            PlayerFire player = GameObject.Find("Player").GetComponent<PlayerFire>();
            player.bulletObjectPool.Add(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
        gameObject.SetActive(false);

        EnemyManager_ manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager_>();
        manager.enemyObjectPool.Add(gameObject);
    }
}
