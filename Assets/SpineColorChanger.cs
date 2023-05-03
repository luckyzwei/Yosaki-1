using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class SpineColorChanger : MonoBehaviour
{
    public Color color0 = new Color(0.3f,0.3f,0.3f);
    
    void Awake()
    {
       var skeleton = GetComponent<SkeletonAnimation>();
       
       skeleton.skeleton.SetColor(color0);
    }

}
