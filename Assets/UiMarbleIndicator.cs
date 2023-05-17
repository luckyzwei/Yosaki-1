﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
using BackEnd;
using JetBrains.Annotations;

public class UiMarbleIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private TextMeshProUGUI marbleName;

    [SerializeField]
    private TextMeshProUGUI marbleDesc;

    private MarbleTableData currentTableData;

    [SerializeField]
    private Transform selecIcon;

    [SerializeField]
    private List<Transform> marbleCircles;

    private CompositeDisposable disposable = new CompositeDisposable();

    [SerializeField]
    private Button unlockButton;

    [SerializeField]
    private Animator marbleAwakeAnimator;

    [SerializeField] private SeletableTab _selectableTab;
    private void OnDestroy()
    {
        disposable.Dispose();
    }

    private void Start()
    {
        OnClickMarbleView(0);
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.foxTowerStart).Value > 0)
        {
            _selectableTab.OnSelect(1);
        }
    }

    public void OnClickMarbleView(int id)
    {
        currentTableData = TableManager.Instance.MarbleTable.dataArray[id];
        RefreshMarbleUi();
        Subscribe();
    }

    private void Subscribe()
    {
        disposable.Clear();

        ServerData.userInfoTable.GetTableData(UserInfoTable.marbleAwake).AsObservable().Subscribe(e =>
        {
            marbleAwakeAnimator.enabled = e == 1;
        }).AddTo(disposable);

        ServerData.marbleServerTable.TableDatas[currentTableData.Stringid].hasItem.AsObservable().Subscribe(e =>
        {
            unlockButton.gameObject.SetActive(e == 0);
        }).AddTo(disposable);


    }

    private void RefreshMarbleUi()
    {
        selecIcon.transform.position = marbleCircles[currentTableData.Id].transform.position;

        marbleName.SetText(currentTableData.Name);

        if (currentTableData.Islock == true)
        {
            priceText.SetText("미공개");
        }
        else
        {
            priceText.SetText(Utils.ConvertBigNum(currentTableData.Unlockprice));
        }

        marbleName.color = CommonUiContainer.Instance.itemGradeColor[currentTableData.Grade];

        bool marbleAwaked = ServerData.userInfoTable.GetTableData(UserInfoTable.marbleAwake).Value == 1f;

        string abilDesc = null;

        if (currentTableData.Islock)
        {
            marbleDesc.SetText("미공개");
        }
        else
        {
            abilDesc += "-각성전-\n";

            //능력치 한개짜리
            if (currentTableData.Abilitytype.Length == 1)
            {
                StatusType statusType = (StatusType)currentTableData.Abilitytype[0];

                float statusValue = currentTableData.Abilityvalue[0];

                if (statusType.IsPercentStat())
                {
                    abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue * 100f}";
                }
                else
                {
                    abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue}";
                }
            }
            else
            {
                for (int i = 0; i < currentTableData.Abilitytype.Length; i++)
                {
                    StatusType statusType = (StatusType)currentTableData.Abilitytype[i];

                    float statusValue = currentTableData.Abilityvalue[i];

                    if (statusType.IsPercentStat())
                    {
                        abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue * 100f}";
                    }
                    else
                    {
                        abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue}";
                    }

                    if (i != currentTableData.Abilitytype.Length - 1)
                    {
                        abilDesc += "\n";
                    }
                }
            }
            abilDesc += "\n<color=red>-각성후-\n";
            //각성시
            if (currentTableData.Abilitytype.Length == 1)
            {
                StatusType statusType = (StatusType)currentTableData.Abilitytype[0];

                float statusValue = currentTableData.Awakevalue[0];

                if (statusType.IsPercentStat())
                {
                    abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue * 100f}";
                }
                else
                {
                    abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue}";
                }
            }
            else
            {
                for (int i = 0; i < currentTableData.Abilitytype.Length; i++)
                {
                    StatusType statusType = (StatusType)currentTableData.Abilitytype[i];

                    float statusValue = currentTableData.Awakevalue[i];

                    if (statusType.IsPercentStat())
                    {
                        abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue * 100f}";
                    }
                    else
                    {
                        abilDesc += $"{CommonString.GetStatusName(statusType)} {statusValue}";
                    }

                    if (i != currentTableData.Abilitytype.Length - 1)
                    {
                        abilDesc += "\n";
                    }
                }
            }


            if (string.IsNullOrEmpty(currentTableData.Description) == false)
            {
                abilDesc += $"\n{currentTableData.Description}";
            }

            abilDesc += "</color>";

            marbleDesc.SetText(abilDesc);
        }
    }

    public void OnClickAwakeButton()
    {


        //전부 열려있는지 체크
        if (ServerData.marbleServerTable.AllMarblesUnlocked() == false)
        {
            PopupManager.Instance.ShowAlarmMessage($"모든 구슬이 해방되어야 합니다.");
            return;
        }

        bool isMarbleAwaked = ServerData.userInfoTable.TableDatas[UserInfoTable.marbleAwake].Value == 1;

        if (isMarbleAwaked == true)
        {
            PopupManager.Instance.ShowAlarmMessage($"이미 각성 했습니다.");
            return;
        }



        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.Marble)} {Utils.ConvertBigNum(GameBalance.marbleAwakePrice)}개를 사용해서 각성 합니까?\n\n각성효과\n향상된 능력치 적용\n구미호 호연 외형 획득\n순보 딜레이 감소\n드랍 아이템 즉시 획득",
            () =>
            {
                if (ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value < GameBalance.marbleAwakePrice)
                {
                    PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.Marble)}이 부족합니다.", null);
                    return;
                }
                else
                {
                    List<TransactionValue> transactions = new List<TransactionValue>();

                    ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value -= GameBalance.marbleAwakePrice;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.marbleAwake).Value = 1;

                    Param goodsParam = new Param();
                    goodsParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);

                    Param userinfoParam = new Param();
                    userinfoParam.Add(UserInfoTable.marbleAwake, ServerData.userInfoTable.GetTableData(UserInfoTable.marbleAwake).Value);

                    Param costumeParam = new Param();
                    string key = Item_Type.costume5.ToString();
                    var serverData = ServerData.costumeServerTable.TableDatas[key];
                    serverData.hasCostume.Value = true;
                    costumeParam.Add(key, serverData.ConvertToString());

                    transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
                    transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userinfoParam));
                    transactions.Add(TransactionValue.SetUpdate(CostumeServerTable.tableName, CostumeServerTable.Indate, costumeParam));

                    ServerData.SendTransaction(transactions, successCallBack: () =>
                    {
                        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"구슬이 각성됐습니다.\n향상된 능력치로 적용 됩니다.\n구미호 호연 외형 획득!\n순보 딜레이 감소", null);
                       // LogManager.Instance.SendLog("구슬 각성", "각성 완료");

                        //
                        //ui갱신
                        OnClickMarbleView(0);
                        //
                    });
                }
            },
            () =>
            {

            });
    }

    public void OnClickUpgradeButton()
    {
        if (currentTableData.Islock == true)
        {
            PopupManager.Instance.ShowAlarmMessage("미공개 구슬 입니다.");
            return;
        }

        var serverData = ServerData.marbleServerTable.TableDatas[currentTableData.Stringid];

        if (serverData.hasItem.Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 봉인 해제 됐습니다.");
            return;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value < currentTableData.Unlockprice)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.Marble)}이 부족합니다.");
            return;
        }

        SoundManager.Instance.PlaySound("Reward");

        List<TransactionValue> transactionList = new List<TransactionValue>();

        string marbledesc = string.Empty;

        marbledesc += $"pref {ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value}";

        ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value -= currentTableData.Unlockprice;
        ServerData.marbleServerTable.TableDatas[currentTableData.Stringid].hasItem.Value = 1;

        marbledesc += $"after {ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value}";

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
        transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        Param marbleParam = new Param();
        marbleParam.Add(currentTableData.Stringid, ServerData.marbleServerTable.TableDatas[currentTableData.Stringid].ConvertToString());
        transactionList.Add(TransactionValue.SetUpdate(MarbleServerTable.tableName, MarbleServerTable.Indate, marbleParam));

        ServerData.SendTransaction(transactionList, successCallBack: () =>
        {
           //LogManager.Instance.SendLog("구슬 개방", $"{currentTableData.Name} {marbledesc}");
            PopupManager.Instance.ShowAlarmMessage($"{currentTableData.Name} 획득!!");
        });
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += 10000000;
        }
    }

#endif
}
