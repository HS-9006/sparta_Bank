using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class BankManager : MonoBehaviour
{
    public TextMeshProUGUI balanceText;    // Text_Info
    public TextMeshProUGUI cashText;       // Text_Cash
    public TextMeshProUGUI nameText;
    public GameObject Close_Popup;         // 팝업 오브젝트

    private int balance = 0; // 예금 잔액
    private int cash = 0; // 현금 보유액
    private string userName = "";

    public void SetInitialValues(string userName, int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
        UpdateUI();
    }

    void Start()
    {
        // 시작 시 텍스트에서 숫자 추출하여 변수 초기화
        balance = ParseTextToInt(balanceText.text);
        cash = ParseTextToInt(cashText.text);
        UpdateUI(); // UI에 값 반영
    }


    // 고정 금액 입금
    public void DepositFixedAmount(int amount)
    {
        if (cash >= amount)
        {
            cash -= amount;
            balance += amount;
            UpdateUI();

            // 팝업이 혹시 켜져 있으면 끄기
            if (Close_Popup != null)
                Close_Popup.SetActive(false);
            SyncToGameManager();
        }
        else
        {
            // 현금 부족
            if (Close_Popup != null)
                Close_Popup.SetActive(true);
            
        }
    }

    // 고정 금액 출금
    public void WithdrawFixedAmount(int amount)
    {
        Debug.Log("[출금 버튼 클릭됨] 금액: " + amount);
        if (balance >= amount)
        {
            balance -= amount;
            cash += amount;
            UpdateUI();

            if (Close_Popup != null)
                Close_Popup.SetActive(false); // 팝업 끄기 (남아있을 경우)
            SyncToGameManager();
        }
        else
        {
            if (Close_Popup != null)
                Close_Popup.SetActive(true);  // 잔액 부족 팝업
            Debug.Log("출금 실패: 잔액 부족");
            
        }
    }

    // 텍스트에서 숫자만 추출해서 정수로 변환
    private int ParseTextToInt(string input)
    {
        string numeric = Regex.Replace(input, @"[^\d]", ""); // 정규식 사용해서 숫자 이외 문자 제거
        return int.TryParse(numeric, out int result) ? result : 0;
    }

    // UI 갱신
    private void UpdateUI()
    {
        balanceText.text = "Balance : " + balance.ToString("N0");
        cashText.text = "현금\n" + cash.ToString("N0");
        //nameText.text = userName;
    }

    private void SyncToGameManager()
    {
        if (GameManager.Instance != null && GameManager.Instance.userData != null)
        {
            GameManager.Instance.userData.cash = cash;
            GameManager.Instance.userData.bankBalance = balance;

            GameManager.Instance.SaveUserToServer();
        }
    }

}