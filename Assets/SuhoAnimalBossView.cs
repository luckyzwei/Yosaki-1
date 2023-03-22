using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuhoAnimalBossView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer.sprite = CommonResourceContainer.GetSuhoAnimalSprite(GameManager.Instance.suhoAnimalId);
    }
    


}
