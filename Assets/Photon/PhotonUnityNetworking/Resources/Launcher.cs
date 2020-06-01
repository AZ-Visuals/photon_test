using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private serializable

        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private GameObject progressLabel;

        #endregion

        #region Private

        string gameVersion = "1";

        bool isConnecting;

        #endregion


        #region MonoBehaviour callbacks

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            
            //Connect();
        }

        #endregion

        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        #region MonoBehaviourPunCallbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedCalled");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("Disconnected cause: {0}", cause);

            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("JoinRandomFailed");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");

                PhotonNetwork.LoadLevel("Room for 1");
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Debug.Log("We load the 'Room for 1' ");

                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        #endregion
    }
}
