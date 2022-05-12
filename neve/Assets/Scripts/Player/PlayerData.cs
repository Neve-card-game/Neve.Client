<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    
    private ServerConnector connector;
    private Text Username;
    private Text LoginStatus;

    private void Awake()
    {
        connector = FindObjectOfType<ServerConnector>();

        if (GameObject.FindGameObjectWithTag("Username") != null && GameObject.FindGameObjectWithTag("Loginstatus") != null && connector != null)
        {
            Username = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>();
            LoginStatus = GameObject.FindGameObjectWithTag("Loginstatus").GetComponent<Text>();
            Username.text = connector.Loginplayer.Username;
            LoginStatus.text = connector.Loginplayer.Status ? "在线" : "离线";

        }
    }



}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    
    private ServerConnector connector;
    private Text Username;
    private Text LoginStatus;

    private void Awake()
    {
        connector = FindObjectOfType<ServerConnector>();

        if (GameObject.FindGameObjectWithTag("Username") != null && GameObject.FindGameObjectWithTag("Loginstatus") != null && connector != null)
        {
            Username = GameObject.FindGameObjectWithTag("Username").GetComponent<Text>();
            LoginStatus = GameObject.FindGameObjectWithTag("Loginstatus").GetComponent<Text>();
            Username.text = connector.Loginplayer.Username;
            LoginStatus.text = connector.Loginplayer.Status ? "在线" : "离线";

        }
    }



}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
