using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TMPro;
using UniRx;
using UnityEngine;

public class UiGumgiMountainSoul : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI abilValue;

    [SerializeField]
    private WeaponView weaponView;


    [SerializeField] private GameObject transBeforeObject;
    [SerializeField] private GameObject transAfterObject;

    void Start()
    {
        abilDescription.SetText($"처치 {PlayerStats.gumgiSoulDivideNum}마리당 검기의 능력치가 {PlayerStats.gumgiSoulAbilValue * 100f}% 강화 됩니다!");
        SubScribe();
    }

    private void SubScribe()
    {
        //?? 단게 검기 능력치 +??% 강화됨
        ServerData.equipmentTable.TableDatas[EquipmentTable.Weapon_View].AsObservable().Subscribe(e =>
        {
            if (e == -1)
            {
                e = 0;
            }

            weaponView.Initialize(TableManager.Instance.WeaponData[e], null);
        }).AddTo(this);

        ServerData.goodsTable.GetTableData(GoodsTable.Fw).AsObservable().Subscribe(e =>
        {
            UpdateGumSoulAddValue();
        }).AddTo(this);

        ServerData.userInfoTable.GetTableData(UserInfoTable.graduateGumSoul).AsObservable().Subscribe(e =>
        {
            transBeforeObject.SetActive(e < 1);
            transAfterObject.SetActive(e >= 1);
            UpdateGumSoulAddValue();
        }).AddTo(this);
    }

    private void UpdateGumSoulAddValue()
    {
        var grade = (int)(ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiSoulClear].Value / PlayerStats.gumgiSoulDivideNum);

        abilValue.SetText($"{grade}단계 검기 능력치 +{PlayerStats.GetGumgiAbilAddValue() * 100f}% 강화됨");
    }

    public void OnClickTransButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiSoulClear].Value < GameBalance.GumSoulGraduateScore)
        {
            PopupManager.Instance.ShowAlarmMessage($"최고 점수 {GameBalance.GumSoulGraduateScore} 이상일때 각성 가능!");
        }
        else
        {
            PopupManager.Instance.ShowYesNoPopup(CommonString.Notice,
                $"검의영혼 각성시 최고점수가 {GameBalance.GumSoulFixedScore}로 고정 됩니다. \n" +
                $"그리고 검의영혼 효과가 {GameBalance.GumSoulGraduatePlusValue}배 강화 됩니다.\n" +
                "각성 하시겠습니까??", () =>
                {
                    ServerData.userInfoTable.TableDatas[UserInfoTable.graduateGumSoul].Value = 1;
                    ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiSoulClear].Value = GameBalance.GumSoulFixedScore;
                    
                    List<TransactionValue> transactions = new List<TransactionValue>();
                    
                    Param userInfoParam = new Param();
                    userInfoParam.Add(UserInfoTable.graduateGumSoul, ServerData.userInfoTable.TableDatas[UserInfoTable.graduateGumSoul].Value);
                    userInfoParam.Add(UserInfoTable.gumGiSoulClear, ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiSoulClear].Value);

                    transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userInfoParam));
                    
                    ServerData.SendTransaction(transactions,successCallBack: () =>
                    {
                        
                    });
                    
                    PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "각성 완료!!", null);
              
                }, null);
        }
    }
}