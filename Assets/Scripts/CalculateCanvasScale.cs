using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCanvasScale : MonoBehaviour
{
    [Header("������ � ������:")]
    [SerializeField] private float widthInMeters = 1;

    public void Calculate()
    {
        Canvas target = GetComponent<Canvas>();

        if (target.renderMode != RenderMode.WorldSpace)
        {
            Debug.Log(this + " ������ �������� ����� 'Render Mode' ���������� ���������� ��� � 'World Space'");
            return;
        }

        RectTransform tr = GetComponent<RectTransform>();
        float size = widthInMeters / tr.sizeDelta.x;
        tr.localScale = new Vector3(size, size, size);

        BoxCollider coll = GetComponent<BoxCollider>();

        if (coll == null)
        {
            BoxCollider box = gameObject.AddComponent<BoxCollider>();
            box.size = new Vector3(tr.sizeDelta.x, tr.sizeDelta.y, 1);
        }
        else
        {
            coll.size = new Vector3(tr.sizeDelta.x, tr.sizeDelta.y, 1);
        }
    }
}
