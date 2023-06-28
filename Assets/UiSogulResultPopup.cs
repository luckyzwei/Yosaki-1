using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiSogulResultPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lastClearStageDesc;

    [SerializeField]
    private GameObject deadGameObject;

    [SerializeField]
    private GameObject clearObject;

    public void Initialize(int lastClearStage, bool clearLastStage, bool isPlayerDead, string defix = "단계 돌파!!")
    {
        this.gameObject.SetActive(true);

        if (GameManager.contentsType == GameManager.ContentsType.VisionTower)
        {
            lastClearStageDesc.SetText($"{lastClearStage}등급 돌파!!");
        }
        else
        {
            lastClearStageDesc.SetText($"{lastClearStage}{defix}");
        }

        deadGameObject.SetActive(isPlayerDead);

        clearObject.SetActive(clearLastStage);
    }
}
