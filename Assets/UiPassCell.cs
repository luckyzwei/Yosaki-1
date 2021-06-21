﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using BackEnd;
using TMPro;

public class UiPassCell : MonoBehaviour
{

    [SerializeField]
    private Image itemIcon_free;

    [SerializeField]
    private Image itemIcon_ad;

    [SerializeField]
    private TextMeshProUGUI itemAmount_free;

    [SerializeField]
    private TextMeshProUGUI itemAmount_ad;

    [SerializeField]
    private GameObject lockIcon_Free;

    [SerializeField]
    private GameObject lockIcon_Ad;

    private PassInfo passInfo;

    [SerializeField]
    private GameObject rewardedObject_Free;

    [SerializeField]
    private GameObject rewardedObject_Ad;

    [SerializeField]
    private GameObject gaugeImage;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    private CompositeDisposable disposables = new CompositeDisposable();

    private void OnDestroy()
    {
        disposables.Dispose();
    }
    private void Subscribe()
    {
        disposables.Clear();

        //무료보상 데이터 갱신
        DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_Free_Key].Subscribe(e =>
        {
            bool rewarded = HasReward(passInfo.rewardType_Free_Key, passInfo.id);
            rewardedObject_Free.SetActive(rewarded);

        }).AddTo(disposables);

        //패스보상 데이터 변경시
        DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_IAP_Key].Subscribe(e =>
        {
            bool rewarded = HasReward(passInfo.rewardType_IAP_Key, passInfo.id);
            rewardedObject_Ad.SetActive(rewarded);

        }).AddTo(disposables);

        //레벨 변할때
        DatabaseManager.statusTable.GetTableData(StatusTable.Level).AsObservable().Subscribe(e =>
        {
            lockIcon_Free.SetActive(!CanGetReward());
            lockIcon_Ad.SetActive(!CanGetReward());
            gaugeImage.SetActive(CanGetReward());
        }).AddTo(disposables);
    }

    public void Initialize(PassInfo passInfo)
    {
        this.passInfo = passInfo;

        SetAmount();

        SetItemIcon();

        SetDescriptionText();

        Subscribe();
    }

    private void SetAmount()
    {
        itemAmount_free.SetText(Utils.ConvertBigNum(passInfo.rewardTypeValue_Free));
        itemAmount_ad.SetText(Utils.ConvertBigNum(passInfo.rewardTypeValue_IAP));
    }

    private void SetItemIcon()
    {
        itemIcon_free.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)(int)passInfo.rewardType_Free);
        itemIcon_ad.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)(int)passInfo.rewardType_IAP);
    }

    private void SetDescriptionText()
    {
        descriptionText.SetText($"Lv {passInfo.require}");
    }

    public List<string> GetSplitData(string key)
    {
        return DatabaseManager.passServerTable.TableDatas[key].Value.Split(',').ToList();
    }

    public bool HasReward(string key, int data)
    {
        var splitData = GetSplitData(key);
        return splitData.Contains(data.ToString());
    }

    public void OnClickFreeRewardButton()
    {
        if (CanGetReward() == false)
        {
            PopupManager.Instance.ShowAlarmMessage("레벨이 부족합니다.");
            return;
        }

        if (HasReward(passInfo.rewardType_Free_Key, passInfo.id))
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 받았습니다!");
            return;
        }

        PopupManager.Instance.ShowAlarmMessage("보상을 수령했습니다!");

        GetFreeReward();

    }

    //광고아님
    public void OnClickAdRewardButton()
    {
        if (HasPassProduct() == false)
        {
            PopupManager.Instance.ShowAlarmMessage("패스 상품을 구매해야 받을 수 있습니다.");
            return;
        }

        if (CanGetReward() == false)
        {
            PopupManager.Instance.ShowAlarmMessage("레벨이 부족합니다.");
            return;
        }

        if (HasReward(passInfo.rewardType_IAP_Key, passInfo.id))
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 받았습니다!");
            return;
        }

        PopupManager.Instance.ShowAlarmMessage("보상을 수령했습니다!");

        GetAdReward();
    }

    private bool HasPassProduct()
    {
        return IAPManager.Instance.HasProduct(passInfo.shopId);
    }

    private void GetFreeReward()
    {
        //로컬
        DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_Free_Key].Value += $",{passInfo.id}";
        DatabaseManager.AddLocalValue((Item_Type)(int)passInfo.rewardType_Free, passInfo.rewardTypeValue_Free);

        List<TransactionValue> transactionList = new List<TransactionValue>();

        //패스 보상
        Param passParam = new Param();
        passParam.Add(passInfo.rewardType_Free_Key, DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_Free_Key].Value);
        transactionList.Add(TransactionValue.SetUpdate(PassServerTable.tableName, PassServerTable.Indate, passParam));

        var rewardTransactionValue = DatabaseManager.GetItemTypeTransactionValue((Item_Type)(int)passInfo.rewardType_Free);
        transactionList.Add(rewardTransactionValue);

        DatabaseManager.SendTransaction(transactionList);
    }
    private void GetAdReward()
    {
        //로컬
        DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_IAP_Key].Value += $",{passInfo.id}";
        DatabaseManager.AddLocalValue((Item_Type)(int)passInfo.rewardType_IAP, passInfo.rewardTypeValue_IAP);

        List<TransactionValue> transactionList = new List<TransactionValue>();

        //패스 보상
        Param passParam = new Param();
        passParam.Add(passInfo.rewardType_IAP_Key, DatabaseManager.passServerTable.TableDatas[passInfo.rewardType_IAP_Key].Value);
        transactionList.Add(TransactionValue.SetUpdate(PassServerTable.tableName, PassServerTable.Indate, passParam));

        var rewardTransactionValue = DatabaseManager.GetItemTypeTransactionValue((Item_Type)(int)passInfo.rewardType_IAP);
        transactionList.Add(rewardTransactionValue);

        DatabaseManager.SendTransaction(transactionList);
    }

    private bool CanGetReward()
    {
        int currentLevel = DatabaseManager.statusTable.GetTableData(StatusTable.Level).Value;
        return currentLevel >= passInfo.require;
    }
}
