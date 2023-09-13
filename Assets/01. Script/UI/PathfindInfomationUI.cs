using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathfindInfomationUI : MonoSingleton<PathfindInfomationUI>
{
    [SerializeField] private TextMeshProUGUI infomationText;

    private float time;
    private float count;

    public void SetTime(float time)
    {
        this.time = time; 
        SetUI();
    }
    public void SetCount(int count)
    {
        this.count = count;
        SetUI();
    }
    public void SetUI()
    {
        float time = this.time * 1000;
        infomationText.text = $"��ã�⿡ �ɸ� ���� : {time, 3:f}ms \nŽ���� �ȼ� ���� : {count}";
    }
}
