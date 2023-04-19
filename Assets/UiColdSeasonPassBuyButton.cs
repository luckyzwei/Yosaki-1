using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UiColdSeasonPassBuyButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descText;

    private CompositeDisposable disposable = new CompositeDisposable();

    public static readonly string seasonPassKey = "seasonpass2";
    
    private Button buyButton;

    [SerializeField]
    private TextMeshProUGUI fireDescription;

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
        buyButton = GetComponent<Button>();

        disposable.Clear();

        ServerData.iapServerTable.TableDatas[seasonPassKey].buyCount.AsObservable().Subscribe(e =>
        {
            descText.SetText(e >= 1 ? "구매완료" : "패스권 구매");
            this.gameObject.SetActive(e <= 0);
        }).AddTo(disposable);

        IAPManager.Instance.WhenBuyComplete.AsObservable().Subscribe(e =>
        {
            SoundManager.Instance.PlaySound("GoldUse");
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

        ServerData.goodsTable.TableDatas[GoodsTable.Event_HotTime_Saved].AsObservable().Subscribe(e =>
        {
            if (fireDescription != null)
            {
                fireDescription.SetText($"불꽃조각 {e}개 획득");
            }
            
        }).AddTo(disposable);
    }

    public void OnClickBuyButton()
    {
        if (ServerData.iapServerTable.TableDatas[seasonPassKey].buyCount.Value >= 1)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 구매 했습니다.");
            return;
        }

#if UNITY_EDITOR|| TEST
        GetPackageItem(seasonPassKey);
        return;
#endif

        IAPManager.Instance.BuyProduct(seasonPassKey);
    }

    public void GetPackageItem(string productId)
    {
        if (productId.Equals("removeadios"))
        {
            productId = "removead";
        }

        if (TableManager.Instance.InAppPurchaseData.TryGetValue(productId, out var tableData) == false)
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 상품 id {productId}", null);
            return;
        }
        else
        {
            // PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{tableData.Title} 구매 성공!", null);
        }

        if (tableData.Productid != seasonPassKey) return;

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"구매 성공!\n{CommonString.GetItemName(Item_Type.Event_HotTime)} {ServerData.goodsTable.TableDatas[GoodsTable.Event_HotTime_Saved].Value}개 획득!", null);

        ServerData.iapServerTable.TableDatas[tableData.Productid].buyCount.Value++;

        ServerData.iapServerTable.UpData(tableData.Productid);

        ServerData.goodsTable.TableDatas[GoodsTable.Event_HotTime].Value += ServerData.goodsTable.TableDatas[GoodsTable.Event_HotTime_Saved].Value;
        
        ServerData.goodsTable.UpData(GoodsTable.Event_HotTime,false);
    }
}
