using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterManager : MonoBehaviour
{
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public TMP_InputField inputPWCheck;
    public TMP_InputField inputName;

    public GameObject popupLogin;
    public GameObject popupRegister;

    // [가입하기] 버튼 클릭 시 실행
    public void OnRegisterClicked()
    {
        StartCoroutine(SendRegisterRequest());
    }

    IEnumerator SendRegisterRequest()
    {
        string userName = inputID.text;
        string password = inputPW.text;

        var json = $"{{\"userName\":\"{userName}\",\"password\":\"{password}\"}}";

        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/api/register", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("회원가입 성공");

            // 회원가입 성공 시 UI 전환
            popupRegister.SetActive(false);
            popupLogin.SetActive(true);
        }
        else
        {
            Debug.LogError("회원가입 실패: " + www.error);
        }
    }

    public void OnGotoRegister()
    {
        popupLogin.SetActive(false);
        popupRegister.SetActive(true);
    }

    // 뒤로가기 버튼
    public void OnBackToLogin()
    {
        popupRegister.SetActive(false);
        popupLogin.SetActive(true);
    }
}