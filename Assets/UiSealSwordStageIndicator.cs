using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiSealSwordStageIndicator : MonoBehaviour
{
    [SerializeField]
    private WeaponView weaponViewPrefab;

    [SerializeField]
    private Transform cellParent;

    private List<WeaponView> viewContainer = new List<WeaponView>();

    [SerializeField]
    private TextMeshProUGUI currentGradeDescription;

    [SerializeField]
    private TextMeshProUGUI probDescription;
    
    [SerializeField]
    private TextMeshProUGUI probDescription2_my;
     
    [SerializeField]
    private TextMeshProUGUI probDescription2;
    
    private int currentIdx = 0;

    private void Start()
    {
        int myStage = (int)(ServerData.userInfoTable.TableDatas[UserInfoTable.currentFloorIdx9].Value-1);
        
        if (myStage <= 0)
        {
            myStage = 0;
        }
        
        var tableData = TableManager.Instance.SealTowerTable.dataArray;
        var currentData = tableData[myStage];
        
        probDescription2_my.SetText($"현재 내 단계 : {myStage+1}\n{CommonString.GetItemName(Item_Type.SealWeaponClear)} 1개당 무기 {currentData.Gachacount}개 획득");
        
    }

    public void Initialize(int currentIdx)
    {
        this.currentIdx = currentIdx;

        UpdateUi();
    }


    public void UpdateUi()
    {
        currentGradeDescription.SetText($"{currentIdx + 1}단계 소탕 정보");

        var tableData = TableManager.Instance.SealTowerTable.dataArray;
        var currentData = tableData[currentIdx];

        int maxGrade = currentData.Spawnweaponmaxgrade;
        int slotNum = (maxGrade + 1) * 4;

        Debug.LogError(slotNum);

        while (viewContainer.Count < slotNum)
        {
            WeaponView weaponView = Instantiate<WeaponView>(weaponViewPrefab, cellParent);
            viewContainer.Add(weaponView);
        }

        var weaponTable = TableManager.Instance.sealSwordTable.dataArray;


        for (int i = 0; i < viewContainer.Count; i++)
        {
            if (i < slotNum)
            {
                viewContainer[i].gameObject.SetActive(true);
                viewContainer[i].Initialize(null, null, null, null, weaponTable[i]);
            }
            else
            {
                viewContainer[i].gameObject.SetActive(false);
            }
        }

        string prob0 = $"<color=#a52a2aff>하급 : {(Math.Round(currentData.Gachalv1,5) * 100f)}% ";
        string prob1 = $"<color=green>중급 : {(Math.Round(currentData.Gachalv2,5) * 100f)}% ";
        string prob2 = $"<color=blue>상급 :  {(Math.Round(currentData.Gachalv3,5) * 100f)}% ";
        string prob3 = $"<color=purple>특급 :  {(Math.Round(currentData.Gachalv4,5) * 100f)}%";
        string prob4 = $"<color=red>전설 :  {(Math.Round(currentData.Gachalv5,5) * 100f)}%";

        string result = string.Empty;

        if (currentData.Gachalv1 != 0f)
        {
            result += prob0;
        }
        
        if (currentData.Gachalv2 != 0f)
        {
            result += prob1;
        }
        
        if (currentData.Gachalv3 != 0f)
        {
            result += prob2;
        }
        
        if (currentData.Gachalv4 != 0f)
        {
            result += prob3;
        }
        
        if (currentData.Gachalv5 != 0f)
        {
            result += prob4;
        }
        
        probDescription.SetText(result);
        
        probDescription2.SetText($"{CommonString.GetItemName(Item_Type.SealWeaponClear)} 1개당 무기 {currentData.Gachacount}개 획득");
    }

    public void OnClickRightButton()
    {
        var tableData = TableManager.Instance.SealTowerTable.dataArray;

        currentIdx++;

        if (currentIdx == tableData.Length)
        {
            PopupManager.Instance.ShowAlarmMessage("최고단계 입니다.");
        }

        currentIdx = Mathf.Min(currentIdx, tableData.Length - 1);

        UpdateUi();
    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        if (currentIdx <= 0)
        {
            PopupManager.Instance.ShowAlarmMessage("처음단계 입니다.");
        }

        currentIdx = Mathf.Max(currentIdx, 0);

        UpdateUi();
    }
}