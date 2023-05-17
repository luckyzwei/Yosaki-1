using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class WeaponData_Fancy
{
    public WeaponData WeaponData { get; private set; }
    public UiWeaponInventoryView ParentView { get; private set; }
    public WeaponData_Fancy(WeaponData weaponData,UiWeaponInventoryView parentView)
    {
        this.WeaponData = weaponData;
        this.ParentView = parentView;
    }
    public void Upgrade()
    {
        if (WeaponData != null)
        {
            int amount = ServerData.weaponTable.TableDatas[WeaponData.Stringid].amount.Value;

            if (amount < WeaponData.Requireupgrade)
            {
                return;
            }

            if (TableManager.Instance.WeaponData.TryGetValue(WeaponData.Id + 1, out var nextWeaponData))
            {
                int currentWeaponCount = ServerData.weaponTable.GetCurrentWeaponCount(WeaponData.Stringid);
                int nextWeaponCount = ServerData.weaponTable.GetCurrentWeaponCount(nextWeaponData.Stringid);

                int upgradeNum = currentWeaponCount / WeaponData.Requireupgrade;

                ServerData.weaponTable.UpData(WeaponData, upgradeNum * WeaponData.Requireupgrade * -1);
                ServerData.weaponTable.UpData(nextWeaponData, upgradeNum);

                DailyMissionManager.UpdateDailyMission(DailyMissionKey.WeaponUpgrade, upgradeNum);
                ServerData.weaponTable.SyncToServerAll(new List<int>() { WeaponData.Id, nextWeaponData.Id });
            }
            else
            {
                //맥스레벨 처리
                PopupManager.Instance.ShowAlarmMessage("더이상 승급이 불가능 합니다.");
            }
        }
    }
}

public class UiWeaponInventoryView : FancyScrollView<WeaponData_Fancy>
{
    [SerializeField]
    private UiInventoryWeaponView uiInventoryWeaponViewPrefab;
    
    [SerializeField]
    private Transform viewParentWeapon;
    
    private List<WeaponData_Fancy> weaponDataContainer = new List<WeaponData_Fancy>();
    
    
    
    
    [SerializeField]
    private UiWeaponDetailView uiWeaponDetailView;

    [SerializeField] private WeaponType _weaponType;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     MakeWeaponBoard();
    // }
    private void OnClickWeaponView(WeaponData weaponData, MagicBookData magicBookData)
    {
        uiWeaponDetailView.gameObject.SetActive(true);
        uiWeaponDetailView.Initialize(weaponData, magicBookData);
    }
    [SerializeField]
    private Scroller scroller;
    
    
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;
    
    private void Start()
    {
            
        switch (_weaponType)
        {
            case WeaponType.Basic:
            case WeaponType.Normal:
                MakeBasicNormalBoard();
                break;
            case WeaponType.View:
                MakeViewBoard();
                break;
            case WeaponType.RecommendView:
                MakeRecommendViewBoard();
                break;
            case WeaponType.HasEffectOnly:
                MakeEffectOnlyBoard();
                break;
            default:
                break;
        }

    }

    public void AllUpgradeWeapon(int myIdx)
    {
        for (int i = 0; i <= myIdx; i++)
        {
            weaponDataContainer[i].Upgrade();
        }
    }
    
    private void MakeBasicNormalBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.WeaponTable.dataArray;
    
        List<WeaponData_Fancy> passInfos = new List<WeaponData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            
            if(tableData[i].WEAPONTYPE == WeaponType.View) continue;
            if(tableData[i].WEAPONTYPE == WeaponType.RecommendView) continue;
            if(tableData[i].WEAPONTYPE == WeaponType.HasEffectOnly) continue;
            var weaponData = new WeaponData();

            weaponData = tableData[i];
            
            passInfos.Add(new WeaponData_Fancy(weaponData,this));
            weaponDataContainer.Add(new WeaponData_Fancy(weaponData,this));
        }
        
        passInfos.Sort((a, b) => tableData[a.WeaponData.Id].Displayorder.CompareTo(tableData[b.WeaponData.Id].Displayorder));
    
        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
    private void MakeViewBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.WeaponTable.dataArray;
    
        List<WeaponData_Fancy> passInfos = new List<WeaponData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            
            if(tableData[i].WEAPONTYPE != WeaponType.View) continue;
            var weaponData = new WeaponData();

            weaponData = tableData[i];
            
            passInfos.Add(new WeaponData_Fancy(weaponData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.WeaponData.Id].Displayorder.CompareTo(tableData[b.WeaponData.Id].Displayorder));
    
        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }

    private List<WeaponData_Fancy> SortHasItemList(List<WeaponData_Fancy> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            WeaponData_Fancy weaponData = list[i];
            if (weaponData != null && ServerData.weaponTable.TableDatas[weaponData.WeaponData.Stringid].hasItem.Value > 0)
            {
                list.Insert(0, list[i]);
                list.RemoveAt(i + 1);
            }
        }

        return list;
    }
    private void MakeRecommendViewBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.WeaponTable.dataArray;
    
        List<WeaponData_Fancy> passInfos = new List<WeaponData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            
            if(tableData[i].WEAPONTYPE != WeaponType.RecommendView) continue;
            var weaponData = new WeaponData();

            weaponData = tableData[i];
            
            passInfos.Add(new WeaponData_Fancy(weaponData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.WeaponData.Id].Displayorder.CompareTo(tableData[b.WeaponData.Id].Displayorder));

        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
    private void MakeEffectOnlyBoard()
    {
        scroller.Initialize(PassTypeScroll.None);
            
        scroller.OnValueChanged(UpdatePosition);
        var tableData = TableManager.Instance.WeaponTable.dataArray;
    
        List<WeaponData_Fancy> passInfos = new List<WeaponData_Fancy>();
    
        for (int i = 0; i < tableData.Length; i++)
        {
            if(tableData[i].WEAPONTYPE != WeaponType.HasEffectOnly) continue;
            var weaponData = new WeaponData();

            weaponData = tableData[i];
            
            passInfos.Add(new WeaponData_Fancy(weaponData,this));
    
        }
        
        passInfos.Sort((a, b) => tableData[a.WeaponData.Id].Displayorder.CompareTo(tableData[b.WeaponData.Id].Displayorder));
    
        passInfos = SortHasItemList(passInfos);
        this.UpdateContents(passInfos.ToArray());
        scroller.SetTotalCount(passInfos.Count);
    }
}
