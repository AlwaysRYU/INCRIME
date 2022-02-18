using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // ��ư ����ϹǷ�.

public class GameHint : MonoBehaviour
{
    public GameObject Hint; // ��Ʈ �̹���
    public Button btnHint;  // ��Ʈ ���� ��ư

    // Start is called before the first frame update
    void Start()
    {
        // ������ ������ ��Ʈ ������ �ʰ� ����
        Hint.SetActive(false);

        // ��Ʈ ��ư Ŭ������ �� ShowHint �Լ� ����
        btnHint.onClick.AddListener(ShowHint);
    }

    void ShowHint()
    {
        // ��Ʈ�� ���̰� �Ѵ�
        Hint.SetActive(true);
        // 2�� �ڿ� ��Ʈ�� ������� �Ѵ�. (Invoke�� ù ��° ���ڴ� �Լ���)
        Invoke("HideHint", 3);
    }

    // �Լ����� �ʿ��ϱ� ������ HideHint��� �Լ��� �ϳ� �����д�.
    void HideHint()
    {
        Hint.SetActive(false);
    }
}