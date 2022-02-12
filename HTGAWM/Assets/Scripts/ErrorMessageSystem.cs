using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageSystem : MonoBehaviour
{
    public static ErrorMessageSystem instance;

    [SerializeField]
    GameObject errorBoxObject;

    [SerializeField]
    Text errorMessageText;

    // Start is called before the first frame update
    void Start()
    {
        // �̱� �� �ڵ�
        if (instance == null)
        {
            // 
            // ���� �ٸ� ���� ������ ������Ʈ�� �ı����� ����
            DontDestroyOnLoad(this.gameObject);
            instance = this;// define the class as a static variable
        }
        else
        {
            //it destroys the class if already other class exists
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRecieveErrorMessage(string data)
    {
        errorMessageText.text = data;
        errorBoxObject.SetActive(true);
    }

    public void OnClickCloseErrorBox()
    {
        errorBoxObject.SetActive(false);
    }
}
