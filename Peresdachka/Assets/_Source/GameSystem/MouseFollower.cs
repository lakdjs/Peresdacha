using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private IDisposable subscription;

    private void Start()
    {
        subscription = Observable.EveryUpdate()
            .Subscribe(x =>
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z);
                mousePos = cam.ScreenToWorldPoint(mousePos);
                mousePos.z = 0;
                transform.position = mousePos;
            });
    }
    private void OnDestroy()
    {
        subscription?.Dispose();
    }
}
