using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScrollRectAutoScroll[] _scrollRectAutoScroll;
    [SerializeField] private GoldUI _goldUI;
    [SerializeField] private Bet _bet;
    [SerializeField] private Button[] buttons;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource win;
    
    private static GameManager _instance;

    private Coroutine waintCoroutine;
    private List<int> results = new ();
    private List<Sprite> resultsType = new ();
    private float startTime;


    private const string GoldKey = "gold";
    private int _gold;
    public int Gold
    {
        get { return _gold; }
        set 
        {
            _gold = value;
            PlayerPrefs.SetInt(GoldKey, _gold);
            _goldUI.SetUI(_gold);
        }
    }
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        Gold = PlayerPrefs.GetInt(GoldKey, 1000);
        Gold = 1000;
        _bet.Init(10f, Gold);
    }
    public void Roll()
    {
        audioSource.Play();
        foreach (var button in buttons)
            button.interactable = false;
        Gold -= Mathf.RoundToInt(_bet.value);
        results.Clear();
        resultsType.Clear();
        for (int i = 0; i < _scrollRectAutoScroll.Length; i++)
        {
            _scrollRectAutoScroll[i].StartScroll(1 + i);
        }
        waintCoroutine = StartCoroutine(WaitResult());
    }
    
    private IEnumerator WaitResult()
    {
        bool allResultsReceived = false;

        while (!allResultsReceived)
        {
            allResultsReceived = true;

            foreach (var scrollRectAutoScroll in _scrollRectAutoScroll)
            {
                if (scrollRectAutoScroll.resultIndex == -1)
                {
                    allResultsReceived = false;
                    yield return null;
                    break;
                }
            }
        }

        foreach (var scrollRectAutoScroll in _scrollRectAutoScroll)
        {
            resultsType.Add(scrollRectAutoScroll.resultSprite);
            results.Add(scrollRectAutoScroll.resultIndex);
        }
        Debug.Log("results[0]"+ results[0]);
        
        StopCoroutine(waintCoroutine);
        Gold += CalculeResult();
        
        foreach (var button in buttons)
            button.interactable = true;
        
        _bet.Init(10f, Gold);
    }

    private IEnumerator ScaleAnimanion(RectTransform transform)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 1.35f;

        float timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        startScale = transform.localScale;
        endScale = Vector3.one;
        timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private int CalculeResult()
    {
        var coroutines = new List<Coroutine>();
        startTime = Time.time;
        if (resultsType[0] == resultsType[1])
        {
            if (resultsType[0] == resultsType[2])
            {
                coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[0].items[results[0]])));
                coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[1].items[results[1]])));
                coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[2].items[results[2]])));
                win.Play();
                return (int)_bet.value * 8;
            }
            coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[0].items[results[0]])));
            coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[1].items[results[1]])));
            win.Play();
            return (int)_bet.value * 2; 
        }
        else if (resultsType[1] == resultsType[2])
        {
            coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[1].items[results[1]])));
            coroutines.Add(StartCoroutine(ScaleAnimanion(_scrollRectAutoScroll[2].items[results[2]])));
            win.Play();
            return (int)_bet.value * 2;
        }
        return 0;
    }
}
