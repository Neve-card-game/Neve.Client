using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchManager : MonoBehaviour
{
    [SerializeField]
    private ServerConnector connector;
    public Button button_Ready;

    public TMPro.TextMeshProUGUI Player_Self;
    public TMPro.TextMeshProUGUI Player_Enemy;

    private void Awake()
    {
        connector = FindObjectOfType<ServerConnector>();
        Player_Self.text = connector.Loginplayer.Username;
        StartCoroutine(GetEnemyName());
    }

    IEnumerator GetEnemyName()
    {
        while (true)
        {
            yield return new WaitUntil(() => connector.RefreshRoomPlayerName());
            yield return new WaitForSeconds(0.1f);
            if (connector.EnemyPlayerName != null || connector.EnemyPlayerName.Length != 0)
            {
                Player_Enemy.text = connector.EnemyPlayerName;
            }
            else
            {
                Player_Enemy.text = "空位";
            }
        }
    }
}
