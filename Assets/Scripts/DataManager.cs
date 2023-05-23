using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR
    private StreamWriter sw;
#elif UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void FileDownLoad(string str, string fileName);
#endif

    private string dirPath;                                 // �쐬����csv�̃f�B���N�g��
    private string fileName;                                // �쐬����csv�̃t�@�C����
    private string[] header;                                // �쐬����csv�t�@�C���̃w�b�_�[
    private List<string> dataStrs;                          // �쐬����csv�t�@�C���̒��g


    void Start()
    {
        dirPath = Application.dataPath + "/Scripts/CSV/";   // Assets/Scripts/CSV���w��
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss"); // �V�[���̊J�n���Ԃ��t�@�C�����Ɏw��
        header = new string[] { "No", "Color", "Time" };             // �X�R�A�Ǝ��Ԃ̃w�b�_�[���쐬
        dataStrs = new List<String>();                      // csv�t�@�C���̒��g��������
        if (!Directory.Exists(dirPath))
        {                     // Assets/Scripts/CSV���Ȃ���΃f�B���N�g���̍쐬
            Directory.CreateDirectory(dirPath);
        }
#if UNITY_EDITOR
        sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));
#endif
    }
    void Update()
    {

    }

    public void OnClickOutputButton()
    {
#if UNITY_EDITOR
        sw.WriteLine(string.Join(", ", header));            // �w�b�_�[�̏�������
        foreach(string dataStr in dataStrs)                 // csv�t�@�C���̒��g����������
        {
            sw.WriteLine(dataStr);
        }
        sw.Close();
#elif UNITY_WEBGL
        FileDownLoad(string.Join(",", header) + "\n" + string.Join("\n", dataStrs), fileName);
#endif
        Debug.Log(dirPath + fileName + ".csv���쐬���܂���"); // csv���쐬�������Ƃ��R���\�[���ɕ\��      
    }

    /*  
    //  UpdateData(string dataStr)
    //  dataStr��dataStrs�ɒǉ�����֐�
    //  PlayerController�ŌĂяo�����Ƃ�z��
    */
    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }

    public string ColorToHex()
    {
        Color randColor = UnityEngine.Random.ColorHSV();
        return ColorUtility.ToHtmlStringRGBA(randColor); ;
    }
}