using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ServerConnector : MonoBehaviour
{
    public MatchManager matchManager;
    public string JoinRoomName;
    public string EnemyPlayerName = null;
    public List<Card> MixDeck = new List<Card>();
    public Button Registerbutton;
    public Button Loginbutton;
    private SignalRConnector connector = new SignalRConnector();
    public InputField email;
    public InputField password;
    public InputField username;
    public InputField emailVerified;
    public InputField passwordVerified;
    public Text affirmtext;
    public Text Username;
    public Text LoginStatus;
    public Player Loginplayer;

    public GameState Match;

    private void OnEnable()
    {
        SignalRConnector.MatchStateChange += MatchStateChange;
        SignalRConnector.MainDeckChange += MainDeckChange;
        MatchManager.OnDrawCard += SendDrawCard;
        CardPlay.CardMoveToBF += SendCardState;
    }

    private void OnDisable()
    {
        SignalRConnector.MatchStateChange -= MatchStateChange;
        SignalRConnector.MainDeckChange -= MainDeckChange;
        MatchManager.OnDrawCard -= SendDrawCard;
        CardPlay.CardMoveToBF -= SendCardState;
    }

    public async Task Awake()
    {
        await connector.InitAsync();

        Registerbutton.onClick.AddListener(async () => await RegisterAsync());
        Loginbutton.onClick.AddListener(async () => await LoginAsync());
    }

    public async Task RegisterAsync()
    {
        affirmtext.text = "registering...";
        if (email.text == null || password.text == null || username.text == null)
        {
            affirmtext.text = "please fill in all the fields";
        }
        else if (!email.text.Contains("@") || !email.text.Contains(".com"))
        {
            affirmtext.text = "please enter a valid email";
        }
        else if (await connector.EmailExist(email.text))
        {
            affirmtext.text = "email already exists";
        }
        else
        {
            try
            {
                affirmtext.text = "registering...";
                await connector.Register(email.text, password.text, username.text);
                affirmtext.text = "register success";
            }
            catch
            {
                affirmtext.text = "register failed";
            }
        }
    }

    public async Task LoginAsync()
    {
        affirmtext.text = "logging in...";
        if (emailVerified.text == null || passwordVerified.text == null)
        {
            affirmtext.text = "please fill in all the fields";
        }
        else if (!emailVerified.text.Contains("@") || !emailVerified.text.Contains(".com"))
        {
            affirmtext.text = "please enter a valid email";
        }
        else if (!await connector.EmailExist(emailVerified.text))
        {
            affirmtext.text = "account does not exist. please register first";
        }
        else if (!await connector.PasswordCheck(emailVerified.text, passwordVerified.text))
        {
            affirmtext.text = "password is incorrect";
        }
        else if (await connector.LoginCheck(emailVerified.text))
        {
            affirmtext.text = "Already login!";
        }
        else
        {
            try
            {
                affirmtext.text = "logging in...";
                await connector.Login(emailVerified.text, passwordVerified.text);
                affirmtext.text = "login success";
                await LoadMain();
            }
            catch
            {
                affirmtext.text = "login failed";
            }
        }
    }

    public async Task<Player> LoadDeckList()
    {
        if (await connector.LoginCheck(Loginplayer.Email))
        {
            string[] decklist = await connector.LoadDeckList(Loginplayer.Email);
            Loginplayer.UsingDeckId = Convert.ToInt32(decklist[0]);
            if (decklist[1] == null) { }
            else
            {
                Loginplayer.PlayerDecks =
                    JsonConvert.DeserializeObject(decklist[1], typeof(List<Decks>)) as List<Decks>;
            }

            return Loginplayer;
        }
        else
        {
            Debug.LogError("login failed");
            return null;
        }
    }

    public async Task<bool> SaveDeckList(Player player)
    {
        if (await connector.LoginCheck(Loginplayer.Email))
        {
            return await connector.UpDataDeckList(
                player.Email,
                JsonConvert.SerializeObject(player.PlayerDecks),
                player.UsingDeckId
            );
        }
        else
        {
            return false;
        }
    }

    private async Task LoadMain()
    {
        if (await connector.LoginCheck(emailVerified.text))
        {
            Loginplayer = new Player(await connector.GetPlayer(emailVerified.text));
            Loginplayer.LoginStatus = await connector.LoginCheck(Loginplayer.Email);

            SceneManager.LoadSceneAsync(3);
        }
    }

    public async Task SendMessage(string UserName, string Message)
    {
        await connector.SendMessage(UserName, Message);
    }

    public string ReceiveMessage()
    {
        return connector.ReceiveMessage();
    }

    public async Task<bool> CreateRoom(string RoomName, string RoomPassword)
    {
        if (JoinRoomName == null || JoinRoomName.Length == 0)
        {
            JoinRoomName = RoomName;
            return await connector.CreateRoom(RoomName, RoomPassword, Loginplayer.Username);
        }
        else
        {
            UnityEngine.Debug.Log(JoinRoomName + ":" + Loginplayer.Username);
            return false;
        }
    }

    public async Task<bool> JoinRoom(string RoomName, string RoomPassword)
    {
        if (JoinRoomName == null || JoinRoomName.Length == 0)
        {
            JoinRoomName = RoomName;
            return await connector.JoinRoom(RoomName, RoomPassword, Loginplayer.Username);
        }
        else
        {
            return false;
        }
    }

    public async void LeftRoom()
    {
        await connector.LeftRoom(JoinRoomName, Loginplayer.Username);
        JoinRoomName = null;
    }

    public async Task<List<Room>> GetRoomList()
    {
        return await connector.GetRoomList();
    }

    public bool RefreshRoomPlayerName()
    {
        if (JoinRoomName.Length > 0 && connector.RefreshRoomName())
        {
            RefreshName();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async void RefreshName()
    {
        List<Room> RoomList = await GetRoomList();
        foreach (var room in RoomList)
        {
            if (room.RoomName == JoinRoomName)
            {
                if (room.RoomNumberOfPeople == 1)
                {
                    EnemyPlayerName = null;
                    break;
                }
                else
                {
                    foreach (var PlayerName in room.RoomMemberList)
                    {
                        if (PlayerName != Loginplayer.Username)
                        {
                            EnemyPlayerName = PlayerName;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public async void Disconnect()
    {
        try
        {
            await connector.Logout(Loginplayer.Email);
        }
        catch { }
        await connector.DisconnectAsync();
    }

    //
    public async void StartMatch()
    {
        Match = GameState.Match_Start;
        if (JoinRoomName != null)
            await connector.StartMatch(JoinRoomName, Match);
    }

    public async void PreMatch()
    {
        await LoadDeckList();
        if (JoinRoomName != null)
        {
            Debug.Log(Loginplayer.PlayerDecks[0].DeckName);
            await connector.PreMatch(JoinRoomName, Loginplayer.PlayerDecks[0].Deck, Match);
        }
    }

    //
    private void MatchStateChange(GameState match)
    {
        Match = match;
    }

    private void MainDeckChange(List<Card> deck)
    {
        if (deck != null)
        {
            Debug.Log("更新成功");
            MixDeck = deck;
        }
        else
        {
            Debug.Log("返回Null值");
        }
    }

    private async void SendDrawCard(string name)
    {
        if (JoinRoomName != null)
            await connector.SendDrawCard(JoinRoomName, name);
    }

    private async void SendCardState(string name, CardState cardState)
    {
        if (JoinRoomName != null)
            await connector.SendCardState(JoinRoomName, name,cardState);
    }

    //
    public async void RefreshMainDeck()
    {
        if (Match > 0 && JoinRoomName != null)
        {
            await connector.RefreshMainDeck(JoinRoomName, MixDeck);
        }
    }
}
