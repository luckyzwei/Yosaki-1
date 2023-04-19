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
    [SerializeField] public Button _button;
    [SerializeField] public Image _Image;
    [SerializeField] public List<Sprite> _Sprites;
    
    private SeletableTab _selectableTab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
//idx 227 productid levelpass44 passNum 227

    public void Initialize(int passNum, SeletableTab seletableTab)
    {
        _selectableTab = seletableTab;
        var data = TableManager.Instance.InAppPurchase.dataArray[passNum];
        var levelPassData = TableManager.Instance.LevelPass.dataArray;
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

        int passCount = int.Parse(data.Productid.Replace("levelpass", ""));
        var spriteIdx = passCount % _Sprites.Count;
        _Image.sprite = _Sprites[spriteIdx];
        _textMeshProUGUI.SetText($"여우패스 {passCount}\nLV {Utils.ConvertBigNum(startLevel)}~{Utils.ConvertBigNum(endLevel)}");

        _selectableTab.AddElement(_Image, _textMeshProUGUI);
        _button.onClick.AddListener(() =>
        {
            _selectableTab.OnSelect(passCount - 1);
        });
    }
}
