﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
public class UiLEvelPassBuyButton4 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descText;

    private CompositeDisposable disposable = new CompositeDisposable();

    public string stagePassKey = "levelpass4";

    private Button buyButton;

    void Start()
    {
       // Subscribe();
    }

    private void OnDestroy()
    {
        disposable.Dispose();
    }

    public void SetNewString(string _string)
    {
        stagePassKey = _string;
        Subscribe();
    }
    
    private void Subscribe()
    {
        buyButton = GetComponent<Button>();

        disposable.Clear();

        ServerData.iapServerTable.TableDatas[stagePassKey].buyCount.AsObservable().Subscribe(e =>
        {
            //descText.SetText(e >= 1 ? "구매완료" : "패스구매");
            this.gameObject.SetActive(e <= 0);
        }).AddTo(disposable);

        IAPManager.Instance.WhenBuyComplete.AsObservable().Subscribe(e =>
        {
            SoundManager.Instance.PlaySound("Reward");
            GetPackageItem(e.purchasedProduct.definition.id);
        }).AddTo(disposable);

        IAPManager.Instance.disableBuyButton.AsObservable().Subscribe(e =>
        {
            buyButton.interactable = false;
        }).AddTo(disposable);

        IAPManager.Instance.activeBuyButton.AsObservable().Subscribe(e =>
        {
            buyButton.interactable = true;
        }).AddTo(disposable);
    }

    public void OnClickBuyButton()
    {
        if (ServerData.iapServerTable.TableDatas[stagePassKey].buyCount.Value >= 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 구매 했습니다.");
            return;
        }

#if UNITY_EDITOR|| TEST
        GetPackageItem(stagePassKey);
        return;
#endif

        IAPManager.Instance.BuyProduct(stagePassKey);
    }

    public void GetPackageItem(string productId)
    {
        if (TableManager.Instance.InAppPurchaseData.TryGetValue(productId, out var tableData) == false)
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 상품 id {productId}", null);
            return;
        }

        if (tableData.Productid != stagePassKey) return;

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"구매 성공!", null);
        
        Debug.LogError($"{stagePassKey} 구매");
        
        ServerData.iapServerTable.TableDatas[tableData.Productid].buyCount.Value++;

        ServerData.iapServerTable.UpData(tableData.Productid);
    }
}
