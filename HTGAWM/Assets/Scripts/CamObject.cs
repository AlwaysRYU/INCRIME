using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;

public class CamObject : MonoBehaviour
{
    [SerializeField]
    private GameObject go_CamsParent;

    private VideoSurface[] surfaces;
    public Dictionary<string, int> surfaceIndexDict = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        surfaces = go_CamsParent.GetComponentsInChildren<VideoSurface>(true);
        Debug.Log(surfaces.Length);
        foreach(VideoSurface vid in surfaces){
            vid.gameObject.SetActive(false);
        }
    }

    public void AddOtherUser(uint uid)
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            if (!surfaces[i].gameObject.activeSelf)
            {
                surfaces[i].videoFps = 30;
                surfaces[i].SetForUser(uid);
                surfaces[i].SetEnable(true);
                surfaces[i].gameObject.SetActive(true);
                surfaceIndexDict.Add(uid.ToString(), i);
                Debug.Log("Agora: �ٸ� ������ ������ ���� �߽��ϴ�.");
                return;
            }
        }
    }

    public void DeleteOtherUser(uint uid)
    {
        string targetString = uid.ToString();

        if (surfaceIndexDict.ContainsKey(targetString)) {
            int targetIndex = surfaceIndexDict[targetString];
            surfaces[targetIndex].SetEnable(false);
            surfaces[targetIndex].gameObject.SetActive(false);
            Debug.Log("Agora: ���� ������ ������ �����߽��ϴ�.");
            return;
        }
        Debug.Log("Agora: ���� ������ ������ ���� �߽��ϴ�.");
    }
}
