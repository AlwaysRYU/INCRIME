using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Proof defaultProof;
    public Proof.ProofJson proof;   // ȹ���� ����

    public RawImage proofRawImage; // ������ �̹���


    void Start()
    {
        if(defaultProof != null)
            proof = new Proof.ProofJson(defaultProof);
    }

    private void SetColor(float _alpha)
    {
        Color color = proofRawImage.color;
        color.a = _alpha;
        proofRawImage.color = color;
    }


    //���� �߰�
    public void AddProof(Proof _proof)
    {
        proof = new Proof.ProofJson(_proof);

        proofRawImage.texture = _proof.proofTexture;

        SetColor(1);
    }

    //���Կ� ���� �����
    private void RemoveProof()
    {
        proof = null;
        proofRawImage.texture = null;
        SetColor(0);
    }


   
}
