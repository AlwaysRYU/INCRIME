using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Proof proof;   // ȹ���� ����

    public Image proofImage; // ������ �̹���



    private void SetColor(float _alpha)
    {
        Color color = proofImage.color;
        color.a = _alpha;
        proofImage.color = color;
    }


    //���� �߰�
    public void AddProof(Proof _proof)
    {
        proof = _proof;
        proofImage.sprite = _proof.proofImage;

        SetColor(1);

    }

    //���Կ� ���� �����
    private void RemoveProof()
    {
        proof = null;
        proofImage.sprite = null;
        SetColor(0);
    }


   
}
