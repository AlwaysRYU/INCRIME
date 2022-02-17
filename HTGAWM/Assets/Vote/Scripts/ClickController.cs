using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Project {
    public class ClickController : MonoBehaviour
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
        private Text VoteText;
        [SerializeField]
        private Button VoteBtn;
        [SerializeField]
        private Text timer;

        private AudioSource musicPlayer;
        public AudioClip handcuffsMusic;
        public AudioClip gameEnd;
        public AudioClip shortTime;
        private int isShortTimeOn;
        private string name;

        private string[] same; // 동표일 때, 누가 있는지

        public GameObject btn_next;
        public Text txt_result;
        bool flag;

        public void nameSet(string name){ 
            this.name = name;
        }

        public string nameGet(){
            return name;
        }

        static private readonly char[] Delimiter = new char[] {':'};

        void Start(){
            KimCheck.SetActive(false);
            MaCheck.SetActive(false);
            ChunCheck.SetActive(false);
            JangCheck.SetActive(false);
            YunCheck.SetActive(false);
            ChoiCheck.SetActive(false);
            ResultVote.SetActive(false);
            btn_next.SetActive(false);
            txt_result.gameObject.SetActive(false);

            isShortTimeOn = 0;
        }

        void HideCheck(){
            KimCheck.SetActive(false);
            MaCheck.SetActive(false);
            ChunCheck.SetActive(false);
            JangCheck.SetActive(false);
            YunCheck.SetActive(false);
            ChoiCheck.SetActive(false);
        }

        public void ChunClick(){
            name = "Chun";
            nameSet(name);
            HideCheck();
            ChunCheck.SetActive(true);
        }

        public void KimClick(){
            name = "Kim";
            nameSet(name);
            HideCheck();
            KimCheck.SetActive(true);
        }

        public void JangClick(){
            name = "Jang";
            nameSet(name);
            HideCheck();
            JangCheck.SetActive(true);
        }

        public void MaClick(){
            name = "Ma";
            nameSet(name);
            HideCheck();
            MaCheck.SetActive(true);
        }

        public void YunClick(){
            name = "Yun";
            nameSet(name);
            HideCheck();
            YunCheck.SetActive(true);
        }

        public void ChoiClick(){
            name = "Choi";
            nameSet(name);
            HideCheck();
            ChoiCheck.SetActive(true);
        }

        public void VoteClick(){
            // emitVote 서버에 보낼 클라이언트 투표 데이터
            string data = nameGet();
            Debug.Log("[System] Client : 캐릭터 투표 server.js로 보내기 " + data);
            VoteText.text = "완료";
            VoteBtn.interactable = false;
//            Application.ExternalCall("socket.emit", "first_vote", data, Client.room, 1 );
            Application.ExternalCall("socket.emit", "PLAY_VOTE", data, Client.room, 1);
        }

        // 투표 시간
        public void VoteTimer(string data)
        {
            var pack = data.Split(Delimiter);
            // Debug.Log(pack[1] + " : " + pack[2]);

            Client.minute = pack[1];
            Client.second = pack[2];

            var timetxt = "";
            timetxt = pack[1] + ":" + pack[2];
            timer.text = timetxt;
            if ( isShortTimeOn == 0 ){
                if( pack[1] == "00" && (pack[2] == "10" || pack[2] == "09") ){
                    isShortTimeOn = 1;
                    musicPlayer = GetComponent<AudioSource>();
                    PlaySound(shortTime, musicPlayer);
                }
            } else if ( isShortTimeOn == 1 ){
                if(pack[2] == "00"){
                    musicPlayer = GetComponent<AudioSource>();
                    musicPlayer.Stop();
                    isShortTimeOn = 0;
                }
            }
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

        public int Compare(string[] vote) // max 투표 수
        {
            int max = 0;
            for(int i = 0; i<6; i++)
            {
                if(max < int.Parse(vote[i]))
                {
                    max = int.Parse(vote[i]);
                }
            }
            return max;
        }

        public bool VoteSame(string[] result) // 동표인지 아닌지 체크
        {
            int max = Compare(result);
            int cnt = 0;
            bool flag = false;
            same = new string[6];
            for (int i = 0; i<6; i++)
            {
                if (max == int.Parse(result[i]))
                {
                    switch (i)
                    {
                        case 0:
                            same[cnt] = "마이사";
                            break;
                        case 1:
                            same[cnt] = "김비서";
                            break;
                        case 2:
                            same[cnt] = "천보안";
                            break;
                        case 3:
                            same[cnt] = "장대행";
                            break;
                        case 4:
                            same[cnt] = "최과장";
                            break;
                        case 5:
                            same[cnt] = "윤사원";
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
                
                if (int.Parse(vote[0]) > 0) // 마이사
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
                if (int.Parse(vote[1]) > 0) // 김비서
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
                if (int.Parse(vote[2]) > 0) // 천보안
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
                if (int.Parse(vote[3]) > 0) // 장대행
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
                if (int.Parse(vote[4]) > 0) // 최과장
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
                if (int.Parse(vote[5]) > 0) // 윤사원
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
                PlaySound(handcuffsMusic, musicPlayer); // 음악 실행
                yield return new WaitForSeconds(1.5f);
            }

            flag = VoteSame(result);
            btn_next.SetActive(true);
        }

        public void onVote(string data){
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

            StartCoroutine(WaitForIt(vote, result)); // 사용자에게 투표 갯수 알려줌

            
            // 15초 뒤에 스토리 결과 출력
            // Invoke("MoveResultStory", 15);

        }

        public void ClickNextBtn()
        {
            btn_next.SetActive(false);
            if (flag == true) // 동표가 나왔을 때
            {
                Debug.Log("동표가 나왔습니다. 재투표를 실행합니다. ");
                Application.ExternalCall("socket.emit", "RESULT_VOTE", same, Client.room, 1);
                //                Application.ExternalCall("socket.emit", "MULTI_RESULT_VOTE", same, Client.room , 1);
                SceneManager.LoadScene("VoteTextResult");
            }
            else // 한명이 최대 득표 일 때
            {
                Debug.Log("한명이 최대 득표 " + same[0]);
                Application.ExternalCall("socket.emit", "RESULT_VOTE", same[0], Client.room, 0);
                //              Application.ExternalCall("socket.emit", "SINGLE_RESULT_VOTE", same[0], Client.room, 0);
                SceneManager.LoadScene("VoteTextResult");
            }

        }
    }
}

