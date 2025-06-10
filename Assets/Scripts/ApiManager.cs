using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:3000/user/";

    // 유저 불러오기 (GET)
    public IEnumerator LoadUser(string userName, System.Action<UserData> onComplete)
    {
        UnityWebRequest www = UnityWebRequest.Get(baseUrl + userName);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            UserData data = JsonUtility.FromJson<UserData>(json);
            onComplete?.Invoke(data);
        }
        else
        {
            Debug.LogError("GET 실패: " + www.error);
        }
    }

    // 유저 저장하기 (POST)
    public IEnumerator SaveUser(string userName, UserData data)
    {
        string json = JsonUtility.ToJson(data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(baseUrl + userName, "POST");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST 성공");
        }
        else
        {
            Debug.LogError("POST 실패: " + www.error);
        }
    }
}