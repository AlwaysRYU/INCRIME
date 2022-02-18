using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isFire;
    Vector3 direction;
    public float speed = 10f;

    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
        // �߻� 10�� �� �ı�
        Destroy(gameObject, 10f);
    }

    void Start()
    {

    }

    void Update()
    {
        // isFire�� True�� ��츸 direction �������� �ʴ� speed �Ÿ� ���ư�����
        if (isFire)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // SimpleCharacterController�� �����ͼ� ������Ʈ�� ��ȿ�ϸ�
        var controller = collision.collider.GetComponent<SimpleCharacterController>();
        if(controller != null)
        {
            // ü�� -1
            controller.hp -= 1;
        }
        // �÷��̾� �ƴϴ��� �ı�
        Destroy(gameObject);
    }
}
