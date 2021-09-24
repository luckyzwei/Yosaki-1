﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
public class UiMonthPassBuyButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descText;

    private CompositeDisposable disposable = new CompositeDisposable();

    public static readonly string monthPassKey = "monthpass1";

    void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        disposable.Dispose();
    }

    private void Subscribe()
    {
        disposable.Clear();

        ServerData.iapServerTable.TableDatas[monthPassKey].buyCount.AsObservable().Subscribe(e =>
        {
            descText.SetText(e >= 1 ? "구매완료" : "훈련권 구매");
            this.gameObject.SetActive(e <= 0);
        }).AddTo(disposable);

        IAPManager.Instance.WhenBuyComplete.AsObservable().Subscribe(e =>
        {
            SoundManager.Instance.PlaySound("GoldUse");
            GetPackageItem(e.purchasedProduct.definition.id);
        }).AddTo(disposable);
    }

    public void OnClickBuyButton()
    {
        if (ServerData.iapServerTable.TableDatas[monthPassKey].buyCount.Value >= 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 구매 했습니다.");
            return;
        }

#if UNITY_EDITOR|| TEST
        GetPackageItem(monthPassKey);
        return;
#endif

        IAPManager.Instance.BuyProduct(monthPassKey);
    }

    public void GetPackageItem(string productId)
    {
        if (TableManager.Instance.InAppPurchaseData.TryGetValue(productId, out var tableData) == false)
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 상품 id {productId}", null);
            return;
        }
        else
        {
            // PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{tableData.Title} 구매 성공!", null);
        }

        if (tableData.Productid != monthPassKey) return;

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"구매 성공!", null);

        ServerData.iapServerTable.TableDatas[tableData.Productid].buyCount.Value++;

        ServerData.iapServerTable.UpData(tableData.Productid);
    }
}
