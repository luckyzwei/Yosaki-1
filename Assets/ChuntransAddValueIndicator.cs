using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ChuntransAddValueIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.SetText($"천계 꽃 보유 효과 {PlayerStats.ChunTransAddValue}배로 증가!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
