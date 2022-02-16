using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{

    public class AlibiButton : MonoBehaviour
    {
        // �˸����� ���� ������Ʈ
        public GameObject AlibiPanel;
        public Text Alibi;

        // ���� ��ư Ŭ�� �� �˸����� �г� SetActive
        private bool AlibiCheck = false;

        // Start is called before the first frame update
        void Start()
        {
            AlibiPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ClickAlibiBtn()
        {
            if (AlibiCheck)
            {
                AlibiPanel.SetActive(false);
                AlibiCheck = false;
            }
            else
            {
                AlibiPanel.SetActive(true);
                Alibi.text = Client.alibi.Replace("\\n", "\n"); ;
                AlibiCheck = true;
                Debug.Log(Client.alibi);
            }
        }
    }

}
