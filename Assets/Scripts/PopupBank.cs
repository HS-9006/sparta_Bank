using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    public GameObject atm;             // ATM 버튼 영역
    public GameObject depositPanel;   // 입금 패널
    public GameObject withdrawPanel;  // 출금 패널

    // 입금 화면 열기
    public void ShowDeposit()
    {
        atm.SetActive(false);                // ATM 버튼 숨기기
        depositPanel.SetActive(true);        // 입금 화면 표시
        withdrawPanel.SetActive(false);      // 출금 화면 숨기기
    }

    // 출금 화면 열기
    public void ShowWithdraw()
    {
        atm.SetActive(false);                // ATM 버튼 숨기기
        depositPanel.SetActive(false);       // 입금 화면 숨기기
        withdrawPanel.SetActive(true);       // 출금 화면 표시
    }

    // 초기 ATM 화면으로 돌아가기
    public void BackToATM()
    {
        atm.SetActive(true);              // ATM 다시 보이기
        depositPanel.SetActive(false);   // 입금 패널 끄기
        withdrawPanel.SetActive(false);  // 출금 패널 끄기
    }

}