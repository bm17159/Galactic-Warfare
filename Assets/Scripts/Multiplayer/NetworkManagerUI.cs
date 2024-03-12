using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private GameObject fighter;
    [SerializeField] private GameObject heavy;
    [SerializeField] private GameObject speedster;

    private void Awake()
    {
        serverBtn.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartServer();
            serverBtn.gameObject.SetActive(false);
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
        }));
        hostBtn.onClick.AddListener((() =>
        {
            //NetworkManager.Singleton
            NetworkManager.Singleton.StartHost();
            serverBtn.gameObject.SetActive(false);
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
        }));
        clientBtn.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartClient();
            serverBtn.gameObject.SetActive(false);
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
        }));
        
    }
}
