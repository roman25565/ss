using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectAutoScroll : MonoBehaviour
{
    public float scrollSpeed;
    public float scrollTime;

    public bool isScrolling;
    public Sprite[] itemPrefabs;
    public int itemCount;
    public RectTransform prefab;
    
    private ScrollRect scrollRect;
    private Coroutine scrollCoroutine;
    private float startTime;
    private int targetIndex;
    

    [HideInInspector]public List<RectTransform> items = new ();
    private VerticalLayoutGroup _verticalLayoutGroup;
    private float height;
    private Vector3 midlePoz;
    private Transform content;
    private Vector3 targetPosition;
    private Vector3 startScrollPosition;

    [HideInInspector]public int resultIndex;
    [HideInInspector]public Sprite resultSprite;
    private void Start()
    {
        scrollRect = transform.GetComponent<ScrollRect>();
        content = scrollRect.content;
        _verticalLayoutGroup = content.GetComponent<VerticalLayoutGroup>();
        for (int i = 0; i < itemCount; i++)
        {
            var item = Instantiate(prefab, content);
            item.GetComponent<Image>().sprite = itemPrefabs[i % itemPrefabs.Length];
            
            // item.SetAsFirstSibling();
            items.Add(item);
        }
        height = items[0].rect.height;
        Debug.Log("height:" + height);
        var localPoz= content.localPosition;
        midlePoz = new Vector3(localPoz.x,
            (itemCount - 2.7f) * (height + _verticalLayoutGroup.spacing) / 2 - localPoz.y, 
            localPoz.z);
        content.localPosition = midlePoz;
    }

    public void StartScroll(float time)
    {
        if (isScrolling)
        {
            StopScroll();
        }

        startTime = Time.time;
        scrollTime = time;
        isScrolling = true;
        startScrollPosition = scrollRect.content.localPosition;
        targetPosition = new Vector3(0, startScrollPosition.y - scrollSpeed * scrollTime * 30);
        resultIndex = -1;
        
        scrollCoroutine = StartCoroutine(ScrollToTarget());
    }

    public void StopScroll()
    {
        if (isScrolling)
        {
            isScrolling = false;
             

            startTime = Time.time;
            var s =StartCoroutine(StopScrollToTarget());
            // Отримати індекс елемента, на якому зупинилась прокрутка
            targetIndex = GetItemIndexAtScrollPosition(scrollRect.normalizedPosition.y);

            // Викликати подію зупинки прокрутки
            OnScrollStopped?.Invoke(targetIndex);
            
        }
    }

    private IEnumerator StopScrollToTarget()
    {
        while (Time.time - startTime < 0.2f / 2f)
        {
            Canvas.ForceUpdateCanvases();
            float t = (Time.time - startTime) / 0.2f;
            
            var contentLocalPosition = scrollRect.content.localPosition;
            var a = math.abs(contentLocalPosition.y) % (height + _verticalLayoutGroup.spacing);
            scrollRect.content.localPosition = Vector3.Lerp(contentLocalPosition, 
                contentLocalPosition + Vector3.down * a, t);
            
            yield return null;
        }

        resultIndex = Mathf.FloorToInt(scrollRect.content.localPosition.y / (height + _verticalLayoutGroup.spacing)) + 1;
        resultSprite = items[resultIndex].GetComponent<Image>().sprite;
        
        StopCoroutine(StopScrollToTarget());
    }

    private IEnumerator ScrollToTarget()
    {
        while (isScrolling && (Time.time - startTime) < scrollTime)
        {
            if (scrollRect.normalizedPosition.y >= .99f)
            {
                var step = new Vector3(0, (items.Count - 3f) * (height + _verticalLayoutGroup.spacing));
                scrollRect.content.localPosition = step;
            }
            scrollRect.content.localPosition += Vector3.down * (2000 * Time.deltaTime);
                
            yield return null;
        }

        StopScroll();
    }

    private int GetItemIndexAtScrollPosition(float normalizedPosition)
    {
        // ... Ваш код для визначення індексу елемента за нормалізованою позицією прокрутки ...
        return 0;
    }

    public event Action<int> OnScrollStopped; // Подія, яка викликається при зупинці прокрутки
}