using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class RoomListManager : MonoBehaviour
{
    private ServerConnector connector;

    private string RoomName_Join = null;
    public TextMeshProUGUI RoomMessage;
    public Button CreateRoomButton;
    public Button JoinRoomButton;
    public Button SearchRoomButton;
    public Button RefreshRoomListButton;

    public TMP_InputField RoomName_Create;
    public TMP_InputField RoomName_Search;
    public TMP_InputField RoomPassword_Create;
    public TMP_InputField RoomPassword_Join;

    public GameObject RoomList;
    public GameObject RoomPrefab;
    public RectTransform canvas;

    private void OnEnable()
    {
        RoomClick.RoomOnClick += RoomClickHandler;
    }

    private void OnDisable()
    {
        RoomClick.RoomOnClick -= RoomClickHandler;
    }

    private async void Awake()
    {
        connector = FindObjectOfType<ServerConnector>();

        RefreshRoomList(await connector.GetRoomList());

        RefreshRoomListButton.onClick.AddListener(
            async () => RefreshRoomList(await connector.GetRoomList())
        );

        CreateRoomButton.onClick.AddListener(CreateRoom);

        JoinRoomButton.onClick.AddListener(JoinRoom);

        SearchRoomButton.onClick.AddListener(SearchRoom);
    }

    private void RefreshRoomList(List<Room> roomList)
    {
        RoomList.transform.DestroyChildren();
        int n = 0;
        if (roomList != null)
        {
            foreach (var room in roomList)
            {
                GameObject Rooms = Instantiate(RoomPrefab);
                Rooms.name = room.RoomName;
                Rooms.transform.localScale = canvas.localScale;
                Rooms.transform.Find("RoomId").GetComponent<TextMeshProUGUI>().text = room.RoomId;
                Rooms.transform.Find("RoomName").GetComponent<TextMeshProUGUI>().text =
                    room.RoomName;
                Rooms.transform.Find("RoomNumberOfPeople").GetComponent<TextMeshProUGUI>().text =
                    room.RoomNumberOfPeople.ToString();
                Rooms.transform.SetParent(RoomList.transform);
                if (n % 2 == 0)
                {
                    Rooms.GetComponent<Image>().color = new Color(
                        248 / 255f,
                        176 / 255f,
                        193 / 255f
                    );
                    Rooms.GetComponent<RoomClick>().originColor = new Color(
                        248 / 255f,
                        176 / 255f,
                        193 / 255f
                    );
                }
                n++;
            }
        }
    }

    private async void CreateRoom()
    {
        if (await connector.CreateRoom(RoomName_Create.text, RoomPassword_Create.text))
        {
            RoomMessage.text = "建房成功";
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            RoomMessage.text = "建房失败，房间名重复或者其他原因";
        }
        RoomName_Create.text = null;
        RoomPassword_Create.text = null;
    }

    private async void JoinRoom()
    {
        if (RoomName_Join == null)
        {
            RoomMessage.text = "请点击正确的房间";
        }
        else
        {
            if (!await CheckRoomMembers())
            {
                RoomMessage.text = "尝试加入房间...";
                if (await connector.JoinRoom(RoomName_Join, RoomPassword_Join.text))
                {
                    RoomMessage.text = "加入房间成功";
                    SceneManager.LoadSceneAsync(1);
                }
                else
                {
                    RoomMessage.text = "加入失败";
                }
            }
            else
            {
                RoomMessage.text = "房间已满";
            }
        }
        RoomPassword_Join.text = null;
    }

    private void RoomClickHandler(PointerEventData eventData)
    {
        Color thiscolor = eventData.pointerClick.GetComponent<Image>().color;
        eventData.pointerClick.GetComponent<Image>().color = new Color(
            thiscolor.r - 30 / 255f,
            thiscolor.g - 30 / 255f,
            thiscolor.b - 30 / 255f
        );
        RoomName_Join = eventData.pointerClick.name;
    }

    private async void SearchRoom()
    {
        List<Room> roomList = await connector.GetRoomList();
        List<Room> searchRoom = new List<Room>();
        foreach (var room in roomList)
        {
            if (
                room.RoomId.Contains(RoomName_Search.text)
                || room.RoomName.Contains(RoomName_Search.text)
            )
            {
                searchRoom.Add(room);
            }
        }
        RefreshRoomList(searchRoom);
    }

    private async Task<bool> CheckRoomMembers()
    {
        bool IsFullRoom = false;
        List<Room> RoomList = await connector.GetRoomList();
        foreach (var room in RoomList)
        {
            if (room.RoomName == RoomName_Join)
            {
                if (room.RoomNumberOfPeople >= 2)
                    IsFullRoom = true;
                else
                {
                    IsFullRoom = false;
                }
                break;
            }
        }
        return IsFullRoom;
    }
}
