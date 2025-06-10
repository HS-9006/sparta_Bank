using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class LoginRequest
{
    public string userName;
    public string password;
}

public class LoginManager : MonoBehaviour
{
    [Header("UI 연결")]
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public GameObject popupLogin;             // 로그인창 (PopupBank 안의 자식)
    public GameObject popupRegister; // 새로 연결할 회원가입 UI
    public GameObject background;             // ATM 배경 UI
    public GameObject depositButtonGroup;     // ATM 버튼 영역

    [Header("BankManager 연결")]
    public BankManager bankManager;

    void Start()
    {
        // 시작 시 UI 초기화
        popupLogin.SetActive(true);
        background.SetActive(false);
        depositButtonGroup.SetActive(false);
    }

    public void OpenRegisterUI()
    {
        popupLogin.SetActive(false);       // 로그인 끄기
        popupRegister.SetActive(true);     // 회원가입 켜기
    }
    public void BackToLoginUI()
    {
        popupRegister.SetActive(false);  // 회원가입 창 닫기
        popupLogin.SetActive(true);      // 로그인 창 열기
    }

    public void OnLoginClicked()
    {
        StartCoroutine(SendLoginRequest());
    }

    IEnumerator SendLoginRequest()
    {
        // 요청 데이터 생성
        LoginRequest req = new LoginRequest
        {
            userName = inputID.text,
            password = inputPW.text
        };

        string json = JsonUtility.ToJson(req);

        // POST 요청 생성
        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/api/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // 로그인 성공 → UserData 받기
            UserData user = JsonUtility.FromJson<UserData>(www.downloadHandler.text);

            // BankManager에 데이터 전달
            bankManager.SetInitialValues(user.name, user.cash, user.bankBalance);

            // UI 전환 처리
            popupLogin.SetActive(false);             // 로그인창 끄기
            background.SetActive(true);              // ATM 배경 켜기
            depositButtonGroup.SetActive(true);      // 버튼 영역 켜기
        }
        else
        {
            Debug.LogError("로그인 실패: " + www.error);
        }
    }
}