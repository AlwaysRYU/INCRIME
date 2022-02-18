using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var controller = other.gameObject.GetComponent<SimpleCharacterController>();
        // ������ �浹���� ��쿡�� True
        // �ٸ� ������Ʈ�� null
        if(controller != null)
        {
            controller.score += 1;
            Destroy(gameObject);
        }
    }

}
