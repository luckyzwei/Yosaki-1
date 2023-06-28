using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UniRx;
using TMPro;

//지옥탈환
public class UiBossRankBoard : MonoBehaviour
{
    [SerializeField]
    private UiRankView uiRankViewPrefab;

    [SerializeField]
    private Transform rankViewParent;

    [SerializeField]
    private UiRankView myRankView;

    List<UiRankView> rankViewContainer = new List<UiRankView>();

    [SerializeField]
    private GameObject loadingMask;

    [SerializeField]
    private GameObject failObject;

    [SerializeField]
    private TextMeshProUGUI title;


    private void OnEnable()
    {
        UiTopRankerView.Instance.DisableAllCell();
        SetTitle();
        LoadRankInfo();
    }

    private void SetTitle()
    {
        title.SetText($"랭킹({CommonString.RankPrefix_Boss})");
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        RankManager.Instance.WhenMyBossRankLoadComplete.AsObservable().Subscribe(e =>
        {
            if (e != null)
            {
                myRankView.Initialize($"{e.Rank}", e.NickName, $"{Utils.ConvertBigNum(e.Score)}", e.Rank, e.costumeIdx, e.petIddx, e.weaponIdx, e.magicbookIdx, e.gumgiIdx, e.GuildName,e.maskIdx,e.hornIdx,e.suhoAnimal);
            }
            else
            {
                myRankView.Initialize("나", "미등록", "미등록", 0, -1, -1, -1, -1, -1,string.Empty,-1,-1,-1);
            }


        }).AddTo(this);
    }
    private void LoadRankInfo()
    {
        rankViewParent.gameObject.SetActive(false);
        loadingMask.SetActive(false);
        failObject.SetActive(false);
        RankManager.Instance.GetRankerList(RankManager.Rank_Boss_Uuid, 100, WhenAllRankerLoadComplete);
        RankManager.Instance.RequestMyBossRank();
    }

    private void WhenAllRankerLoadComplete(BackendReturnObject bro)
    {
        if (bro.IsSuccess())
        {
            JObject json_data = JObject.Parse(bro.GetReturnValue());
            //var rows = bro.Rows();

            if (json_data["rows"].Count() > 0)
            {
                rankViewParent.gameObject.SetActive(true);

                int interval = json_data["rows"].Count() - rankViewContainer.Count;

                for (int i = 0; i < interval; i++)
                {
                    var view = Instantiate<UiRankView>(uiRankViewPrefab, rankViewParent);
                    rankViewContainer.Add(view);
                }

                for (int i = 0; i < rankViewContainer.Count; i++)
                {
                    if (i < json_data["rows"].Count())
                    {
                        var splitData = json_data["rows"][i]["NickName"][ServerData.format_string].ToString().Split(CommonString.ChatSplitChar);

                        rankViewContainer[i].gameObject.SetActive(true);
                        string nickName = json_data["rows"][i]["nickname"][ServerData.format_string].ToString();
                        int rank = int.Parse(json_data["rows"][i]["rank"][ServerData.format_Number].ToString());

                        var test = json_data["rows"][i]["score"][ServerData.format_Number].ToString();

                        double score = double.Parse(json_data["rows"][i]["score"][ServerData.format_Number].ToString());
                        score *= GameBalance.BossScoreConvertToOrigin;
                        int costumeId = int.Parse(splitData[0]);
                        int petId = int.Parse(splitData[1]);
                        int weaponId = int.Parse(splitData[2]);
                        int magicBookId = int.Parse(splitData[3]);
                        int gumgiIdx = int.Parse(splitData[4]);
                        int maskIdx = int.Parse(splitData[6]);
                        int hornIdx = -1;
                        int suhoAnimal = -1;

                        if (splitData.Length >= 9)
                        {
                            hornIdx = int.Parse(splitData[8]);
                        }   
                        
                        if (splitData.Length >= 10)
                        {
                            suhoAnimal = int.Parse(splitData[9]);
                        }

                        Color color1 = Color.white;
                        Color color2 = Color.white;

                        
                        //1등
                        if (i == 0)
                        {
                            color1 = Color.yellow;
                        }
                        //2등
                        else if (i == 1)
                        {
                            color1 = Color.yellow;
                        }
                        //3등
                        else if (i == 2)
                        {
                            color1 = Color.yellow;
                        }

#if UNITY_IOS
                    nickName = nickName.Replace(CommonString.IOS_nick, "");
#endif

                        string guildName = string.Empty;
                        if (splitData.Length >= 8)
                        {
                            guildName = splitData[7];
                        }

                        //myRankView.Initialize($"{e.Rank}", e.NickName, $"Lv {e.Score}");
                        rankViewContainer[i].Initialize($"{rank}", $"{nickName}", $"{Utils.ConvertBigNum(score)}", rank, costumeId, petId, weaponId, magicBookId, gumgiIdx,guildName, maskIdx,hornIdx,suhoAnimal);
                    }
                    else
                    {
                        rankViewContainer[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //데이터 없을때
            }

        }
        else
        {
            failObject.SetActive(true);
        }
    }
}
