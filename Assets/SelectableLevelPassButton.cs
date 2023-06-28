using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class SelectableLevelPassButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] public Image _Image;
    [SerializeField] public List<Sprite> _Sprites;

    private LevelPassCellCreater _levelPassCellCreater;

    private int startLevel = 0;
    private int endLevel = 0;

    private int passGrade = 0;

    //왼쪽탭
    public void Initialize(int absolutedPassId, LevelPassCellCreater levelPassCellCreater)
    {
        _levelPassCellCreater = levelPassCellCreater;
        
        var data = TableManager.Instance.InAppPurchase.dataArray[absolutedPassId];
        var levelPassData = TableManager.Instance.LevelPass.dataArray;
        int passCount = 0;
        
        if (data.Productid.Equals("levelpass"))
        {
            passCount = 1;
        }
        else
        {
            passCount= int.Parse(data.Productid.Replace("levelpass", ""));
        }

        passGrade = passCount - 1;
        var spriteIdx = passCount % _Sprites.Count;
        _Image.sprite = _Sprites[spriteIdx];
        startLevel = RoundDownToTenThousand(levelPassData[GetPassMin(levelPassData,data)].Unlocklevel);
        endLevel = RoundDownToTenThousand(levelPassData[GetPassMax(levelPassData,data)].Unlocklevel);
        _textMeshProUGUI.SetText($"여우패스 {passCount}\nLV {Utils.ConvertBigNum(startLevel)}~{Utils.ConvertBigNum(endLevel)}");
    }

    private int GetPassMin(LevelPassData[] levelPassData,InAppPurchaseData data)
    {
        for (var i = 0; i < levelPassData.Length; i+=100)
        {
            if (levelPassData[i].Shopid != data.Productid)
            {
                continue;
            }
            
            return levelPassData[i].Idminmax[0];
        }

        return 0;
    }
    private int GetPassMax(LevelPassData[] levelPassData,InAppPurchaseData data)
    {
        for (var i = 0; i < levelPassData.Length; i+=100)
        {
            if (levelPassData[i].Shopid != data.Productid)
            {
                continue;
            }
            
            return levelPassData[i].Idminmax[1];
        }

        return 0;
    }
    private int RoundDownToTenThousand(int number)
    {
        int result = number / 10000 * 10000;
        return result;
    }

    public void OnClickButton()
    {
        _levelPassCellCreater.ChangeContents(passGrade);
    }
}
