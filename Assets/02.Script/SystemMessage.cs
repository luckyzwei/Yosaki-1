using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : SingletonMono<SystemMessage>
{
    [SerializeField]
    private List<UiMessageText> messagePool;

    private int currentIdx = 0;

    [SerializeField]
    private Mask mask;

    [SerializeField]
    private Image maskImage;

    [SerializeField]
    private Image expandButton;

    [SerializeField]
    private Sprite expand;

    [SerializeField]
    private Sprite shrink;

    [SerializeField]
    private GameObject rootObject;

    private new void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        messagePool.ForEach(e => e.Initialize(string.Empty, true));
        UpdateUi();
    }

    public void OnClickExpandButton()
    {
        mask.enabled = !mask.enabled;

        UpdateUi();
    }

    private void UpdateUi()
    {
        expandButton.sprite = mask.enabled ? expand : shrink;

        maskImage.color = new Color(maskImage.color.r, maskImage.color.g, maskImage.color.b, mask.enabled ? 1f : 0f);

        rootObject.transform.localScale = mask.enabled ? Vector3.one*1.1f : Vector3.one * 1.1f /*1.2f*/;
    }

    private void Start()
    {
        StartCoroutine(messageUpdateRoutine());
    }

    private WaitForSeconds updateDelay = new WaitForSeconds(0.1f);
    
    private IEnumerator messageUpdateRoutine()
    {
        while (true)
        {
            if (messageQueue.Count != 0)
            {
                messagePool[currentIdx].Initialize(messageQueue.Dequeue(), true);
                messagePool[currentIdx].transform.SetAsFirstSibling();
                currentIdx++;
                
                if (currentIdx == messagePool.Count)
                {
                    currentIdx = 0;
                }
            }
         
            yield return updateDelay;
        }
        
    }

    public static Queue<string> messageQueue = new Queue<string>();
    
    private const int messageQueueMaxSize = 10;
    
    public static bool IsMessageQueueFull()
    {
        return messageQueue.Count >= messageQueueMaxSize;
    }
    public void SetMessage(string message)
    {
        messageQueue.Enqueue(message);
    }
}
