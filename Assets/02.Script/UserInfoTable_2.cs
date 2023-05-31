using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;
using UniRx;
using CodeStage.AntiCheat.ObscuredTypes;

public class UserInfoTable_2
{
    public static string Indate;
    public const string tableName = "UserInfo2";
    
    public const string monkeyGodScore = "monkeyGodScore";
    public const string swordGodScore = "swordGodScore";
    public const string hellGodScore = "hellGodScore";
    public const string chunGodScore = "chunGodScore";
    public const string GangChulReset = "GangChulReset";
    public const string stagePassFree = "stagePassFree";
    public const string stagePassAd = "stagePassAd";
    public const string foxFirePassKill = "pass0";
    

    //짝수 월간훈련(Monthlypass)
    public const string evenMonthKillCount = "even0";
    //홓수 월간훈련(Monthlypass2)
    public const string oddMonthKillCount = "odd0";
    
    public const string SealSwordAwakeScore = "SSASS";
    public const string taeguekTower = "tgtw";
    public const string taeguekLock = "tgll";
    public const string SansinTowerIdx = "sst";
    public const string KingTrialGraduateIdx = "ktgi";
    public const string darkScore = "ds";
    public const string graduateGold = "gg";
    public const string gyungRockTower3 = "grt3";


    public bool isInitialize = false;
    private Dictionary<string, double> tableSchema = new Dictionary<string, double>()
    {
        { GangChulReset, 0f },
        { stagePassFree, -1f },
        { stagePassAd, -1f },
        
        { monkeyGodScore, 0f },
        { swordGodScore, 0f },
        { hellGodScore, 0f },
        { chunGodScore, 0f },
        { foxFirePassKill, 0f },
        { evenMonthKillCount, 0f },
        { oddMonthKillCount, 0f },
        { SealSwordAwakeScore, 0f },
        { taeguekTower, 0f },
        { taeguekLock, 0f },
        { SansinTowerIdx, 0f },
        { KingTrialGraduateIdx, 0f },
        { darkScore, 0f },
        { graduateGold, 0f },
        { gyungRockTower3, 0f },
    };

    private Dictionary<string, ReactiveProperty<double>> tableDatas = new Dictionary<string, ReactiveProperty<double>>();
    public Dictionary<string, ReactiveProperty<double>> TableDatas => tableDatas;

    public ReactiveProperty<double> GetTableData(string key)
    {
        return tableDatas[key];
    }

    public void Initialize()
    {
        tableDatas.Clear();

        SendQueue.Enqueue(Backend.GameData.GetMyData, tableName, new Where(), callback =>
        {
            // 이후 처리
            if (callback.IsSuccess() == false)
            {
                Debug.LogError("LoadStatusFailed");
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, CommonString.DataLoadFailedRetry, Initialize);
                return;
            }

            var rows = callback.Rows();

            //맨처음 초기화(캐릭터생성)
            if (rows.Count <= 0)
            {
                Param defultValues = new Param();

                var e = tableSchema.GetEnumerator();

                while (e.MoveNext())
                {
                    defultValues.Add(e.Current.Key, e.Current.Value);
                    tableDatas.Add(e.Current.Key, new ReactiveProperty<double>(e.Current.Value));
                }

                var bro = Backend.GameData.Insert(tableName, defultValues);

                if (bro.IsSuccess() == false)
                {
                    // 이후 처리
                    ServerData.ShowCommonErrorPopup(bro, Initialize);
                    return;
                }
                else
                {
                    var jsonData = bro.GetReturnValuetoJSON();
                    if (jsonData.Keys.Count > 0)
                    {
                        Indate = jsonData[0].ToString();
                    }

                    // data.
                    // statusIndate = data[DatabaseManager.inDate_str][DatabaseManager.format_string].ToString();
                }

                return;
            }
            //나중에 칼럼 추가됐을때 업데이트
            else
            {
                Param defultValues = new Param();
                int paramCount = 0;

                JsonData data = rows[0];

                if (data.Keys.Contains(ServerData.inDate_str))
                {
                    Indate = data[ServerData.inDate_str][ServerData.format_string].ToString();
                }

                var e = tableSchema.GetEnumerator();

                for (int i = 0; i < data.Keys.Count; i++)
                {
                    while (e.MoveNext())
                    {
                        if (data.Keys.Contains(e.Current.Key))
                        {
                            //값로드
                            var value = data[e.Current.Key][ServerData.format_Number].ToString();
                            tableDatas.Add(e.Current.Key, new ReactiveProperty<double>(double.Parse(value)));
                        }
                        else
                        {
                            defultValues.Add(e.Current.Key, e.Current.Value);
                            tableDatas.Add(e.Current.Key, new ReactiveProperty<double>(e.Current.Value));

                            paramCount++;
                        }
                    }
                }

                if (paramCount != 0)
                {
                    var bro = Backend.GameData.Update(tableName, Indate, defultValues);

                    if (bro.IsSuccess() == false)
                    {
                        ServerData.ShowCommonErrorPopup(bro, Initialize);
                        return; //
                    }
                }
            }

            isInitialize = true;
        });
    }
    public void AutoUpdateRoutine()
    {
        if (isInitialize)
        {
            UpdatekillCount();
        }
    }

    private void UpdatekillCount()
    {
        List<TransactionValue> transactions = new List<TransactionValue>();

        Param userInfo_2Param = new Param();
        if (ServerData.userInfoTable.IsMonthlyPass2() == false)
        {
            userInfo_2Param.Add(evenMonthKillCount, tableDatas[evenMonthKillCount].Value);
        }
        else
        {
            userInfo_2Param.Add(oddMonthKillCount, tableDatas[oddMonthKillCount].Value);
        }
        userInfo_2Param.Add(foxFirePassKill, tableDatas[foxFirePassKill].Value);

        

        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, userInfo_2Param));

        ServerData.SendTransaction(transactions);
    }
    static int totalKillCount = 0;
    static double updateRequireNum = 100;
    public void GetKillCountTotal()
    {
        totalKillCount += (int)GameManager.Instance.CurrentStageData.Marbleamount;

        if (totalKillCount < updateRequireNum)
        {
        }
        else
        {
            if (ServerData.userInfoTable.IsMonthlyPass2() == false)
            {
                tableDatas[evenMonthKillCount].Value += updateRequireNum;
            }
            else
            {
                tableDatas[oddMonthKillCount].Value += updateRequireNum;
            }
            totalKillCount = 0;

            tableDatas[foxFirePassKill].Value += updateRequireNum;
        }
    }
    public void UpData(string key, bool LocalOnly)
    {
        if (tableDatas.ContainsKey(key) == false)
        {
            Debug.Log($"UserInfoTable {key} is not exist");
            return;
        }

        UpData(key, tableDatas[key].Value, LocalOnly);
    }

    public void UpData(string key, double data, bool LocalOnly, Action failCallBack = null)
    {
        if (tableDatas.ContainsKey(key) == false)
        {
            Debug.Log($"UserInfoTable {key} is not exist");
            return;
        }

        tableDatas[key].Value = data;

        if (LocalOnly == false)
        {
            Param param = new Param();
            param.Add(key, tableDatas[key].Value);

            SendQueue.Enqueue(Backend.GameData.Update, tableName, Indate, param, e =>
            {
                if (e.IsSuccess() == false)
                {
                    failCallBack?.Invoke();
                    Debug.LogError($"UserInfoTable {key} up failed");
                    return;
                }
            });
        }
    }
}