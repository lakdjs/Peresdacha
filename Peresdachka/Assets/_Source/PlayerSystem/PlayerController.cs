using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float fireCooldown;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    

    private float lastFireTime;

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0) && Time.time - lastFireTime >= fireCooldown)
        {
            CmdFire();
            lastFireTime = Time.time;
        }
    }

    [Command]
    private void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
    }
}
