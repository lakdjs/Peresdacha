using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerHealth : NetworkBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float minRespawnTime;
    [SerializeField] private float maxRespawnTime;

    public void TakeDamage()
    {
        if (!isServer) return;

        RpcDie();
    }

    [ClientRpc]
    private void RpcDie()
    {
        gameObject.SetActive(false);
    }
}
