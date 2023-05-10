using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;                    // プレイヤーの速さ
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // 進行方向
    public AudioClip hitWall, getItem;                          //SE
    AudioSource audioSource;                                                //音源
    [SerializeField] public TextMeshProUGUI scoreText;      // スコアのテキスト
    [SerializeField] public GameObject ClearPanel;          // クリア時に表示するUI
    [SerializeField] public int maxScore = 500;						// クリアのための必要スコア
    private int score;                     									// 現在のスコア
    [SerializeField] public DataManager dataManager;        // データ管理のクラス

    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbodyの取得
        movement = Vector3.zero;
        audioSource = GetComponent<AudioSource>();          // AudioSourceの取得
        score = 0;                                          // scoreの初期化
        scoreText.text = "Score:" + score.ToString();       // 表示するスコアの更新
        ClearPanel.SetActive(false);                        // クリア時に表示するUIを非表示にする
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed);                                        // 力を加える
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
        score += 100;                                                   // スコアに100ポイント加点する
        scoreText.text = "Score:" + score.ToString();                   // 表示するスコアの更新
        if (score >= maxScore)                                                                                      // 必要スコアに到達したら
        {
            ClearPanel.SetActive(true);                                 // クリア時に表示するUIを表示する
        }
        dataManager.UpdateData(score.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss"));
        // csvファイルに書き込むデータを追加
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))                         // Wallタグに接触したとき
        {
            audioSource.PlayOneShot(hitWall);                               // 壁衝突SEを鳴らす
        }
    }
}