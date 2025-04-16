using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance { get; private set; }

    [SerializeField] private GameObject hostPanel;
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private TMP_Text sessionCodeText;
    [SerializeField] private TMP_InputField sessionCodeInput;
    [SerializeField] private TMP_Text errorText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowSessionCode(string code)
    {
        sessionCodeText.text = $"Код сессии: {code}";
        hostPanel.SetActive(true);
    }

    public void JoinSession()
    {
        string sessionCode = sessionCodeInput.text.ToUpper();
        Debug.Log(sessionCode);
        if (!string.IsNullOrEmpty(sessionCode))
        {
            CustomNetworkManager.Instance.JoinGame(sessionCode);
        }
        else
        {
            ShowError("Введите код!");
        }
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
