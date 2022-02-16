using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class typingeffect : MonoBehaviour
{
    public Text tx;
    private string m_text = "";
    private AudioSource musicPlayer;
    public AudioClip keyboard;
    static private readonly char[] Delimiter = new char[] { ':' };
    public GameObject ReVoteBtn;
    public Text vote_desc;

    // ����
    static string[] votearr;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        ReVoteBtn.SetActive(false);
        vote_desc.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // second multi vote
    IEnumerator _second_multi_typing(string m_text, string[] data)
    {
        yield return new WaitForSeconds(1f);
        PlaySound(keyboard, musicPlayer);
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        MoveResultStory();
    }

    // multi vote
    IEnumerator _multi_typing(string m_text, string[] data)
    {
        yield return new WaitForSeconds(1f);
        PlaySound(keyboard, musicPlayer);
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        musicPlayer.Stop();
        MoveSecondVote(data);
    }

    // single vote
    IEnumerator _typing(string m_text)
    {
        yield return new WaitForSeconds(1f);
        PlaySound(keyboard, musicPlayer);
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        musicPlayer.Stop();
        MoveResultStory();
    }

    public void VoteTextResult(string data)
    {
        Debug.Log("���� --> " + data + " : " + Client.role + " player ID " + Client.name );
        // TODO 
        if ( Client.role.Equals("���") && !data.Equals("���") )
        {
            Debug.Log("�������� �¸��߽��ϴ�. --> " + data + " : " + Client.role);
            Client.win = 1;
        }
        if ( !Client.role.Equals("���") && data.Equals("���") )
        {
            Debug.Log("������ �����߽��ϴ�. --> " + data + " : " + Client.role);
            Client.win = 1;
        }
        m_text = data + "�� �������� ����Ǿ����ϴ�.";
        StartCoroutine(_typing(m_text));

        
    }

    public void MultiVoteTextResult(string data)
    {
        Debug.Log("��Ƽ����" + data);
        var pack = data.Split(Delimiter);
        for (int i = 0; i < pack.Length; i++)
        {
            m_text += pack[i] + " ";
        }
        m_text += "��ǥ�� ����ǥ �մϴ�.";
        StartCoroutine(_multi_typing(m_text, pack));
    }

    public void MultiSecondVoteTextResult(string data)
    {
        Debug.Log("��Ƽ����" + data);
        var pack = data.Split(Delimiter);
        for (int i = 0; i < pack.Length; i++)
        {
            m_text += pack[i] + " ";
        }
        m_text += "��ǥ�� ���Խ��ϴ�. \n ������ �̰���ϴ�.";
        if (Client.role.Equals("���") )
        {
            Debug.Log("�������� �¸��߽��ϴ�. --> " + data + " : " + Client.role);
            Client.win = 1;
        }
        StartCoroutine(_second_multi_typing(m_text, pack));
    }

    public void MoveResultStory()
    {
        SceneManager.LoadScene("ResultPage");
    }

    public void MoveSecondVote(string[] arrr)
    {
        votearr = ( string[]) arrr.Clone();
        ReVoteBtn.SetActive(true);
        vote_desc.gameObject.SetActive(true);
        Debug.Log("[system] 2�� ��ǥ�� �ʿ��մϴ�. " + arrr[0] + arrr[1]);
    }
    
    public void PlaySound(AudioClip clip, AudioSource audioPlayer)
    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.loop = false;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }

    public void StopSound(AudioClip clip, AudioSource audioPlayer)
    {
        audioPlayer.Stop();
    }

    public void ClickReVoteBtn()
    {
        Application.ExternalCall("socket.emit", "MOVE_SECOND_VOTE", votearr, Client.room);
        SceneManager.LoadScene("VoteSecondScene");
    }
}
