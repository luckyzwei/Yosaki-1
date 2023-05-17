using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;

public class UiTaeguekLockMask : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private UiTaeguekDescription uiTaeguekDescription;
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekLock].AsObservable().Subscribe(e => { rootObject.SetActive(e == 0); }).AddTo(this);
    }

    public void OnClickUnlockButton()
    {
        if (ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekLock].Value == 1) return;

        //강철이 보스 id
        int bossId = 20;

        var bossTableData = TableManager.Instance.TwelveBossTable.dataArray[bossId];

        var bossServerData = ServerData.bossServerTable.TableDatas[bossTableData.Stringid];

        double currentDamage = 0f;

        if (string.IsNullOrEmpty(bossServerData.score.Value) == false)
        {
            currentDamage = double.Parse(bossServerData.score.Value);
        }

        var taeguekTableData = TableManager.Instance.taegeukTitle.dataArray;

        int setStage = 0;

        for (int i = 0; i < taeguekTableData.Length; i++)
        {
            if (currentDamage >= taeguekTableData[i].Hp)
            {
                setStage = i;
            }
        }

        setStage--;

        if (setStage < 0)
        {
            setStage = 0;
        }

        uiTaeguekDescription.currentIdx = setStage;
        uiTaeguekDescription.Initialize(setStage);

        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekTower].Value = setStage;
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekLock].Value = 1;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param taeguekParam = new Param();
        taeguekParam.Add(UserInfoTable_2.taeguekTower, ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekTower].Value);
        taeguekParam.Add(UserInfoTable_2.taeguekLock, ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekLock].Value);

        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, taeguekParam));

        ServerData.SendTransaction(transactions, successCallBack: () => { PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{taeguekTableData[setStage].Titlename} 시작", null); });
    }
}