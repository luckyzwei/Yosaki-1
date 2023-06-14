using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;


public class MagicBook_Fancy
{
    public MagicBookData MagicBookData { get; private set; }
    public UiMagicBookInventoryView ParentView { get; private set; }
    public MagicBook_Fancy(MagicBookData magicbookData,UiMagicBookInventoryView parentView)
    {
        this.MagicBookData = magicbookData;
        this.ParentView = parentView;

    }

    public void Upgrade()
    {
        if (MagicBookData != null)
        {
            int amount = ServerData.magicBookTable.TableDatas[MagicBookData.Stringid].amount.Value;

            if (amount < MagicBookData.Requireupgrade)
            {
                return;
            }

            if (TableManager.Instance.MagicBoocDatas.TryGetValue(MagicBookData.Id + 1, out var nextMagicBook))
            {
                int currentWeaponCount = ServerData.magicBookTable.GetCurrentMagicBookCount(MagicBookData.Stringid);
                int nextWeaponCount = ServerData.magicBookTable.GetCurrentMagicBookCount(nextMagicBook.Stringid);

                int upgradeNum = currentWeaponCount / MagicBookData.Requireupgrade;

                ServerData.magicBookTable.UpData(MagicBookData, upgradeNum * MagicBookData.Requireupgrade * -1);
                ServerData.magicBookTable.UpData(nextMagicBook, upgradeNum);

                DailyMissionManager.UpdateDailyMission(DailyMissionKey.MagicbookUpgrade, upgradeNum);
                ServerData.magicBookTable.SyncToServerAll(new List<int>() { MagicBookData.Id, nextMagicBook.Id });
            }
            else
            {
                //맥스레벨 처리
                PopupManager.Instance.ShowAlarmMessage("더이상 승급이 불가능 합니다.");
            }
        }
    }
}
public class UiMagicBookInventoryView : FancyScrollView<MagicBook_Fancy>
{
    [SerializeField] private MagicBookType _magicBookType;
    
    [SerializeField]
    private Scroller scroller;
    
    private List<MagicBook_Fancy> magicBookDataContainer = new List<MagicBook_Fancy>();
    private List<MagicBook_Fancy> magicBookDataContainer2 = new List<MagicBook_Fancy>();
    
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;
    
    private void Start()
    {
        MakeBoard();

    }

    private void MakeBoard()
    {
                    
        switch (_magicBookType)
        {
            case MagicBookType.Basic:
            case MagicBookType.Normal:
                MakeBasicNormalBoard();
                break;
            case MagicBookType.View:
                MakeViewBoard();
                break;
            default:
                break;
        }
    }
    public void AllUpgradeMagicBook(int myIdx)
    {
        for (int i = 0; i <= myIdx; i++)
        {
            magicBookDataContainer2[i].Upgrade();
        }
    }
    private List<MagicBook_Fancy> SortHasItemList(List<MagicBook_Fancy> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            MagicBook_Fancy data = list[i];
            if (data != null && ServerData.magicBookTable.TableDatas[data.MagicBookData.Stringid].hasItem.Value > 0)
            {
                if (list[0].MagicBookData.Id < list[i].MagicBookData.Id)
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
        MakeBoard();
    }
    
    private void OnEnable()
    {
        SortHasItem();
    }
    private void MakeBasicNormalBoard()
    {
        magicBookDataContainer.Clear();
        magicBookDataContainer2.Clear();
        scroller.Initialize(TypeScroll.InventoryView);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.MagicBookTable.dataArray;
    
        List<MagicBook_Fancy> passInfos = new List<MagicBook_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            if(tableData[i].MAGICBOOKTYPE == MagicBookType.View) continue;
            var magicBookData = new MagicBookData();

            magicBookData = tableData[i];
            
            passInfos.Add(new MagicBook_Fancy(magicBookData,this));
            magicBookDataContainer.Add(new MagicBook_Fancy(magicBookData,this));
            magicBookDataContainer2.Add(new MagicBook_Fancy(magicBookData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.MagicBookData.Id].Displayorder.CompareTo(tableData[b.MagicBookData.Id].Displayorder));
    
        for (int i = 0; i < passInfos.Count; i++)
        {
            MagicBook_Fancy magicBookData = passInfos[i];
            if (magicBookData != null && ServerData.magicBookTable.TableDatas[magicBookData.MagicBookData.Stringid].hasItem.Value > 0)
            {
                passInfos.Insert(0, passInfos[i]);
                passInfos.RemoveAt(i + 1);
            }
        }
        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
    private void MakeViewBoard()
    {
        scroller.Initialize(TypeScroll.InventoryOnlyView);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.MagicBookTable.dataArray;
    
        List<MagicBook_Fancy> passInfos = new List<MagicBook_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            
            if(tableData[i].MAGICBOOKTYPE != MagicBookType.View) continue;
            var magicBookData = new MagicBookData();

            magicBookData = tableData[i];
            
            passInfos.Add(new MagicBook_Fancy(magicBookData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.MagicBookData.Id].Displayorder.CompareTo(tableData[b.MagicBookData.Id].Displayorder));
    
        for (int i = 0; i < passInfos.Count; i++)
        {
            MagicBook_Fancy magicBookData = passInfos[i];
            if (magicBookData != null && ServerData.magicBookTable.TableDatas[magicBookData.MagicBookData.Stringid].hasItem.Value > 0)
            {
                passInfos.Insert(0, passInfos[i]);
                passInfos.RemoveAt(i + 1);
            }
        }
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
}
