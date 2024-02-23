using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {
        serverBtn.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartServer();
        }));
        hostBtn.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartHost();
        }));
        clientBtn.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartClient();
        }));
        
    }
}
