using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        if (!isLocalPlayer) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

        CmdMove(moveDirection);
    }

    [Command]
    private void CmdMove(Vector3 moveDirection)
    {
        RpcMove(moveDirection);
    }

    [ClientRpc]
    private void RpcMove(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }
}
