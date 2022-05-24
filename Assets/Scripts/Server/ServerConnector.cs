using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ServerConnector : MonoBehaviour
{
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
    public GameObject loginpanel;
    public GameObject mainpanel;

    public Player Loginplayer;



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
            if (decklist[1] == null)
            {

            }
            else
            {
                Loginplayer.PlayerDecks = JsonConvert.DeserializeObject(decklist[1], typeof(List<Decks>)) as List<Decks>;
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
            return await connector.UpDataDeckList(player.Email, JsonConvert.SerializeObject(player.PlayerDecks), player.UsingDeckId);
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

    public async Task SendMessage(string UserName,string Message){
        await connector.SendMessage(UserName,Message);
    }

    public string ReceiveMessage(){
        return connector.ReceiveMessage();
    }

}
