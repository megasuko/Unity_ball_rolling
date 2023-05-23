using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI missText;      // 
    [SerializeField] public TextMeshProUGUI comboText;     // 
    [SerializeField] public TextMeshProUGUI TimeText;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI rankText;
    [SerializeField] public TextMeshProUGUI itemText;
    private int penalty;
    private int timeBonus;
    private int comboBonus;
    private int totalScore;
    private int itemScore;

    // Start is called before the first frame update
    void Start()
    {
        penalty = PlayerController.hit * -50;
        timeBonus = (30 - (int)PlayerController.playTime) * 10;
        totalScore = PlayerController.score + timeBonus;
        itemScore = PlayerController.item * 100;
        comboBonus = PlayerController.score - itemScore - penalty;
        missText.text = "Penalty : " + penalty.ToString();
        comboText.text = "Combo Bonus : " + comboBonus.ToString();
        TimeText.text = "Time Bonus : " + timeBonus.ToString();
        scoreText.text = "Total Score : " + totalScore.ToString();
        itemText.text = "item : " + itemScore.ToString();
        switch (totalScore / 100)
        {
            case 8:
                rankText.text = "SSS";
                break;
            case 7:
                rankText.text = "SS";
                break;
            case 6:
                rankText.text = "S";
                break;
            case 5:
                rankText.text = "A";
                break;
            case 4:
                rankText.text = "B";
                break;
            case 3:
                rankText.text = "C";
                break;
            case 2:
                rankText.text = "D";
                break;
            case 1:
                rankText.text = "E";
                break;
            default:
                rankText.text = "F";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
