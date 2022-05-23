using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private ServerConnector connector;

    public Button button_MatchUp;

    public TMPro.TextMeshPro Player_Self;
    public TMPro.TextMeshPro Player_Enemy;
    private void Awake()
    {
        connector = FindObjectOfType<ServerConnector>();
    }

}
