using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ATMUIManager : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textCash;
    public TextMeshProUGUI textBalance;

    // 유저 정보를 불러와 UI에 표시하는 함수
    public void Refresh()
    {
        UserData user = GameManager.Instance.userData;

        textName.text = "이름: " + user.userName;
        textCash.text = "현금: " + string.Format("{0:N0}", user.cash) + "원";
        textBalance.text = "Balance: " + string.Format("{0:N0}", user.bankBalance) + "원";
    }

    private void Start()
    {
        Refresh(); // 게임 시작 시 한 번 UI 업데이트
    }
}