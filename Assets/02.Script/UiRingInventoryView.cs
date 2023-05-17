using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;


public class RingData_Fancy
{
    public NewGachaTableData RingData { get; private set; }
    public UiRingInventoryView ParentView { get; private set; }
    public RingData_Fancy(NewGachaTableData ringData,UiRingInventoryView parentView)
    {
        this.RingData = ringData;
        this.ParentView = parentView;
    }

    public void Upgrade()
    {
        if (RingData != null)
        {
            int amount = ServerData.newGachaServerTable.TableDatas[RingData.Stringid].amount.Value;

            if (amount < RingData.Requireupgrade)
            {
                return;
            }

            if (TableManager.Instance.NewGachaData.TryGetValue(RingData.Id + 1, out var nextNewGacha))
            {
                int currentWeaponCount = ServerData.newGachaServerTable.GetCurrentNewGachaCount(RingData.Stringid);
                int nextWeaponCount = ServerData.newGachaServerTable.GetCurrentNewGachaCount(nextNewGacha.Stringid);

                int upgradeNum = currentWeaponCount / RingData.Requireupgrade;

                ServerData.newGachaServerTable.UpData(RingData, upgradeNum * RingData.Requireupgrade * -1);
                ServerData.newGachaServerTable.UpData(nextNewGacha, upgradeNum);

                ServerData.newGachaServerTable.SyncToServerAll(new List<int>() { RingData.Id, nextNewGacha.Id });
            }
            else
            {
                //맥스레벨 처리
                PopupManager.Instance.ShowAlarmMessage("더이상 승급이 불가능 합니다.");
            }
        }
    }
}

public class UiRingInventoryView : FancyScrollView<RingData_Fancy>
{
    [SerializeField] private RingType _ringType;
    
    [SerializeField]
    private Scroller scroller;
    
    private List<RingData_Fancy> ringDataContainer = new List<RingData_Fancy>();
    
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;
    
    private void Start()
    {
            
        switch (_ringType)
        {
            case RingType.Basic:
            case RingType.Normal:
                MakeBasicNormalBoard();
                break;
            case RingType.View:
                break;
            default:
                break;
        }

    }
    public void AllUpgradeRing(int myIdx)
    {
        for (int i = 0; i <= myIdx; i++)
        {
            ringDataContainer[i].Upgrade();
        }
    }
    private void MakeBasicNormalBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.NewGachaTable.dataArray;
    
        List<RingData_Fancy> passInfos = new List<RingData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            var ringData = new NewGachaTableData();

            ringData = tableData[i];
            
            passInfos.Add(new RingData_Fancy(ringData,this));
            ringDataContainer.Add(new RingData_Fancy(ringData,this));
        }
        
        passInfos.Sort((a, b) => tableData[a.RingData.Id].Displayorder.CompareTo(tableData[b.RingData.Id].Displayorder));
    
        for (int i = 0; i < passInfos.Count; i++)
        {
            RingData_Fancy ringData = passInfos[i];
            if (ringData != null && ServerData.newGachaServerTable.TableDatas[ringData.RingData.Stringid].hasItem.Value > 0)
            {
                passInfos.Insert(0, passInfos[i]);
                passInfos.RemoveAt(i + 1);
            }
        }
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
    
    private void MakeViewBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.NewGachaTable.dataArray;
    
        List<RingData_Fancy> passInfos = new List<RingData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            
            var ringData = new NewGachaTableData();

            ringData = tableData[i];
            
            passInfos.Add(new RingData_Fancy(ringData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.RingData.Id].Displayorder.CompareTo(tableData[b.RingData.Id].Displayorder));
    
        for (int i = 0; i < passInfos.Count; i++)
        {
            RingData_Fancy ringData = passInfos[i];
            if (ringData != null && ServerData.newGachaServerTable.TableDatas[ringData.RingData.Stringid].hasItem.Value > 0)
            {
                passInfos.Insert(0, passInfos[i]);
                passInfos.RemoveAt(i + 1);
            }
        }
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
}
