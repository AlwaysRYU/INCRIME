using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Project
{
    public class VoteSecondController : MonoBehaviour
    {
        [SerializeField]
        private GameObject KimCheck;
        [SerializeField]
        private GameObject MaCheck;
        [SerializeField]
        private GameObject ChunCheck;
        [SerializeField]
        private GameObject JangCheck;
        [SerializeField]
        private GameObject YunCheck;
        [SerializeField]
        private GameObject ChoiCheck;
        [SerializeField]
        private GameObject ResultVote;
        [SerializeField]
        private GameObject MaHand;
        [SerializeField]
        private GameObject KimHand;
        [SerializeField]
        private GameObject ChunHand;
        [SerializeField]
        private GameObject JangHand;
        [SerializeField]
        private GameObject ChoiHand;
        [SerializeField]
        private GameObject YunHand;

        [SerializeField]
        private GameObject MaBtn;
        [SerializeField]
        private GameObject KimBtn;
        [SerializeField]
        private GameObject ChunBtn;
        [SerializeField]
        private GameObject JangBtn;
        [SerializeField]
        private GameObject ChoiBtn;
        [SerializeField]
        private GameObject YunBtn;
        
        [SerializeField]
        private Text timer;

        [SerializeField]
        private Text VoteText;
        [SerializeField]
        private Button VoteBtn;

        private AudioSource musicPlayer;
        public AudioClip handcuffsMusic;
        public AudioClip gameEnd;

        public GameObject wait_image;

        private string name;

        private string[] same; // ��ǥ�� ��, ���� �ִ���

        public GameObject btn_next;
        public Text txt_result;
        bool flag;

        public void nameSet(string name)
        {
            this.name = name;
        }

        public string nameGet()
        {
            return name;
        }

        static private readonly char[] Delimiter = new char[] { ':' };

        void Start()
        {
            HideCheck();
            ResultVote.SetActive(false);

            btn_next.SetActive(false);
            txt_result.gameObject.SetActive(false);
        }

        void HideCheck()
        {
            KimCheck.SetActive(false);
            MaCheck.SetActive(false);
            ChunCheck.SetActive(false);
            JangCheck.SetActive(false);
            YunCheck.SetActive(false);
            ChoiCheck.SetActive(false);
        }

        public void ChunClick()
        {
            name = "Chun";
            nameSet(name);
            HideCheck();
            ChunCheck.SetActive(true);
        }

        public void KimClick()
        {
            Debug.Log("��� Ŭ��");
            name = "Kim";
            nameSet(name);
            HideCheck();
            KimCheck.SetActive(true);
        }

        public void JangClick()
        {
            name = "Jang";
            nameSet(name);
            HideCheck();
            JangCheck.SetActive(true);
        }

        public void MaClick()
        {
            name = "Ma";
            nameSet(name);
            HideCheck();
            MaCheck.SetActive(true);
        }

        public void YunClick()
        {
            name = "Yun";
            nameSet(name);
            HideCheck();
            YunCheck.SetActive(true);
        }

        public void ChoiClick()
        {
            name = "Choi";
            nameSet(name);
            HideCheck();
            ChoiCheck.SetActive(true);
        }

        public void VoteClick()
        {
            // emitVote ������ ���� Ŭ���̾�Ʈ ��ǥ ������
            string data = nameGet();
            Debug.Log("[System] Client : ĳ���� ��ǥ server.js�� ������ " + data);
            VoteText.text = "�ٸ� �÷��̾ ��ٷ� �ּ���";
            VoteBtn.interactable = false;
//            Application.ExternalCall("socket.emit", "SECOND_VOTE", data);
            Application.ExternalCall("socket.emit", "PLAY_VOTE", data, Client.room, 2);
        }

        public void MoveResultStory()
        {
            PlaySound(gameEnd, musicPlayer);
            SceneManager.LoadScene("ResultPage");
        }

        public void PlaySound(AudioClip clip, AudioSource audioPlayer)
        {
            audioPlayer.Stop();
            audioPlayer.clip = clip;
            audioPlayer.loop = false;
            audioPlayer.time = 0;
            audioPlayer.Play();
        }

        public void PanelHide()
        {
            MaBtn.SetActive(false);
            KimBtn.SetActive(false);
            ChunBtn.SetActive(false);
            JangBtn.SetActive(false);
            ChoiBtn.SetActive(false);
            YunBtn.SetActive(false);
        }

        // ��ǥ �ð�
        public void VoteTimer(string data)
        {
            var pack = data.Split(Delimiter);
            // Debug.Log(pack[1] + " : " + pack[2]);

            Client.minute = pack[1];
            Client.second = pack[2];

            var timetxt = "";
            timetxt = pack[1] + ":" + pack[2];
            timer.text = timetxt;
        }

        public int Compare(string[] vote)
        {
            int max = 0;
            for (int i = 0; i < 6; i++)
            {
                if (max < int.Parse(vote[i]))
                {
                    max = int.Parse(vote[i]);
                }
            }
            return max;
        }

        public bool VoteSame(string[] result)
        {
            int max = Compare(result);
            int cnt = 0;
            bool flag = false;
            same = new string[6];
            for (int i = 0; i < 6; i++)
            {
                if (max == int.Parse(result[i]))
                {
                    switch (i)
                    {
                        case 0:
                            same[cnt] = "���̻�";
                            break;
                        case 1:
                            same[cnt] = "���";
                            break;
                        case 2:
                            same[cnt] = "õ����";
                            break;
                        case 3:
                            same[cnt] = "�����";
                            break;
                        case 4:
                            same[cnt] = "�ְ���";
                            break;
                        case 5:
                            same[cnt] = "�����";
                            break;
                    }
                    cnt++;
                }
            }
            if (cnt > 1)
            {
                flag = true;
            }
            Debug.Log(flag);
            return flag;
        }

        IEnumerator WaitForIt(string[] vote, string[] result)
        {

            int max = Compare(vote);
            musicPlayer = GetComponent<AudioSource>();
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < max; i++)
            {

                if (int.Parse(vote[0]) > 0) // ���̻�
                {
                    if (i == 0)
                    {
                        MaHand.SetActive(true);
                        vote[0] = (int.Parse(vote[0]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(MaHand, MaHand.transform.parent.transform) as GameObject;
                        vote[0] = (int.Parse(vote[0]) - 1).ToString();
                    }
                }
                if (int.Parse(vote[1]) > 0) // ���
                {
                    if (i == 0)
                    {
                        KimHand.SetActive(true);
                        vote[1] = (int.Parse(vote[1]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(KimHand, KimHand.transform.parent.transform) as GameObject;
                        vote[1] = (int.Parse(vote[1]) - 1).ToString();
                    }
                }
                if (int.Parse(vote[2]) > 0) // õ����
                {
                    if (i == 0)
                    {
                        ChunHand.SetActive(true);
                        vote[2] = (int.Parse(vote[2]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(ChunHand, ChunHand.transform.parent.transform) as GameObject;
                        vote[2] = (int.Parse(vote[2]) - 1).ToString();
                    }
                }
                if (int.Parse(vote[3]) > 0) // �����
                {
                    if (i == 0)
                    {
                        JangHand.SetActive(true);
                        vote[3] = (int.Parse(vote[3]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(JangHand, JangHand.transform.parent.transform) as GameObject;
                        vote[3] = (int.Parse(vote[3]) - 1).ToString();
                    }
                }
                if (int.Parse(vote[4]) > 0) // �ְ���
                {
                    if (i == 0)
                    {
                        ChoiHand.SetActive(true);
                        vote[4] = (int.Parse(vote[4]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(ChoiHand, ChoiHand.transform.parent.transform) as GameObject;
                        vote[4] = (int.Parse(vote[4]) - 1).ToString();
                    }
                }
                if (int.Parse(vote[5]) > 0) // �����
                {
                    if (i == 0)
                    {
                        YunHand.SetActive(true);
                        vote[5] = (int.Parse(vote[5]) - 1).ToString();
                    }
                    else
                    {
                        GameObject ins = Instantiate(YunHand, YunHand.transform.parent.transform) as GameObject;
                        vote[5] = (int.Parse(vote[5]) - 1).ToString();
                    }

                }
                PlaySound(handcuffsMusic, musicPlayer); // ���� ����
                yield return new WaitForSeconds(1.5f);
            }


            btn_next.SetActive(true);
            flag = VoteSame(result);
            
        }

        public void onVote(string data)
        {
            ResultVote.SetActive(true);
            txt_result.gameObject.SetActive(true);

            MaHand.SetActive(false);
            KimHand.SetActive(false);
            ChunHand.SetActive(false);
            JangHand.SetActive(false);
            ChoiHand.SetActive(false);
            YunHand.SetActive(false);

            var vote = data.Split(Delimiter);
            var result = data.Split(Delimiter);

            StartCoroutine(WaitForIt(vote, result)); // ����ڿ��� ��ǥ ���� �˷���


            // 15�� �ڿ� ���丮 ��� ���
            // Invoke("MoveResultStory", 15);

        }

        public void SecondVote(string data)
        {
            wait_image.SetActive(false);
            Debug.Log("�ι�° ��ǥ" + data);
            PanelHide();
            var again = data.Split(Delimiter);
            for (int i = 0; i < again.Length; i++)
            {
                if (again[i] == "���̻�")
                {
                    MaBtn.SetActive(true);
                }
                else if (again[i] == "���")
                {
                    KimBtn.SetActive(true);
                }
                else if (again[i] == "õ����")
                {
                    ChunBtn.SetActive(true);
                }
                else if (again[i] == "�����")
                {
                    JangBtn.SetActive(true);
                }
                else if (again[i] == "�ְ���")
                {
                    ChoiBtn.SetActive(true);
                }
                else if (again[i] == "�����")
                {
                    YunBtn.SetActive(true);
                }
            }
        }
        public void ClickNextBtn()
        {
            btn_next.SetActive(false);
            if (flag == true) // ��ǥ�� ������ ��
            {
                Debug.Log("�ι�° ��ǥ���� ��ǥ�� ����");
                Application.ExternalCall("socket.emit", "RESULT_SECOND_VOTE", same, Client.room ,1);
                SceneManager.LoadScene("VoteTextResult");
            }
            else // �Ѹ��� �ִ� ��ǥ �� ��
            {
                Debug.Log("�ι�° ��ǥ���� �Ѹ��� �ִ� ��ǥ " + same[0]);
                Application.ExternalCall("socket.emit", "RESULT_SECOND_VOTE", same[0], Client.room, 0);
                SceneManager.LoadScene("VoteTextResult");
            }

        }
    }
}