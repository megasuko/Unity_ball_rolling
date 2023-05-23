using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;                    // プレイヤーの速さ
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // 進行方向
    public AudioClip hitWall, getItem;                          //SE
    AudioSource audioSource;                                                //音源
    [SerializeField] public TextMeshProUGUI scoreText;      // スコアのテキスト
    [SerializeField] public TextMeshProUGUI getText;     // ゲットアイテム数
    [SerializeField] public TextMeshProUGUI playTimeText;
    [SerializeField] public TextMeshProUGUI comboText;
    [SerializeField] public GameObject ClearPanel;          // クリア時に表示するUI
    [SerializeField] public GameObject ComboObject;          // 
    [SerializeField] public int maxScore = 500;						// クリアのための必要スコア
    [SerializeField] public int maxItem = 5;                //初期に存在するアイテム数
    public static int score;                     			// 現在のスコア
    public static int item;                                       // ゲットしたアイテム数
    public bool clearFlag = false;                         // クリアしたかのフラグ
    public static float playTime;
    public static float comboTime;                                  // コンボ経過時間 
    private int combo;
    public static int maxcombo;
    public static int hit;
    public Slider comboSlider;
    [SerializeField] public DataManager dataManager;        // データ管理のクラス
    public GameObject SceneController;
    public static List<string> dataStrs;                          // 作成するcsvファイルの中身


    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbodyの取得
        movement = Vector3.zero;
        audioSource = GetComponent<AudioSource>();          // AudioSourceの取得
        score = 0;                                          // scoreの初期化
        item = 0;
        scoreText.text = "Score : " + score.ToString();       // 表示するスコアの更新
        getText.text = "Item : 0 / " + maxItem.ToString();       // 表示するスコアの更新
        ClearPanel.SetActive(false);                        // クリア時に表示するUIを非表示にする
        ComboObject.SetActive(false);
        playTime = 0;                                         // プレイ時間の初期化
        comboTime = 0;                                         // コンボ時間の初期化
        combo = 0;
        maxcombo = 0;
        hit = 0;
        SceneController = GameObject.Find("SceneController");
        dataStrs = new List<String>();                      // csvファイルの中身を初期化
    }

    void FixedUpdate()
    {
        if (!clearFlag)     // クリアしていないとき操作可能
        {
            rb.AddForce(movement * speed);                                        // 力を加える
            playTime += Time.deltaTime;
            playTimeText.text = playTime.ToString("f1") + "s";
            if (combo > 0)
            {
                comboTime += Time.deltaTime;
                comboSlider.value = 5f - comboTime;
                if (comboTime >= 5.0f)
                {
                    comboTime = 0;
                    combo = 0;
                    ComboObject.SetActive(false);
                }
            }
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();                       // キー入力の方向を取得
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);     // 3次元に変換
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))                           // PickUpタグに接触したとき
        {
            audioSource.PlayOneShot(getItem);                               // アイテム取得SEを鳴らす
            Destroy(other.gameObject);                                      // オブジェクトを消去する
        }
        score += (100 + 50 * combo);                                                   // スコアに100ポイント加点する
        combo++;
        if (combo > maxcombo)
        {
            maxcombo = combo;
        }
        comboTime = 0;
        item++;                                                            // ゲットしたアイテム数を増やす
        scoreText.text = "Score : " + score.ToString();                   // 表示するスコアの更新
        getText.text = "Item : " + item.ToString() + " / " + maxItem.ToString();                   // ゲットアイテム数
        ComboObject.SetActive(true);
        comboText.text = combo.ToString();
        if (item >= maxItem)                                                                                      // すべてのアイテムを取得したら
        {
            ClearPanel.SetActive(true);                                 // クリア時に表示するUIを表示する
            ComboObject.SetActive(false);
            clearFlag = true;               // クリアフラグを立てる
            SceneController.GetComponent<SceneController>().ResultFade();
        }
        UpdateData(SceneManager.GetActiveScene().name + "," + item.ToString() + "," + score.ToString() + "," + hit.ToString() + "," + combo.ToString() + "," + maxcombo.ToString() + "," + playTime.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss"));
        // csvファイルに書き込むデータを追加
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))                         // Wallタグに接触したとき
        {
            audioSource.PlayOneShot(hitWall);                               // 壁衝突SEを鳴らす
            hit++;
            if (!clearFlag)
            {
                score -= 50;                                                    // 50点減点
            }
            scoreText.text = "Score : " + score.ToString();                   // 表示するスコアの更新
            if (combo > 0)
            {
                comboTime = 0;
                combo = 0;
                ComboObject.SetActive(false);
            }
        }
    }

    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }
}