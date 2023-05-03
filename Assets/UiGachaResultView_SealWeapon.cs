using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGachaResultView_SealWeapon : SingletonMono<UiGachaResultView_SealWeapon>
{
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private GachaResultViewCell GachaResultViewCellPrefab;

    [SerializeField]
    private Transform cellParent;

    private List<GachaResultViewCell> GachaResultViewCellContainer = new List<GachaResultViewCell>();

    [SerializeField]
    private GameObject closeButton;


    private Action retryCallback;

    private Coroutine directionRoutine;

    private List<GachaResultCellInfo> results;

    [HideInInspector]
    public bool isAuto = false;




    private WaitForSeconds autoDelay = new WaitForSeconds(0.35f);


    private void OnEnable()
    {
        OnAutoStateChanged(false);
    }

    public void OnAutoStateChanged(bool auto)
    {
        isAuto = auto;

        if (auto)
        {

            if (directionRoutine == null)
            {
                retryCallback?.Invoke();
            }
        }
        else
        {
            
        }
    }

    private enum State
    {
        playing, end
    }

    private State state;

    public void Initialize(List<GachaResultCellInfo> results, Action retryCallback)
    {
        state = State.playing;

        this.retryCallback = retryCallback;

        closeButton.SetActive(false);

        GachaResultViewCellContainer.ForEach(e => e.gameObject.SetActive(false));

        rootObject.SetActive(true);

        int interval = Mathf.Min(results.Count, 500) - GachaResultViewCellContainer.Count;

        interval = Mathf.Min(interval, 500);

        for (int i = 0; i < interval; i++)
        {
            var cell = Instantiate<GachaResultViewCell>(GachaResultViewCellPrefab, cellParent);
            GachaResultViewCellContainer.Add(cell);
        }


        this.results = results;

        directionRoutine = StartCoroutine(ActiveRoutine());

    }

    private string GachaCompleteKey = "GachaComplete";
    private IEnumerator ActiveRoutine()
    {
        SoundManager.Instance.PlaySound(GachaCompleteKey);

        if (isAuto == false)
        {
            WaitForSeconds delay = new WaitForSeconds(0.01f);

            int loopNum = Mathf.Min(GachaResultViewCellContainer.Count, 500);

            for (int i = 0; i < loopNum; i++)
            {
                GachaResultViewCellContainer[i].gameObject.SetActive(i < results.Count);

                if (i < results.Count)
                {
                    GachaResultViewCellContainer[i].Initialzie(results[i].weaponData, results[i].magicBookData, results[i].skillData, results[i].newGachaData,results[i].sealSwordData,results[i].amount);
                }

                if (isAuto == false)
                {
                    yield return delay;
                }
            }

        }

        if (isAuto)
        {
            SkipDirection();
            yield return autoDelay;
            retryCallback?.Invoke();
            yield break;
        }
        else
        {
            WhenDirectionEnd();
        }
    }

    private void WhenDirectionEnd()
    {

        directionRoutine = null;

        closeButton.SetActive(true);


        state = State.end;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAuto == false)
        {
            if (state == State.playing)
            {
                SkipDirection();
            }
        }
    }

    private void SkipDirection()
    {
        for (int i = 0; i < GachaResultViewCellContainer.Count; i++)
        {
            GachaResultViewCellContainer[i].gameObject.SetActive(i < results.Count);

            if (i < results.Count)
            {
                GachaResultViewCellContainer[i].Initialzie(results[i].weaponData, results[i].magicBookData, results[i].skillData, results[i].newGachaData,results[i].sealSwordData, results[i].amount);
            }
        }

        if (directionRoutine != null)
        {
            StopCoroutine(directionRoutine);
        }

        WhenDirectionEnd();
    }

    public void OnClickRetryButton()
    {
        retryCallback?.Invoke();
    }


}
