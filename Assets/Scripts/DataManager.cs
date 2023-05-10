using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    private StreamWriter sw;
    private string dirPath;                                 // �쐬����csv�̃f�B���N�g��
    private string fileName;                                // �쐬����csv�̃t�@�C����
    private string[] header;                                // �쐬����csv�t�@�C���̃w�b�_�[
    private List<string> dataStrs;                          // �쐬����csv�t�@�C���̒��g


    void Start()
    {
        dirPath = Application.dataPath + "/Scripts/CSV/";   // Assets/Scripts/CSV���w��
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss"); // �V�[���̊J�n���Ԃ��t�@�C�����Ɏw��
        header = new string[] { "Score", "Time" };             // �X�R�A�Ǝ��Ԃ̃w�b�_�[���쐬
        dataStrs = new List<String>();                      // csv�t�@�C���̒��g��������
        if (!Directory.Exists(dirPath))
        {                     // Assets/Scripts/CSV���Ȃ���΃f�B���N�g���̍쐬
            Directory.CreateDirectory(dirPath);
        }
        sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));
    }
    void Update()
    {

    }

    public void OnClickOutputButton()
    {
        sw.WriteLine(string.Join(", ", header));            // �w�b�_�[�̏�������
        foreach (string dataStr in dataStrs)                 // csv�t�@�C���̒��g����������
        {
            sw.WriteLine(dataStr);
        }
        sw.Close();
        Debug.Log(dirPath + fileName + ".csv���쐬���܂���"); // csv���쐬�������Ƃ��R���\�[���ɕ\��      
    }

    /// <summary>
    ///  UpdateData(string dataStr)
    ///  dataStr��dataStrs�ɒǉ�����֐�
    ///  PlayerController�ŌĂяo�����Ƃ�z��
    /// </summary>
    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }
}
