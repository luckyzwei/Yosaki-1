using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUltiSkillEffect : SingletonMono<UiUltiSkillEffect>
{
    [SerializeField]
    private List<GameObject> ultSkillEffect;
    
    public void ShowUltSkillEffect(int idx)
    {
        if (SettingData.showVisionSkill.Value == 0 ) return;
        
        if (PlayerMoveController.Instance.MoveDirection == MoveDirection.Left)
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
   
        
        if (idx == 46)
        {
            ultSkillEffect[0].SetActive(true);
        }

        if (idx == 47)
        {
            ultSkillEffect[1].SetActive(true);
        }

        if (idx == 48)
        {
            ultSkillEffect[2].SetActive(true);
        }

        if (idx == 49)
        {
            ultSkillEffect[3].SetActive(true);
        }
        
        if (idx == 55)
        {
            ultSkillEffect[4].SetActive(true);
        }  
        
        if (idx == 95)
        {
            ultSkillEffect[5].SetActive(true);
        }
    }

    public void OffAllUltSkillEffect()
    {
        for (var i = 0; i < ultSkillEffect.Count; i++)
        {
            ultSkillEffect[i].gameObject.SetActive(false);   
        }
    }
    
    
    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowUltSkillEffect(95);
        }
    }
#endif
}
