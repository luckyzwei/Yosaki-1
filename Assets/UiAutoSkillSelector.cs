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
    [SerializeField] private List<GameObject> _visionSkills;

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
                _visionSkills[i].SetActive(e > 0);
            }
        }).AddTo(this);


        //AutoManager.Instance.AutoMode.AsObservable().Subscribe(e =>
        //{
        //    toggleObject.ForEach(element => element.gameObject.SetActive(e));
        //    //jumpAutoObject.gameObject.SetActive(e);
        //}).AddTo(this);

        //var list = SkillCoolTimeManager.registeredSkillIdx;

        //for (int i = 0; i < list.Count; i++)
        //{
        //    int idx = i;
        //    list[i].AsObservable().Subscribe(id =>
        //    {
        //        toggleCheckBoxObject[idx].SetActive(id == 1);
        //    }).AddTo(this);
        //}
    }

}
