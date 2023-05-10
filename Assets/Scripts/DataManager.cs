using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    private StreamWriter sw;
    private string dirPath;                                 // 作成するcsvのディレクトリ
    private string fileName;                                // 作成するcsvのファイル名
    private string[] header;                                // 作成するcsvファイルのヘッダー
    private List<string> dataStrs;                          // 作成するcsvファイルの中身


    void Start()
    {
        dirPath = Application.dataPath + "/Scripts/CSV/";   // Assets/Scripts/CSVを指定
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss"); // シーンの開始時間をファイル名に指定
        header = new string[] { "Score", "Time" };             // スコアと時間のヘッダーを作成
        dataStrs = new List<String>();                      // csvファイルの中身を初期化
        if (!Directory.Exists(dirPath))
        {                     // Assets/Scripts/CSVがなければディレクトリの作成
            Directory.CreateDirectory(dirPath);
        }
        sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));
    }
    void Update()
    {

    }

    public void OnClickOutputButton()
    {
        sw.WriteLine(string.Join(", ", header));            // ヘッダーの書き込み
        foreach (string dataStr in dataStrs)                 // csvファイルの中身を書き込み
        {
            sw.WriteLine(dataStr);
        }
        sw.Close();
        Debug.Log(dirPath + fileName + ".csvを作成しました"); // csvを作成したことをコンソールに表示      
    }

    /// <summary>
    ///  UpdateData(string dataStr)
    ///  dataStrをdataStrsに追加する関数
    ///  PlayerControllerで呼び出すことを想定
    /// </summary>
    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }
}
