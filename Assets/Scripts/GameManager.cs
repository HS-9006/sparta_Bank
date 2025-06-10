using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userData;
    public ApiManager apiManager;

    // UI 연결용
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI userNameText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUserFromServer(); // 게임 시작 시 서버에서 데이터 불러오기
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    private void Start()
    {
        // Start에서 UI 갱신을 위해 LoadUser 내에서 콜백 처리

    }

    public void LoadUserFromServer()
    {
        StartCoroutine(apiManager.LoadUser("test123", (loadedUser) =>
        {
            if (loadedUser != null)
            {
                userData = loadedUser;
                Debug.Log($"서버에서 불러옴: {userData.userName}, {userData.cash}, {userData.bankBalance}");
                UpdateUI(); // UI 갱신
            }
            else
            {
                Debug.LogWarning("서버에서 데이터를 불러오지 못했습니다.");
            }
        }));
    }

    public void SaveUserToServer()
    {
        StartCoroutine(apiManager.SaveUser("test123", userData));
    }

    public void UpdateUI()
    {
        if (cashText != null) cashText.text = $"현금\n{userData.cash:N0}";
        if (balanceText != null) balanceText.text = $"Balance : {userData.bankBalance:N0}";
        if (userNameText != null) userNameText.text = userData.userName;
    }

    // 입금/출금 후 호출할 메서드 (예시)
    public void Deposit(int amount)
    {
        userData.bankBalance += amount;
        UpdateUI();
        SaveUserToServer();
    }

    private void OnApplicationQuit()
    {
        SaveUserToServer(); // 종료 시 저장
    }
}