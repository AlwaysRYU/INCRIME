using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project
{

    public class AlibiButton : MonoBehaviour
    {
        // �˸����� ���� ������Ʈ
        public GameObject AlibiPanel;
        public Text Alibi;

        // ���� ��ư Ŭ�� �� �˸����� �г� SetActive

        // Start is called before the first frame update
        void Start()
        {
            AlibiPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [SerializeField]
        private TextMeshProUGUI roleName;
        public void ClickAlibiBtn()
        {
            if (AlibiPanel.activeSelf)
            {
                AlibiPanel.SetActive(false);
            }
            else
            {
                AlibiPanel.SetActive(true);
                roleName.text = Client.role;
                if (Client.alibi != null)
                    Alibi.text = Client.alibi.Replace("\\n", "\n");
                Debug.Log(Client.alibi);
            }
        }
    }

}
