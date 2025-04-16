using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SessionManager : NetworkBehaviour
{
    public static SessionManager Instance { get; private set; }

    private Dictionary<string, string> sessions = new Dictionary<string, string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public string CreateSession(string hostAddress)
    {
        string sessionCode = GenerateSessionCode();
        sessions[sessionCode] = hostAddress;
        return sessionCode;
    }

    public string GetHostAddress(string sessionCode)
    {
        return sessions.TryGetValue(sessionCode, out string hostAddress) ? hostAddress : null;
    }

    private string GenerateSessionCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] code = new char[6];
        for (int i = 0; i < 6; i++)
        {
            code[i] = chars[Random.Range(0, chars.Length)];
        }
        return new string(code);
    }
}
