using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;                    // �v���C���[�̑���
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // �i�s����
    public AudioClip hitWall, getItem;                          //SE
    AudioSource audioSource;                                                //����
    [SerializeField] public TextMeshProUGUI scoreText;      // �X�R�A�̃e�L�X�g
    [SerializeField] public GameObject ClearPanel;          // �N���A���ɕ\������UI
    [SerializeField] public int maxScore = 500;						// �N���A�̂��߂̕K�v�X�R�A
    private int score;                     									// ���݂̃X�R�A
    [SerializeField] public DataManager dataManager;        // �f�[�^�Ǘ��̃N���X

    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbody�̎擾
        movement = Vector3.zero;
        audioSource = GetComponent<AudioSource>();          // AudioSource�̎擾
        score = 0;                                          // score�̏�����
        scoreText.text = "Score:" + score.ToString();       // �\������X�R�A�̍X�V
        ClearPanel.SetActive(false);                        // �N���A���ɕ\������UI���\���ɂ���
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed);                                        // �͂�������
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
        score += 100;                                                   // �X�R�A��100�|�C���g���_����
        scoreText.text = "Score:" + score.ToString();                   // �\������X�R�A�̍X�V
        if (score >= maxScore)                                                                                      // �K�v�X�R�A�ɓ��B������
        {
            ClearPanel.SetActive(true);                                 // �N���A���ɕ\������UI��\������
        }
        dataManager.UpdateData(score.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss"));
        // csv�t�@�C���ɏ������ރf�[�^��ǉ�
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))                         // Wall�^�O�ɐڐG�����Ƃ�
        {
            audioSource.PlayOneShot(hitWall);                               // �ǏՓ�SE��炷
        }
    }
}