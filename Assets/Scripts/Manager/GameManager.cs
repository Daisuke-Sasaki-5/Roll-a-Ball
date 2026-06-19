using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private bool isStarted = false; // ゲーム開始済みフラグ

    private float timer;

    // チェックポイント通過数
    private int checkPointCount;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartCoroutine(WaitForFadeThenInit());
        }
    }

    private IEnumerator WaitForFadeThenInit()
    {
        // フェード完了を待つ
        if (FadeManager.instance != null)
        {
            while (!FadeManager.instance.IsFadeComplete) yield return null;
        }
        InitGame();
    }

    /// <summary>
    ///  ゲームの初期化
    /// </summary>
    private void InitGame()
    {
        // 現在のシーンをチェック
        if (SceneManager.GetActiveScene().name != "GameScene") return;

        isStarted = false;
        timer = 0f;
        UIManager.Instance.UpdateTimeUI(timer);

        // チェックポイントのリセット
        checkPointCount = 0;

        // プレイヤー操作禁止
        FindObjectOfType<Player>().enabled = false;

        UIManager.Instance.ShowStartUI(true);
        UIManager.Instance.UpdateCheckPointUI(checkPointCount);

        // ゲーム停止中
        Time.timeScale = 0f;
    }

    private void Update()
    {
        // ==== エンターキーでゲームスタート ====
        if (!isStarted && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartGame();
        }

        // ゲーム開始時だけ時間加算
        if(isStarted)
        {
            timer += Time.deltaTime;
            UIManager.Instance.UpdateTimeUI(timer);
        }
    }

    private void StartGame()
    {
        isStarted = true;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UIManager.Instance.ShowStartUI(false);

        //startTiem = Time.time;

        FindObjectOfType<Player>().enabled = true;
    }

    public void AddCheckPoint()
    {
        checkPointCount++;

        UIManager.Instance.UpdateCheckPointUI(checkPointCount);
    }

    public void TryClear()
    {
        Debug.Log("クリア");
        //clearTime = timer;
        PlayerPrefs.SetFloat("ClearTime", timer);
        FadeManager.instance.FadeToScene("ResultScene");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // ==== シーンリセット ====
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            StartCoroutine(WaitForFadeThenInit());
        }
        else
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
