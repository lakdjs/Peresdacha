using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
