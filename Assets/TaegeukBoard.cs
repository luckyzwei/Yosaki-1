using System.Collections;
using System.Collections.Generic;
using BackEnd;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UniRx;

public class TaegeukBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentFloor;

    [SerializeField]
    private SkeletonGraphic skeletonGraphic;
    
    public void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekTower].AsObservable().Subscribe(e => { currentFloor.SetText($"{e + 1}단계 입장"); }).AddTo(this);

        ServerData.equipmentTable.TableDatas[EquipmentTable.CostumeLook].AsObservable().Subscribe(e =>
        {

            skeletonGraphic.skeletonDataAsset = CommonUiContainer.Instance.costumeList[e];

        }).AddTo(this);

    }
    
    public void OnClickEnterButton()
    {
        int currentIdx = (int)ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.taeguekTower].Value;

        if (currentIdx >= TableManager.Instance.taegeukTitle.dataArray.Length)
        {
            PopupManager.Instance.ShowAlarmMessage("도전 완료!");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{currentIdx + 1}단계\n도전 할까요?", () =>
        {
            GameManager.Instance.LoadContents(GameManager.ContentsType.TaeguekTower);

        }, null);
    }
    
    
}
