<<<<<<< HEAD
using System;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalRConnector
{
    private HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:7207/nevehub").Build();
    public async Task InitAsync()
    {
        await StartConnectionAsync();
    }

    public async Task StartConnectionAsync()
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("¡¥Ω” ß∞‹"+ex);
        }
    }

    public async Task Register(string email,string password,string username)
    {
        try
        {
            await connection.InvokeAsync("Register",email,password,username);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task Login(string email,string password)
    {
        try
        {
            await connection.InvokeAsync("Login",email, password);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task<bool> EmailExist(string email)
    {
        try
        {
           return await connection.InvokeAsync<bool>("Eamilexist", email);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<bool> PasswordCheck(string email, string password)
    {
        try
        {
            return await connection.InvokeAsync<bool>("CheckPassword", email,password);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<bool> LoginCheck(string email)
    {
        try
        {
            return await connection.InvokeAsync<bool>("CheckLogin", email);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<User> GetPlayer(string email)
    {
        try
        {
            return await connection.InvokeAsync<User>("GetPlayerMessage", email);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public async Task<string[]> LoadDeckList(string email)
    {
        try
        {
            return await connection.InvokeAsync<string[]>("LoadDeckList", email);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    public async Task<bool> UpDataDeckList(string email,string decklist,int deckid)
    {
        try
        {
            return await connection.InvokeAsync<bool>("SaveDeckList",email,decklist,deckid);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }

}
=======
using System;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalRConnector
{
    private HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:7207/nevehub").Build();
    public async Task InitAsync()
    {
        await StartConnectionAsync();
    }

    public async Task StartConnectionAsync()
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("¡¥Ω” ß∞‹"+ex);
        }
    }

    public async Task Register(string email,string password,string username)
    {
        try
        {
            await connection.InvokeAsync("Register",email,password,username);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task Login(string email,string password)
    {
        try
        {
            await connection.InvokeAsync("Login",email, password);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task<bool> EmailExist(string email)
    {
        try
        {
           return await connection.InvokeAsync<bool>("Eamilexist", email);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<bool> PasswordCheck(string email, string password)
    {
        try
        {
            return await connection.InvokeAsync<bool>("CheckPassword", email,password);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<bool> LoginCheck(string email)
    {
        try
        {
            return await connection.InvokeAsync<bool>("CheckLogin", email);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }

    public async Task<User> GetPlayer(string email)
    {
        try
        {
            return await connection.InvokeAsync<User>("GetPlayerMessage", email);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public async Task<string[]> LoadDeckList(string email)
    {
        try
        {
            return await connection.InvokeAsync<string[]>("LoadDeckList", email);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    public async Task<bool> UpDataDeckList(string email,string decklist,int deckid)
    {
        try
        {
            return await connection.InvokeAsync<bool>("SaveDeckList",email,decklist,deckid);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }

}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
