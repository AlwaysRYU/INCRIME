using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameInfo;

public class AgoraController : MonoBehaviour
{
    private static AgoraController agoraController = null; // �̱���

    public CamObject camObject;
    static TestHelloUnityVideo app;      // ������

    private CanvasGroup camCanvasGroup;

    void Awake()
    {
        // �̱���
        if (agoraController == null)
        {
            agoraController = this;
            
            DontDestroyOnLoad(this.gameObject);
            if (ReferenceEquals(app, null) && !ReferenceEquals(camObject, null))
            {
                Debug.Log("ķ �ν��Ͻ� ����");
                app = new TestHelloUnityVideo(camObject); // create app
                app.loadEngine("b16baf20b1fc49e99bd375ad30d5e340");
            }
            // ���� ���̶� ä�η� ������
            app.join(Client.room + "MeetingScene", true);
            camCanvasGroup = camObject.gameObject.GetComponent<CanvasGroup>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static AgoraController GetAgoraControllerInstance()
    {
        return agoraController;
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    void CamShow()
    {
        camCanvasGroup.alpha = 1;
        camCanvasGroup.interactable = true;
        camCanvasGroup.blocksRaycasts = true;
    }
    void CamHide()
    {
        camCanvasGroup.alpha = 0;
        camCanvasGroup.interactable = false;
        camCanvasGroup.blocksRaycasts = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + "�ư��� �����մϴ�.");
        if(app != null)
        {
            if (GameRoomInfo.agoraVideoScene.Contains(scene.name))
            {
                app.leave();
                JoinAgoraRoom(GameRoomInfo.roomNo + scene.name, true);
                CamShow();
            }
            else if (GameRoomInfo.agoraKeepScene.Contains(scene.name))
            {
                //����: �׳� �ư���� ���� ���¸� �����ϱ� ������
            }
            else
            {
                app.leave();
                CamHide();
            }
        }
        
    }

    public void JoinAgoraRoom(string joinRoomID, bool audioFlag)
    {
        if (app != null)
            app.join(joinRoomID, audioFlag);
    }

    //�ư�� ������ �޼���
    public void LeaveAgoraRoom()
    {
        // �ư�� ������ �׽�Ʈ
        if(app != null)
            app.leave(); // leave channel
    }

    
}
