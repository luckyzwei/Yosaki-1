﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using Spine.Unity;

public class UiYoungMulCraftBoard : SingletonMono<UiYoungMulCraftBoard>
{
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private WeaponView sinSuNorigaeView;

    private MagicBookData sinsuData;

    [SerializeField]
    private TextMeshProUGUI currentLevel;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    public void Initialize(int norigaeId)
    {
        sinsuData = TableManager.Instance.MagicBookTable.dataArray[norigaeId];

        var sinsuServerData = ServerData.magicBookTable.TableDatas[sinsuData.Stringid];

        if (sinsuServerData.hasItem.Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보유하고 있습니다.");
            return;
        }

        descriptionText.SetText($"환수 장비 {GameBalance.YoungMulCreateEquipLevel}이상 필요");

        rootObject.SetActive(true);

        sinSuNorigaeView.Initialize(null, sinsuData);

        if (Subscribed == false)
        {

            Subscribed = true;

            Subscribe();
        }
    }

    private bool Subscribed = false;
    private void Subscribe()
    {
        ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).AsObservable().Subscribe(level =>
        {
            currentLevel.SetText($"현재 강화도 : {level}");
        }).AddTo(this);
    }

    public void OnClickCraftButton()
    {
        if (ServerData.magicBookTable.TableDatas[sinsuData.Stringid].hasItem.Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보유중");
            return;
        }

        //조건
        //환수장비 몇강 이상
        if (ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value < GameBalance.YoungMulCreateEquipLevel)
        {
            PopupManager.Instance.ShowAlarmMessage($"환수 장비 강화가 부족합니다.\n{GameBalance.YoungMulCreateEquipLevel}레벨 이상 필요");
            return;
        }

        //조건

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.magicBookTable.TableDatas[sinsuData.Stringid].amount.Value += 1;
        ServerData.magicBookTable.TableDatas[sinsuData.Stringid].hasItem.Value = 1;

        Param magicBookParam = new Param();

        magicBookParam.Add(sinsuData.Stringid, ServerData.magicBookTable.TableDatas[sinsuData.Stringid].ConvertToString());

        transactions.Add(TransactionValue.SetUpdate(MagicBookTable.tableName, MagicBookTable.Indate, magicBookParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            SoundManager.Instance.PlaySound("Reward");
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "영물 제작 완료!", null);
         //   LogManager.Instance.SendLogType("YoungMul", "C", "C");
        });
    }
}
