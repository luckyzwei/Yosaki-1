using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using BackEnd;

public class UiMileageRefund : MonoBehaviour
{
    [SerializeField]
    private UiText uiText;

    [SerializeField]
    private Transform parents;

    [SerializeField]
    private GameObject rootObject;


    void Start()
    {
        RefundRoutine();
        
        DolPassRefundRoutine();
        NewGachaRefundRoutine();
        TitleRefundRoutine();
        ChunmaDokebiFireRefundRoutine();
        TitleRefundRoutine2();
        RelocateLevelPass();
        TowerFloorAdjust();
    }

    private void RefundRoutine()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.mileageRefund).Value != 0)
        {
            rootObject.SetActive(false);
            return;
        }

        float mileageTotalNum = 0;

        string description = string.Empty;

        var serverTable = ServerData.iAPServerTableTotal.TableDatas;

        var localTableData = TableManager.Instance.InAppPurchase.dataArray;

        for (int i = 0; i < localTableData.Length; i++)
        {
            for (int j = 0; j < localTableData[i].Rewardtypes.Length; j++)
            {
                if (localTableData[i].Rewardtypes[j] == 9000)
                {
                    int buyCount = serverTable[localTableData[i].Productid].buyCount.Value;

                    if (buyCount == 0) continue;

                    var mileageNum = localTableData[i].Rewardvalues[j] * buyCount;

                    mileageTotalNum += mileageNum;

                    var textObject = Instantiate<UiText>(uiText, parents);

                    textObject.Initialize($"{localTableData[i].Title} {buyCount}회 구매 마일리지 {mileageNum}개");
                }
            }
        }

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value += mileageTotalNum;
        ServerData.userInfoTable.GetTableData(UserInfoTable.mileageRefund).Value = 1;

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.Mileage, ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value);

        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.mileageRefund, ServerData.userInfoTable.GetTableData(UserInfoTable.mileageRefund).Value);

        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        if (mileageTotalNum == 0)
        {
            ServerData.userInfoTable.GetTableData(UserInfoTable.mileageRefund).Value = 1;
            ServerData.userInfoTable.UpData(UserInfoTable.mileageRefund, false);

            rootObject.SetActive(false);
        }
        else
        {
            rootObject.SetActive(true);

            var totalText = Instantiate<UiText>(uiText, parents);

            totalText.Initialize($"마일리지 총 {mileageTotalNum}개 소급됨");

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "마일리지 소급 완료!", null);
            });
        }
    }
    private void DolPassRefundRoutine()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.dolPassRefund).Value != 0)
        {
            return;
        }

        if (ServerData.iapServerTable.TableDatas["dolpass"].buyCount.Value < 1)
        {
            //돌패스 구매 x
            ServerData.userInfoTable.GetTableData(UserInfoTable.dolPassRefund).Value = 1;
            ServerData.userInfoTable.UpData(UserInfoTable.dolPassRefund, false);
            return;
        }
        

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value += GameBalance.DolPassDiceRefundValue;
        ServerData.userInfoTable.GetTableData(UserInfoTable.dolPassRefund).Value = 1;

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.EventDice, ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value);

        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.dolPassRefund, ServerData.userInfoTable.GetTableData(UserInfoTable.dolPassRefund).Value);

        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"주사위 총 {GameBalance.DolPassDiceRefundValue}개 소급됨\n" +
                                                                        $"주사위 소급 완료!", null);
        });
        
    }
    private void NewGachaRefundRoutine()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.newGachaEnergyRefund).Value != 0)
        {
            return;
        }

        if (ServerData.userInfoTable.GetTableData(UserInfoTable.relicKillCount).Value>25000)
        {
            //돌패스 구매 x
            ServerData.userInfoTable.GetTableData(UserInfoTable.newGachaEnergyRefund).Value = 1;
            ServerData.userInfoTable.UpData(UserInfoTable.newGachaEnergyRefund, false);
            return;
        }
        

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value -=
            (float)ServerData.userInfoTable.GetTableData(UserInfoTable.relicKillCount).Value;
        if (ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value < 0)
        {
            ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value = 0;
        }
        ServerData.userInfoTable.GetTableData(UserInfoTable.newGachaEnergyRefund).Value = 1;

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.NewGachaEnergy, ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value);

        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.newGachaEnergyRefund, ServerData.userInfoTable.GetTableData(UserInfoTable.newGachaEnergyRefund).Value);

        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
        });
        
    }

    private void TitleRefundRoutine()
    {
        
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle).Value != 0)
        {
            return;
        }
        ////////타이틀 재 장착/////////////////
        ServerData.equipmentTable.ChangeEquip(EquipmentTable.TitleSelectId, -1);
        PlayerStats.ResetAbilDic();
        ////////////////////////////////////////
        ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle).Value = 1;
        var tableDatas = TableManager.Instance.TitleTable.dataArray;
        int levelTitleIdx = 0;
        int stageTitleIdx = 0;
        for (int i = 0; i < tableDatas.Length; i++)
        {
            //레벨
            if (tableDatas[i].Displaygroup == 0)
            {
                if (ServerData.titleServerTable.TableDatas[tableDatas[i].Stringid].rewarded.Value > 0)
                {
                    levelTitleIdx = i;
                }
            }
            //스테이지
            else if (tableDatas[i].Displaygroup == 1)
            {
                if (ServerData.titleServerTable.TableDatas[tableDatas[i].Stringid].rewarded.Value > 0)
                {
                    stageTitleIdx = i;
                }
            }
        }
        List<TransactionValue> transactions = new List<TransactionValue>();

        
    
        Param userInfoParam = new Param();
        if (levelTitleIdx == 0)
        {
            //받은게없음
        }
        else
        {
            var currentTitleLevel = TableManager.Instance.TitleTable.dataArray[levelTitleIdx].Condition;
            var levelTableData = TableManager.Instance.titleLevel.dataArray;
            for (int i = 0; i < levelTableData.Length; i++)
            {
                if (levelTableData[i].Condition == currentTitleLevel)
                {
                    ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value = i;
                    break;
                }
            }
            
            userInfoParam.Add(UserInfoTable.titleLevel, ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value);
        }
        if (stageTitleIdx == 0)
        {
            //받은게 없음
        }
        else
        {
            var currentTitleStage = TableManager.Instance.TitleTable.dataArray[stageTitleIdx].Condition;
            var stageTableData = TableManager.Instance.titleStage.dataArray;
            for (int i = 0; i < stageTableData.Length; i++)
            {
                if (stageTableData[i].Condition == currentTitleStage)
                {
                    ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value = i;
                    break;
                }
            }
            userInfoParam.Add(UserInfoTable.titleStage, ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value);
        }
        



        userInfoParam.Add(UserInfoTable.titleConvertNewTitle, ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            Debug.LogError(
                $"Lv : {ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value} / Stage : {ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value}");
        });
    }    
    private void TitleRefundRoutine2()
    {
        
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle2).Value != 0)
        {
            return;
        }
        ////////타이틀 재 장착/////////////////
        ServerData.equipmentTable.ChangeEquip(EquipmentTable.TitleSelectId, -1);
        PlayerStats.ResetAbilDic();
        ////////////////////////////////////////
        ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle2).Value = 1;
        var tableDatas = TableManager.Instance.TitleTable.dataArray;
        int weaponTitleIdx = 0;
        for (int i = 0; i < tableDatas.Length; i++)
        {
            //무기
            if (tableDatas[i].Displaygroup == 2)
            {
                if (ServerData.titleServerTable.TableDatas[tableDatas[i].Stringid].rewarded.Value > 0)
                {
                    weaponTitleIdx = i;
                }
            }
        }
        List<TransactionValue> transactions = new List<TransactionValue>();

        
    
        Param userInfoParam = new Param();
        if (weaponTitleIdx == 0)
        {
            //받은게없음
        }
        else if (weaponTitleIdx < 402)
        {
            //필멸무기(암) 미만임
        }
        else
        {
            var weaponId = TableManager.Instance.TitleTable.dataArray[weaponTitleIdx].Id;
            var levelTableData = TableManager.Instance.titleWeapon.dataArray;
            for (int i = 0; i < levelTableData.Length; i++)
            {
                if (levelTableData[i].Titeid == weaponId)
                {
                    ServerData.userInfoTable.GetTableData(UserInfoTable.titleWeapon).Value = i;
                    break;
                }
            }
            
            userInfoParam.Add(UserInfoTable.titleWeapon, ServerData.userInfoTable.GetTableData(UserInfoTable.titleWeapon).Value);
        }

        userInfoParam.Add(UserInfoTable.titleConvertNewTitle2, ServerData.userInfoTable.GetTableData(UserInfoTable.titleConvertNewTitle2).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            Debug.LogError(
                $"Weapon : {ServerData.userInfoTable.GetTableData(UserInfoTable.titleWeapon).Value} ");
        });
    }
    
    private void TowerFloorAdjust()
    {
        
        if (ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.towerFloorAdjust).Value != 0)
        {
            return;
        }
        ////////////////////////////////////////
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.towerFloorAdjust).Value = 1;
        
        List<TransactionValue> transactions = new List<TransactionValue>();
        Param userInfoParam = new Param();

        var floor1 = ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value;

        if (floor1 > 2)
        {
            ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value = Mathf.Max(0, (int)((floor1-1) / 15));
            userInfoParam.Add(UserInfoTable.currentFloorIdx, ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value);
        }
        var floor2 = ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx2).Value;
        if (floor2>2)
        {
            ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx2).Value = Mathf.Max(0, (int)((floor2-1) / 3));
            userInfoParam.Add(UserInfoTable.currentFloorIdx2, ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx2).Value);
        }

        if (floor1 > 0 || floor2 > 0)
        {
            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
        }
        

        Param userInfo_2Param = new Param();
        
        userInfo_2Param.Add(UserInfoTable_2.towerFloorAdjust, ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.towerFloorAdjust).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, userInfo_2Param));

        
        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            Debug.LogError($"요괴도장 : {ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value}\n" +
                           $"상급 요괴도장 : {ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx2).Value}");
        });
    }
    
    private int GetMaxValueFromStringList(List<string> strList)
    {
        //문자열 비었으면
        if (strList == null || strList.Count == 0)
        {
            return -1;
        }
        List<int> intList = new List<int>();
    
        foreach (string str in strList)
        {
            int num;
            if (int.TryParse(str, out num))
            {
                intList.Add(num);
            }
        }
        //변환한게 비었으면
        if (intList.Count == 0)
        {
            return -1;
        }
        return intList.Max();
    }
    private void RelocateLevelPass()
    {
        
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.relocateLevelPass).Value != 0)
        {
            return;
        }
        ServerData.userInfoTable.GetTableData(UserInfoTable.relocateLevelPass).Value = 1;

        // StagePass
        var stageFreeRewardStr = ServerData.passServerTable.TableDatas[PassServerTable.stagePassReward].Value;
        var stageFreeRewardArr = stageFreeRewardStr.Split(',');
        var stageFreeMax = GetMaxValueFromStringList(stageFreeRewardArr.ToList());

        var stageAdRewardStr = ServerData.passServerTable.TableDatas[PassServerTable.stagePassAdReward].Value;
        var stageAdRewardArr = stageAdRewardStr.Split(',');
        var stageAdMax = GetMaxValueFromStringList(stageAdRewardArr.ToList());

        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassFree).Value = stageFreeMax;
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassAd).Value = stageAdMax;
        
        List<TransactionValue> transactions = new List<TransactionValue>();
        
        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.relocateLevelPass, ServerData.userInfoTable.GetTableData(UserInfoTable.relocateLevelPass).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
        
        Param userInfo2Param = new Param();
        userInfo2Param.Add(UserInfoTable_2.stagePassFree, ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassFree).Value);
        userInfo2Param.Add(UserInfoTable_2.stagePassAd, ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassAd).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, userInfo2Param));
        
        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            Debug.LogError($" stageFree : {ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassFree).Value}" +
                           $"stageAd : {ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.stagePassAd).Value}");
        });
    }
    private void ChunmaDokebiFireRefundRoutine()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.chunmaRefund).Value != 0)
        {
            return;
        }
        ServerData.userInfoTable.GetTableData(UserInfoTable.chunmaRefund).Value = 1;
        var list = ServerData.bossServerTable.GetChunmaRewardedIdxList();
        var tableData= TableManager.Instance.TwelveBossTable.dataArray[55];
        var amount = 0;
        for (int i = 130; i < 133; i++)
        {
            if (list.Contains(i))
            {
                amount += (int)tableData.Rewardvalue[i] - 30;
            }    
        }
        
        List<TransactionValue> transactions = new List<TransactionValue>();
        
        if (amount == 0)
        {        
            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.chunmaRefund, ServerData.userInfoTable.GetTableData(UserInfoTable.chunmaRefund).Value);
            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                Debug.LogError("소급 없음");
            });
        }
        else
        {
            ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value += amount;
        
            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.chunmaRefund, ServerData.userInfoTable.GetTableData(UserInfoTable.chunmaRefund).Value);
            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.DokebiFire, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value);
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup("십만대산 도깨비불 소급", $"도깨비불 {amount}개 소급 완료", null);
            });
        }

    }
}
