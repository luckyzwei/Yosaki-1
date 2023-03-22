using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GumSoulTransAddValueIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.SetText($"검기 능력치 {GameBalance.GumSoulGraduatePlusValue}배 증가!");
    }
}
