using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName;
    public string name;
    public string password;
    public int cash;
    public int bankBalance;

    public UserData() { }

    public UserData(string name, int baseCash, int baseBalance)
    {
        userName = name;
        cash = baseCash;
        bankBalance = baseBalance;
    }

    public UserData(string userName, string name, string password, int cash, int bankBalance)
    {
        this.userName = userName;
        this.name = name;
        this.password = password;
        this.cash = cash;
        this.bankBalance = bankBalance;
    }
}