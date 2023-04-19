using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineColorChanger : MonoBehaviour
{
    private Color color = new Color(0.3f,0.3f,0.3f);
    
    void Awake()
    {
       var skeleton = GetComponent<SkeletonAnimation>();
       
       skeleton.skeleton.SetColor(color);
        
       
    }

}
