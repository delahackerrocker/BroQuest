using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ChatUI : MonoBehaviourPun
{
    public TextMeshProUGUI chatThread;
    public TMP_InputField chatInput;

    public static ChatUI instance;

    private void Awake()
    {
        instance = this;
        chatInput.text = "";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EventSystem.current.currentSelectedGameObject == chatInput.gameObject)
            {
                OnChatInputSend();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(chatInput.gameObject);
            }
        }
    }

    public void OnChatInputSend()
    {
        if (chatInput.text.Length > 0)
        {
            photonView.RPC("Thread", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, chatInput.text);
            chatInput.text = "";
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    [PunRPC]
    void Thread(string playerName, string message)
    {
        chatThread.text += string.Format("<b>{0}:</b> {1}\n", playerName, message);
        chatThread.rectTransform.sizeDelta = new Vector2(chatThread.rectTransform.sizeDelta.x, chatThread.mesh.bounds.size.y+20);
    }
}
