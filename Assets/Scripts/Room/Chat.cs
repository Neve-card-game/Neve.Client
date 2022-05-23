using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    public TMPro.TMP_InputField InputMessage;
    public GameObject ChatList;
    public GameObject ChatMessagePrefab;
    public ServerConnector connection;

    //private int n = 0;

    private void Awake()
    {
        connection = FindObjectOfType<ServerConnector>();
        //StartCoroutine(ReceiveMessage());
    }

    IEnumerator ReceiveMessage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //GameObject message = Instantiate(ChatMessagePrefab);
            //message.name = "message" + n;
            //message.GetComponentInChildren<TextMeshProUGUI>().text = connection.receiveMessage();
            //message.transform.SetParent(ChatList.transform);

            //if (n % 2 != 0)
            //{
           //     message.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            //}
            //n++;
        }
    }

}
