using System.Data.Common;
using System.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;

public class SignalRConnector
{
    private HubConnection connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7207/nevehub")
        .Build();
    private string returnMessage = null;
    private bool isReceive = false;
    private bool isRefreshRoomName = false;

    public async Task InitAsync()
    {
        await StartConnectionAsync();
        connection.On<string>(
            "ReceiveMessage",
            (message) =>
            {
                returnMessage = message;
                isReceive = true;
            }
        );

        connection.On<string>(
            "Check",
            (message) =>
            {
                if (message != null)
                {
                    UnityEngine.Debug.Log(message);
                    isRefreshRoomName = true;
                }
            }
        );
    }

    public async Task DisconnectAsync()
    {
        try
        {
            await connection.StopAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("stop failed" + ex);
        }
    }

    public async Task StartConnectionAsync()
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("start failed" + ex);
        }
    }

    public async Task Register(string email, string password, string username)
    {
        try
        {
            await connection.InvokeAsync("Register", email, password, username);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task Login(string email, string password)
    {
        try
        {
            await connection.InvokeAsync("Login", email, password);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task Logout(string email)
    {
        try
        {
            await connection.InvokeAsync("Logout", email);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async Task<bool> EmailExist(string email)
    {
        try
        {
            return await connection.InvokeAsync<bool>("Emailexist", email);
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
            return await connection.InvokeAsync<bool>("CheckPassword", email, password);
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
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    public async Task<bool> UpDataDeckList(string email, string decklist, int deckid)
    {
        try
        {
            return await connection.InvokeAsync<bool>("SaveDeckList", email, decklist, deckid);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }

    public async Task SendMessage(string userName, string Message)
    {
        try
        {
            await connection.InvokeAsync("SendMessage", userName, Message);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public string ReceiveMessage()
    {
        if (isReceive)
        {
            isReceive = false;
            return returnMessage;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> CreateRoom(string roomName, string roomPassword, string username)
    {
        try
        {
            return await connection.InvokeAsync<bool>(
                "CreateRoom",
                roomName,
                roomPassword,
                username
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public async Task<bool> JoinRoom(string roomName, string roomPassword, string username)
    {
        try
        {
            return await connection.InvokeAsync<bool>(
                "AddToRoom",
                roomName,
                roomPassword,
                username
            );
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public async Task<bool> LeftRoom(string roomName, string username)
    {
        try
        {
            return await connection.InvokeAsync<bool>("RemoveFromRoom", roomName, username);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }
    }

    public async Task<List<Room>> GetRoomList()
    {
        try
        {
            return await connection.InvokeAsync<List<Room>>("GetRoomList");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
    }

    public bool RefreshRoomName(){
        if(isRefreshRoomName){
            isRefreshRoomName = false;
            return true;
        }
        else{
            return false;
        }
    }
}
