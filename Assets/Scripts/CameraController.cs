using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;                             // �v���C���[
    private Vector3 offset;                                         // �I�t�Z�b�g

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;     // �I�t�Z�b�g�̏�����
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;    // �I�t�Z�b�g��ێ������J�����ړ�
    }
}
