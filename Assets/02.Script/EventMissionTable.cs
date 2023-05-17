using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;
using UniRx;

[System.Serializable]
public class EventMissionServerData
{
    public int idx;
    public ReactiveProperty<int> clearCount;
    public ReactiveProperty<int> rewardCount;
    public ReactiveProperty<int> adrewardCount;

    public string ConvertToString()
    {
        return $"{idx},{clearCount.Value},{rewardCount.Value},{adrewardCount.Value}";
    }
}

public class EventMissionTable
{
    public static string Indate;
    public const string tableName = "EventMission";

    private ReactiveDictionary<string, EventMissionServerData> tableDatas = new ReactiveDictionary<string, EventMissionServerData>();
    public ReactiveDictionary<string, EventMissionServerData> TableDatas => tableDatas;

    public bool CheckMissionAttendance()
    {
        return tableDatas["Mission6"].clearCount.Value==0;
    }
    public int CheckMissionClearCount(string key)
    {
        return tableDatas[key].clearCount.Value;
    } 
    public int CheckMissionRewardCount(string key)
    {
        return tableDatas[key].rewardCount.Value;
    } 
    public void UpdateMissionClearCount(string key, int amount)
    {
        tableDatas[key].clearCount.Value += amount;
    } 
    public void UpdateMissionClearToCount(string key, int amount)
    {
        tableDatas[key].clearCount.Value = amount;
    } 
    public void UpdateMissionRewardCount(string key, int amount)
    {
        tableDatas[key].rewardCount.Value += amount;
    }
    public void UpdateMissionAdRewardCount(string key, int amount)
    {
        tableDatas[key].adrewardCount.Value += amount;
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

            //맨처음 초기화
            if (rows.Count <= 0)
            {
                Param defultValues = new Param();

                //이벤트미션
                EventMissionData[] table = TableManager.Instance.EventMission.dataArray;
                for (int i = 0; i < table.Length; i++)
                {
                    var missionData = new EventMissionServerData();
                    missionData.idx = table[i].Id;
                    missionData.clearCount = new ReactiveProperty<int>(0);
                    missionData.rewardCount = new ReactiveProperty<int>(0);
                    missionData.adrewardCount = new ReactiveProperty<int>(0);
                   

                    tableDatas.Add(table[i].Stringid, missionData);
                    defultValues.Add(table[i].Stringid, missionData.ConvertToString());
                }
                //짝수월간 미션
                MonthMissionData[] table2 = TableManager.Instance.MonthMission.dataArray;
                for (int i = 0; i < table2.Length; i++)
                {
                    var missionData = new EventMissionServerData();
                    missionData.idx = table2[i].Id;
                    missionData.clearCount = new ReactiveProperty<int>(0);
                    missionData.rewardCount = new ReactiveProperty<int>(0);
                    missionData.adrewardCount = new ReactiveProperty<int>(0);


                    tableDatas.Add(table2[i].Stringid, missionData);
                    defultValues.Add(table2[i].Stringid, missionData.ConvertToString());
                }
                //홀수월간미션
                MonthMission2Data[] table3 = TableManager.Instance.MonthMission2.dataArray;

                for (int i = 0; i < table3.Length; i++)
                {
                    var missionData = new EventMissionServerData();
                    missionData.idx = table3[i].Id;
                    missionData.clearCount = new ReactiveProperty<int>(0);
                    missionData.rewardCount = new ReactiveProperty<int>(0);
                    missionData.adrewardCount = new ReactiveProperty<int>(0);

                   

                    tableDatas.Add(table3[i].Stringid, missionData);
                    defultValues.Add(table3[i].Stringid, missionData.ConvertToString());
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
                //이벤트미션
                var table = TableManager.Instance.EventMission.dataArray;

                for (int i = 0; i < table.Length; i++)
                {
                    if (data.Keys.Contains(table[i].Stringid))
                    {
                        //값로드
                        var value = data[table[i].Stringid][ServerData.format_string].ToString();


                        var splitData = value.Split(',');

                        var missionData = new EventMissionServerData();

                        missionData.idx = int.Parse(splitData[0]);
                        missionData.clearCount = new ReactiveProperty<int>(int.Parse(splitData[1]));
                        missionData.rewardCount = new ReactiveProperty<int>(int.Parse(splitData[2]));

                        if (splitData.Length < 4)
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(0);
                            paramCount++;
                            defultValues.Add(table[i].Stringid, missionData.ConvertToString());
                        }
                        else
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(int.Parse(splitData[3]));
                        }



                        tableDatas.Add(table[i].Stringid, missionData);
                    }
                    else
                    {
                        var missionData = new EventMissionServerData();
                        missionData.idx = table[i].Id;
                        missionData.clearCount = new ReactiveProperty<int>(0);
                        missionData.rewardCount = new ReactiveProperty<int>(0);
                        missionData.adrewardCount = new ReactiveProperty<int>(0);
                        

                        defultValues.Add(table[i].Stringid, missionData.ConvertToString());
                        tableDatas.Add(table[i].Stringid, missionData);
                        paramCount++;
                    }
                }
                //짝수월간미션
                var table2 = TableManager.Instance.MonthMission.dataArray;

                for (int i = 0; i < table2.Length; i++)
                {
                    if (data.Keys.Contains(table2[i].Stringid))
                    {
                        //값로드
                        var value = data[table2[i].Stringid][ServerData.format_string].ToString();


                        var splitData = value.Split(',');

                        var missionData = new EventMissionServerData();

                        missionData.idx = int.Parse(splitData[0]);
                        missionData.clearCount = new ReactiveProperty<int>(int.Parse(splitData[1]));
                        missionData.rewardCount = new ReactiveProperty<int>(int.Parse(splitData[2]));

                        if (splitData.Length < 4)
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(0);
                            paramCount++;
                            defultValues.Add(table2[i].Stringid, missionData.ConvertToString());
                        }
                        else
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(int.Parse(splitData[3]));
                        }



                        tableDatas.Add(table2[i].Stringid, missionData);
                    }
                    else
                    {
                        var missionData = new EventMissionServerData();
                        missionData.idx = table2[i].Id;
                        missionData.clearCount = new ReactiveProperty<int>(0);
                        missionData.rewardCount = new ReactiveProperty<int>(0);
                        missionData.adrewardCount = new ReactiveProperty<int>(0);
                        

                        defultValues.Add(table2[i].Stringid, missionData.ConvertToString());
                        tableDatas.Add(table2[i].Stringid, missionData);
                        paramCount++;
                    }
                }
                //홀수월간미션
                var table3 = TableManager.Instance.MonthMission2.dataArray;

                for (int i = 0; i < table3.Length; i++)
                {
                    if (data.Keys.Contains(table3[i].Stringid))
                    {
                        //값로드
                        var value = data[table3[i].Stringid][ServerData.format_string].ToString();


                        var splitData = value.Split(',');

                        var missionData = new EventMissionServerData();

                        missionData.idx = int.Parse(splitData[0]);
                        missionData.clearCount = new ReactiveProperty<int>(int.Parse(splitData[1]));
                        missionData.rewardCount = new ReactiveProperty<int>(int.Parse(splitData[2]));

                        if (splitData.Length < 4)
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(0);
                            paramCount++;
                            defultValues.Add(table3[i].Stringid, missionData.ConvertToString());
                        }
                        else
                        {
                            missionData.adrewardCount = new ReactiveProperty<int>(int.Parse(splitData[3]));
                        }



                        tableDatas.Add(table3[i].Stringid, missionData);
                    }
                    else
                    {
                        var missionData = new EventMissionServerData();
                        missionData.idx = table3[i].Id;
                        missionData.clearCount = new ReactiveProperty<int>(0);
                        missionData.rewardCount = new ReactiveProperty<int>(0);
                        missionData.adrewardCount = new ReactiveProperty<int>(0);
                        

                        defultValues.Add(table3[i].Stringid, missionData.ConvertToString());
                        tableDatas.Add(table3[i].Stringid, missionData);
                        paramCount++;
                    }
                }

                if (paramCount != 0)
                {
                    var bro = Backend.GameData.Update(tableName, Indate, defultValues);

                    if (bro.IsSuccess() == false)
                    {
                        ServerData.ShowCommonErrorPopup(bro, Initialize);
                        return;
                    }
                }

            }
        });
    }
  
    public void SyncToServerEach(string key)
    {
        Param param = new Param();
        param.Add(key, tableDatas[key].ConvertToString());

        SendQueue.Enqueue(Backend.GameData.Update, tableName, Indate, param, e =>
        {
            if (e.IsSuccess() == false)
            {
                Debug.Log($"이벤트 일일미션 {key} up failed");
                return;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError($"이벤트 일일미션 Key {key} sync complete");
            }
#endif
        });
    }
}
