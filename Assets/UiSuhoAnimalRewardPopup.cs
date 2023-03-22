using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiTwelveRewardPopup;
using System.Linq;
using UniRx;
using UnityEngine.UI;


public class UiSuhoAnimalRewardPopup : SingletonMono<UiSuhoAnimalRewardPopup>
{
    [SerializeField]
    private GameObject rootObject;

    private SuhopetTableData bossTableData;
    private SuhoSuhoPetServerData suhoSuhoPetServerData;

    [SerializeField]
    private UiSuhoAnimalRewardView uiSuhoRewardViews;

    private List<UiSuhoAnimalRewardView> uiSuhoRewardViewss = new List<UiSuhoAnimalRewardView>();

    [SerializeField]
    private Transform cellParent;

    [SerializeField]
    private TextMeshProUGUI damText;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI getButton;

    [SerializeField]
    private Image getButton_Image;

    [SerializeField]
    private Sprite orangeButton;

    [SerializeField]
    private Sprite purpleButton;

    [SerializeField]
    private UiAnimalView petIcon;

    private bool RewardAllReceived()
    {
        int rewardedItemCount = suhoSuhoPetServerData.rewardedItem.Value.Split(BossServerTable.rewardSplit).Length;

        return rewardedItemCount - 1 == bossTableData.Rewardcut.Length;
    }


    public void OnClickGetPetButton()
    {
        if (RewardAllReceived() == false)
        {
            PopupManager.Instance.ShowAlarmMessage("모든 보상을 수령해야 합니다!");
            return;
        }

        if (suhoSuhoPetServerData.hasItem.Value != 0)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보유하고 있습니다!");
            return;
        }

        suhoSuhoPetServerData.hasItem.Value = 1;
        ServerData.suhoAnimalServerTable.UpdateData(bossTableData.Stringid);

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "수호동물 획득!", null);
    }

    public void OnClickStartButton()
    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 하시겠습니까?", () =>
        {
            GameManager.Instance.SetSuhoAnimalBossId(bossTableData.Id);
            GameManager.Instance.LoadContents(GameManager.ContentsType.SuhoAnimal);
        }, () => { });
    }

    public void Initialize(int bossId)
    {
        //플레이 안했을때 예외처리
        if (bossId == -1) bossId = 0;

        bossTableData = TableManager.Instance.suhoPetTable.dataArray[bossId];

        petIcon.Initialize(bossTableData); 
        
        suhoSuhoPetServerData = ServerData.suhoAnimalServerTable.TableDatas[bossTableData.Stringid];

        nameText.SetText(bossTableData.Name);

        double currentDamage = 0f;

        if (string.IsNullOrEmpty(suhoSuhoPetServerData.score.Value) == false)
        {
            currentDamage = double.Parse(suhoSuhoPetServerData.score.Value);
        }

        if (damText != null)
        {
            damText.SetText($"최고 피해량 : {Utils.ConvertBigNum(currentDamage)}");
        }


        if (rootObject != null)
        {
            rootObject.SetActive(true);
        }

        bossTableData = TableManager.Instance.suhoPetTable.dataArray[bossId];

        int makeCellAmount = bossTableData.Rewardcut.Length - uiSuhoRewardViewss.Count;

        for (int i = 0; i < makeCellAmount; i++)
        {
            var cell = Instantiate<UiSuhoAnimalRewardView>(uiSuhoRewardViews, cellParent);

            uiSuhoRewardViewss.Add(cell);
        }

        for (int i = 0; i < uiSuhoRewardViewss.Count; i++)
        {
            if (i < bossTableData.Rewardcut.Length)
            {
                uiSuhoRewardViewss[i].gameObject.SetActive(true);

                TwelveBossRewardInfo info = new TwelveBossRewardInfo(i, bossTableData.Rewardcut[i],
                    bossTableData.Rewardtype[i], bossTableData.Rewardvalue[i], bossTableData.Cutstring[i],
                    currentDamage);

                uiSuhoRewardViewss[i].Initialize(info, suhoSuhoPetServerData);
            }
            else
            {
                uiSuhoRewardViewss[i].gameObject.SetActive(false);
            }
        }

        Subscribe();
    }

    private CompositeDisposable disposable = new CompositeDisposable();

    private void Subscribe()
    {
        disposable.Clear();

        suhoSuhoPetServerData.hasItem.Subscribe(e =>
        {
            getButton.SetText(e != 0 ? "보유중" : "획득");
            getButton_Image.sprite = e != 0 ? purpleButton : orangeButton;
        }).AddTo(disposable);
    }

    private void OnDestroy()
    {
        base.OnDestroy();
        disposable.Dispose();
    }
}