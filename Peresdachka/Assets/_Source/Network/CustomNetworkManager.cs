using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private GameObject redPlayerPrefab;
    [SerializeField] private GameObject bluePlayerPrefab;

    [SerializeField] private Transform redSpawnPoint;
    [SerializeField] private Transform blueSpawnPoint;

    private int playerCount;
    private string currentSessionCode;

    public static CustomNetworkManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void HostGame()
    {
        StartHost();
        string ip = "localhost";
        currentSessionCode = SessionManager.Instance.CreateSession(ip);
        Debug.Log($"Сессия создана! Код: {currentSessionCode}");
        UImanager.Instance.ShowSessionCode(currentSessionCode);
    }

    public void JoinGame(string sessionCode)
    {
        string hostAddress = SessionManager.Instance.GetHostAddress(sessionCode);
        if (!string.IsNullOrEmpty(hostAddress))
        {
            Debug.Log($"Подключаемся к {hostAddress}...");
            networkAddress = hostAddress;
            StartClient();
        }
        else
        {
            Debug.LogError("Неверный код сессии!");
            UImanager.Instance.ShowError("Сессия не найдена!");
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        GameObject playerPrefab = (playerCount % 2 == 0) ? redPlayerPrefab : bluePlayerPrefab;
        Transform spawnPoint = (playerCount % 2 == 0) ? redSpawnPoint : blueSpawnPoint;

        if (spawnPoint == null)
        {
            spawnPoint = FindObjectOfType<NetworkStartPosition>()?.transform;
        }

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        NetworkServer.AddPlayerForConnection(conn, player);

        playerCount++;
    }
}
