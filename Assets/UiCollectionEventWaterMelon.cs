using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UiCollectionEventWaterMelon : MonoBehaviour
{
    [SerializeField] private UiCollectionFallEventCell eventCostumeCell;
    [SerializeField] private Transform costumeTransform;
    [SerializeField] private UiCollectionFallEventCell eventGoodsCell;
    [SerializeField] private Transform goodsTransform;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tabledata = TableManager.Instance.fallCollection.dataArray;

        for (int i = 0; i < tabledata.Length; i++)
        {
            if (tabledata[i].Active == false) continue;
            if (Utils.IsCostumeItem((Item_Type)tabledata[i].Itemtype))
            {
                var prefab = Instantiate(eventCostumeCell, costumeTransform);
                prefab.Initialize(i);
            }
            else
            {
                var prefab = Instantiate(eventGoodsCell, goodsTransform);
                prefab.Initialize(i);
            }
        }
    }
}