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
    private List<RingData_Fancy> ringDataContainer2 = new List<RingData_Fancy>();
    
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;
    
    private void Start()
    {
        MakeBoard();

    }

    private void MakeBoard()
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
    private List<RingData_Fancy> SortHasItemList(List<RingData_Fancy> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            RingData_Fancy data = list[i];
            if (data != null && ServerData.newGachaServerTable.TableDatas[data.RingData.Stringid].hasItem.Value > 0)
            {
                if (list[0].RingData.Id < list[i].RingData.Id)
                {
                    list.Insert(0, list[i]);
                    list.RemoveAt(i + 1);
                }
            }
        }

        return list;
    }
    public void SortHasItem()
    {
        ringDataContainer = SortHasItemList(ringDataContainer);
        this.UpdateContents(ringDataContainer.ToArray());
    }
    
    private void OnEnable()
    {
        SortHasItem();
    }
    public void AllUpgradeRing(int myIdx)
    {
        for (int i = 0; i <= myIdx; i++)
        {
            ringDataContainer2[i].Upgrade();
        }
    }
    private void MakeBasicNormalBoard()
    {
        ringDataContainer.Clear();
        ringDataContainer2.Clear();
        
        scroller.Initialize(TypeScroll.InventoryView);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.NewGachaTable.dataArray;
    
        List<RingData_Fancy> passInfos = new List<RingData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            var ringData = new NewGachaTableData();

            ringData = tableData[i];
            
            passInfos.Add(new RingData_Fancy(ringData,this));
            ringDataContainer.Add(new RingData_Fancy(ringData,this));
            ringDataContainer2.Add(new RingData_Fancy(ringData,this));
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
        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
    
}
