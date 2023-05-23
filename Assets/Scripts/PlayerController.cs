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
    [SerializeField] private float speed;                    // �v���C���[�̑���
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // �i�s����
    public AudioClip hitWall, getItem;                          //SE
    AudioSource audioSource;                                                //����
    [SerializeField] public TextMeshProUGUI scoreText;      // �X�R�A�̃e�L�X�g
    [SerializeField] public TextMeshProUGUI getText;     // �Q�b�g�A�C�e����
    [SerializeField] public TextMeshProUGUI playTimeText;
    [SerializeField] public TextMeshProUGUI comboText;
    [SerializeField] public GameObject ClearPanel;          // �N���A���ɕ\������UI
    [SerializeField] public GameObject ComboObject;          // 
    [SerializeField] public int maxScore = 500;						// �N���A�̂��߂̕K�v�X�R�A
    [SerializeField] public int maxItem = 5;                //�����ɑ��݂���A�C�e����
    public static int score;                     			// ���݂̃X�R�A
    public static int item;                                       // �Q�b�g�����A�C�e����
    public bool clearFlag = false;                         // �N���A�������̃t���O
    public static float playTime;
    public static float comboTime;                                  // �R���{�o�ߎ��� 
    private int combo;
    public static int maxcombo;
    public static int hit;
    public Slider comboSlider;
    [SerializeField] public DataManager dataManager;        // �f�[�^�Ǘ��̃N���X
    public GameObject SceneController;
    public static List<string> dataStrs;                          // �쐬����csv�t�@�C���̒��g


    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbody�̎擾
        movement = Vector3.zero;
        audioSource = GetComponent<AudioSource>();          // AudioSource�̎擾
        score = 0;                                          // score�̏�����
        item = 0;
        scoreText.text = "Score : " + score.ToString();       // �\������X�R�A�̍X�V
        getText.text = "Item : 0 / " + maxItem.ToString();       // �\������X�R�A�̍X�V
        ClearPanel.SetActive(false);                        // �N���A���ɕ\������UI���\���ɂ���
        ComboObject.SetActive(false);
        playTime = 0;                                         // �v���C���Ԃ̏�����
        comboTime = 0;                                         // �R���{���Ԃ̏�����
        combo = 0;
        maxcombo = 0;
        hit = 0;
        SceneController = GameObject.Find("SceneController");
        dataStrs = new List<String>();                      // csv�t�@�C���̒��g��������
    }

    void FixedUpdate()
    {
        if (!clearFlag)     // �N���A���Ă��Ȃ��Ƃ�����\
        {
            rb.AddForce(movement * speed);                                        // �͂�������
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
        Vector2 moveDirection = value.Get<Vector2>();                       // �L�[���͂̕������擾
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);     // 3�����ɕϊ�
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))                           // PickUp�^�O�ɐڐG�����Ƃ�
        {
            audioSource.PlayOneShot(getItem);                               // �A�C�e���擾SE��炷
            Destroy(other.gameObject);                                      // �I�u�W�F�N�g����������
        }
        score += (100 + 50 * combo);                                                   // �X�R�A��100�|�C���g���_����
        combo++;
        if (combo > maxcombo)
        {
            maxcombo = combo;
        }
        comboTime = 0;
        item++;                                                            // �Q�b�g�����A�C�e�����𑝂₷
        scoreText.text = "Score : " + score.ToString();                   // �\������X�R�A�̍X�V
        getText.text = "Item : " + item.ToString() + " / " + maxItem.ToString();                   // �Q�b�g�A�C�e����
        ComboObject.SetActive(true);
        comboText.text = combo.ToString();
        if (item >= maxItem)                                                                                      // ���ׂẴA�C�e�����擾������
        {
            ClearPanel.SetActive(true);                                 // �N���A���ɕ\������UI��\������
            ComboObject.SetActive(false);
            clearFlag = true;               // �N���A�t���O�𗧂Ă�
            SceneController.GetComponent<SceneController>().ResultFade();
        }
        UpdateData(SceneManager.GetActiveScene().name + "," + item.ToString() + "," + score.ToString() + "," + hit.ToString() + "," + combo.ToString() + "," + maxcombo.ToString() + "," + playTime.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss"));
        // csv�t�@�C���ɏ������ރf�[�^��ǉ�
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))                         // Wall�^�O�ɐڐG�����Ƃ�
        {
            audioSource.PlayOneShot(hitWall);                               // �ǏՓ�SE��炷
            hit++;
            if (!clearFlag)
            {
                score -= 50;                                                    // 50�_���_
            }
            scoreText.text = "Score : " + score.ToString();                   // �\������X�R�A�̍X�V
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