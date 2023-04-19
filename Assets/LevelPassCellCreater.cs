using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPassCellCreater : MonoBehaviour
{
    [SerializeField]
    private UiLevelPassCell levelPassCell;

    [SerializeField] private UiLEvelPassBuyButton4 _uiLEvelPassBuyButton4;
    
    [SerializeField]
    private Transform cellParent;

    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _buyText;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private Image _image;
    private float cellSize = 105.9f;


    private bool initialized = false;

    private int scrollId = 0;

    [SerializeField]
    private int GradeId = 0;

    [SerializeField] private bool InitByAuto = false;
    private void Start()
    {
        if (InitByAuto == false)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        var tableData = TableManager.Instance.LevelPass.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Passgrade != GradeId) continue;

            var prefab = Instantiate<UiLevelPassCell>(levelPassCell, cellParent);

            var passInfo = new PassInfo();

            passInfo.require = tableData[i].Unlocklevel;
            passInfo.id = tableData[i].Id;

            passInfo.rewardType_Free = tableData[i].Reward1_Free;
            passInfo.rewardTypeValue_Free = tableData[i].Reward1_Value;
            passInfo.rewardType_Free_Key = NewLevelPass.freeReward;

            passInfo.rewardType_IAP = tableData[i].Reward2_Pass;
            passInfo.rewardTypeValue_IAP = tableData[i].Reward2_Value;
            passInfo.rewardType_IAP_Key = NewLevelPass.premiumReward;
            passInfo.passGrade = tableData[i].Passgrade;

            prefab.Initialize(passInfo);
        }
    }

    public void Initialize(int passNum, SeletableTab seletableTab)
    {
        var data = TableManager.Instance.InAppPurchase.dataArray[passNum];
        var levelPassData = TableManager.Instance.LevelPass.dataArray;
        _uiLEvelPassBuyButton4.SetNewString(data.Productid);
        int passCount = int.Parse(data.Productid.Replace("levelpass", ""));
        
        //색변경
        var spriteIdx = passCount % _sprites.Count;
        _image.sprite = _sprites[spriteIdx];
        _title.color = _colors[spriteIdx];
        //
        _title.SetText($"여우패스 {passCount}");
        var startLevel = 0;
        var endLevel = 0;
        for (var i = 0; i < levelPassData.Length; i += 100)
        {
            if (levelPassData[i].Shopid == data.Productid)
            {
                for (var j = i; levelPassData[i].Shopid == data.Productid; j--)
                {
                    i--;
                }

                startLevel = levelPassData[i - 1].Unlocklevel + 1000;
                endLevel = levelPassData[i + 99].Unlocklevel + 1000;
                
                break;
            }
        }
        
        _buyText.SetText($"패스 구매 \nLV {Utils.ConvertBigNum(startLevel)}~{Utils.ConvertBigNum(endLevel)}");

        GradeId = passCount - 1;
        
        var tableData = TableManager.Instance.LevelPass.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Passgrade != GradeId) continue;

            var prefab = Instantiate<UiLevelPassCell>(levelPassCell, cellParent);

            var passInfo = new PassInfo();

            passInfo.require = tableData[i].Unlocklevel;
            passInfo.id = tableData[i].Id;

            passInfo.rewardType_Free = tableData[i].Reward1_Free;
            passInfo.rewardTypeValue_Free = tableData[i].Reward1_Value;
            passInfo.rewardType_Free_Key = NewLevelPass.freeReward;

            passInfo.rewardType_IAP = tableData[i].Reward2_Pass;
            passInfo.rewardTypeValue_IAP = tableData[i].Reward2_Value;
            passInfo.rewardType_IAP_Key = NewLevelPass.premiumReward;
            passInfo.passGrade = tableData[i].Passgrade;

            prefab.Initialize(passInfo);
        }

        seletableTab.AddGameObject(this.gameObject);
    }
}
