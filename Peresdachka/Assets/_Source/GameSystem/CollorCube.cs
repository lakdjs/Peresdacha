using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.Pool;
using System;

public class CollorCube : MonoBehaviour
{
    public IObjectPool<CollorCube> pool;

    [SerializeField] private Material[] materials;

    private Rigidbody _rb;
    private MeshRenderer _mesh;

    private IDisposable subscription;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        subscription = Observable.EveryValueChanged(transform, t => t.position)
            .Subscribe(pos => {
                if (pos.y < -5f) 
                { pool.Release(this); 
                } 
            });
    }

    private void OnEnable()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-3f, 3f), 5, 0);

        _rb.velocity = Vector3.zero;
        _mesh.material = materials[UnityEngine.Random.Range(0, 2)];
        _rb.AddTorque(Vector3.one);
    }

    private void OnDestroy()
    {
        subscription.Dispose();
    }
}
