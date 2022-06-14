using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRoom : MonoBehaviour
{
    private ServerConnector connector;
    void Start()
    {
        try{
            connector = FindObjectOfType<ServerConnector>();
        }
        catch{}
    }

    public void LeftThisRoom(){
        connector.LeftRoom();
    }
}
