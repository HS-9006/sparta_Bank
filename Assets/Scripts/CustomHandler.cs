using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomHandler : MonoBehaviour
{
    public TMP_InputField depositInputField; // 입금
    public TMP_InputField withdrawInputField; // 출금
    public BankManager bankManager;             // BankManager 참조

    // 사용자 지정 금액 입금 버튼 클릭 시 호출
    public void OnClickDepositCustom()
    {
        string input = depositInputField.text;

        if (int.TryParse(input, out int amount) && amount > 0)
        {
            bankManager.DepositFixedAmount(amount);  // 입금 요청
        }
        else
        {
            Debug.Log("숫자만 입력하세요.");
        }

        depositInputField.text = ""; // 입력 초기화
    }

    // 사용자 지정 금액 출금 버튼 클릭 시 호출
    public void OnClickWithdrawCustom()
    {
        string input = withdrawInputField.text;

        if (int.TryParse(input, out int amount) && amount > 0)
        {
            bankManager.WithdrawFixedAmount(amount); // 출금 요청
        }
        else
        {
            Debug.Log("숫자만 입력하세요.");
        }

        withdrawInputField.text = ""; // 입력 초기화
    }

}