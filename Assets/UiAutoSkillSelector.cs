using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UiAutoSkillSelector : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> toggleObject;

    [SerializeField]
    private List<GameObject> toggleCheckBoxObject;

    //[SerializeField]
    //private GameObject jumpAutoObject;

    //[SerializeField]
    //private GameObject jumpToggleObject;
    [SerializeField] private List<GameObject> _visionSkills; //버튼과 토글

    void Start()
    {
        SkillCoolTimeManager.LoadSelectedSkill();
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill0).AsObservable().Subscribe(e =>
        {
            for (int i = 0; i < _visionSkills.Count; i++)
            {
                _visionSkills[i].SetActive(ServerData.goodsTable.GetVisionSkillIdx()>0);
            }
        }).AddTo(this);


    }

}
