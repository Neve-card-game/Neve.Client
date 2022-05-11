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
        Registerbutton.onClick.AddListener(async ()=> await RegisterAsync());
        Loginbutton.onClick.AddListener(async ()=> await LoginAsync());
        

    }

   
    public async Task RegisterAsync()
    {
        affirmtext.text = "�����";
        if (email.text == null && password.text == null && username.text == null)
        {
            affirmtext.text = "��д�ֶβ���Ϊ�գ�";
        }
        else if (!email.text.Contains("@") || !email.text.Contains(".com"))
        {
            affirmtext.text = "����д��ȷ�����䣡";
        }
        else if (await connector.EmailExist(email.text))
        {
            affirmtext.text = "�����Ѿ���ע�ᣡ";
        }
        else {

            try
            {
                affirmtext.text = "ע����";
                await connector.Register(email.text, password.text, username.text);
                affirmtext.text = "ע��ɹ�";
            }
            catch
            {
                affirmtext.text = "ע��ʧ��";
            }
        }
    }

    public async Task LoginAsync()
    {
        affirmtext.text = "�����";
        if (emailVerified.text == null && passwordVerified.text == null)
        {
            affirmtext.text = "��д�ֶβ���Ϊ�գ�";
        }
        else if (!emailVerified.text.Contains("@") || !emailVerified.text.Contains(".com"))
        {
            affirmtext.text = "����д��ȷ�����䣡";
        }
        else if (!await connector.PasswordCheck(emailVerified.text,passwordVerified.text))
        {
            affirmtext.text = "���������������û�������";
        }
        else
        {

            try
            {
                affirmtext.text = "��½��";
                await connector.Login(emailVerified.text,passwordVerified.text);
                affirmtext.text = "��½�ɹ�";
                await LoadMain();
            }
            catch
            {
                affirmtext.text = "��½ʧ��";
            }
        }
    }

    public async Task<Player> LoadDeckList()
    {
        if (await connector.LoginCheck(Loginplayer.Email))
        {
            string[] decklist = await connector.LoadDeckList(Loginplayer.Email);
            Loginplayer.UsingDeckId = Convert.ToInt32(decklist[0]);
            if(decklist[1] == null)
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
            Debug.LogError("δ����");
            return null;
        }
    }

    public async Task<bool> SaveDeckList(Player player)
    {
        if (await connector.LoginCheck(Loginplayer.Email))
        {
           return await connector.UpDataDeckList(player.Email,JsonConvert.SerializeObject(player.PlayerDecks),player.UsingDeckId);
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
            
            Loginplayer =  new Player( await connector.GetPlayer(emailVerified.text));
            Loginplayer.LoginStatus = await connector.LoginCheck(Loginplayer.Email);

            SceneManager.LoadSceneAsync(3);
            
        }

    }

    
}
