﻿using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UiRewardView;
public class UiTwelveBossContentsView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private GameObject lockObject;

    [SerializeField]
    private Image bossIcon;

    private TwelveBossTableData bossTableData;

    [SerializeField]
    private Button enterButton;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private bool initByInspector = false;

    [SerializeField]
    private int bossId = 0;

    private void Start()
    {
        InitByInspector();
    }

    private void InitByInspector()
    {
        if (initByInspector)
        {
            Initialize(TableManager.Instance.TwelveBossTable.dataArray[bossId]);
        }
    }

    public void Initialize(TwelveBossTableData bossTableData)
    {
        this.bossTableData = bossTableData;

        if (title != null)
        {
            title.SetText(bossTableData.Name);
        }

        var score = ServerData.bossServerTable.TableDatas[bossTableData.Stringid].score.Value;
        if (string.IsNullOrEmpty(score) == false)
        {
            description.SetText($"최고 피해량 : {Utils.ConvertBigNum(double.Parse(score))}");
        }
        else
        {
            description.SetText("기록 없음");
        }

        lockObject.SetActive(bossTableData.Islock);
        buttons.SetActive(bossTableData.Islock == false);

        if (bossTableData.Id>=124&&bossTableData.Id<=131)
        {
            bossIcon.sprite = CommonResourceContainer.GetDarkIconSprite(bossTableData.Id - 124);
        }
        else if (bossTableData.Id < CommonUiContainer.Instance.bossIcon.Count)
        {
            bossIcon.sprite = CommonUiContainer.Instance.bossIcon[bossTableData.Id];
        }
    }

    public void OnClickRewardButton()
    {
        UiTwelveRewardPopup.Instance.Initialize(bossTableData.Id);

        UiContentsEnterPopup.Instance.transform.SetAsLastSibling();
    }

    public void OnClickEnterButton()
    {
        PopupManager.Instance.ShowYesNoPopup("알림", "도전 할까요?", () =>
        {
            enterButton.interactable = false;
            GameManager.Instance.SetBossId(bossTableData.Id);
            GameManager.Instance.LoadContents(GameManager.ContentsType.TwelveDungeon);
        }, () => { });
    }
}
