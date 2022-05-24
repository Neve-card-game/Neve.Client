using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Chat : MonoBehaviour
{
    public TMPro.TMP_InputField InputMessage;
    public GameObject ChatList;
    public GameObject ChatMessagePrefab;
    public ServerConnector connection;
    public Button SendButton;
    public GameObject ChatArea;

    private int n = 0;

    private void Awake()
    {
        connection = FindObjectOfType<ServerConnector>();

        SendButton.onClick.AddListener(SendMessage);
        InputMessage.onEndEdit.AddListener((temp) => KeyEnter());
        StartCoroutine(ReceiveMessage());
    }

    public async void SendMessage()
    {
        if (InputMessage.text != null)
        {
            await connection.SendMessage(connection.Loginplayer.Username, InputMessage.text);
            InputMessage.text = null;
        }
    }

    public void KeyEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SendMessage();
        }
    }

    IEnumerator ReceiveMessage()
    {
        while (true)
        {
            string Message = null;
            yield return new WaitUntil(
                () =>
                {
                    Task.Delay(10);
                    Message = connection.ReceiveMessage();
                    if (Message != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            );
            GameObject message = Instantiate(ChatMessagePrefab);
            message.name = "message" + n;
            message.GetComponentInChildren<TextMeshProUGUI>().text = Message;
            message.transform.SetParent(ChatList.transform);

            if (n % 2 != 0)
            {
                message.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            }
            n++;
            yield return new WaitForSeconds(0.01f);
            ChatArea.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        }
    }
}
