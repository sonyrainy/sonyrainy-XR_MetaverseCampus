using UnityEngine;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour
{
    private List<View> m_Views = new List<View>();

    void Start()
    {
        // 예제 데이터 추가 (테스트용으로 몇 개의 뷰를 추가합니다)
        m_Views.Add(new View(new Rect(0, 0, 0.5f, 0.5f)));
        m_Views.Add(new View(new Rect(0.5f, 0, 0.5f, 0.5f)));
    }

    public Rect GetViewport(int viewIndex = 0)
    {
        // 리스트가 비어있는지 확인
        if (m_Views == null || m_Views.Count == 0)
        {
            Debug.LogError("The m_Views list is empty.");
            return new Rect(); // 기본값 반환
        }

        // 유효한 인덱스인지 확인
        if (viewIndex < 0 || viewIndex >= m_Views.Count)
        {
            Debug.LogError($"Index {viewIndex} was out of range. Must be non-negative and less than the size of the collection.");
            return new Rect(); // 기본값 반환
        }

        return m_Views[viewIndex].viewport;
    }
}

public class View
{
    public Rect viewport;

    public View(Rect viewport)
    {
        this.viewport = viewport;
    }
}