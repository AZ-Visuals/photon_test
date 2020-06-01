using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public fields

        public GameObject playerPrefab;
        public GameObject sphere;

        #endregion

        #region Photon callbacks

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

        #region Public methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void ActivateSphereOnButton()
        {
            bool activate = sphere.activeInHierarchy;

            sphere.GetComponent<PhotonView>().RPC("ActivateSphere", RpcTarget.All, !activate);
        }

        #endregion

        #region Private methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Not master");
            }

            Debug.LogFormat("Loading level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        #endregion

        #region Photon Callbacks

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.LogFormat("OnPlayerEnteredRoom : {0}", newPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.LogFormat("OnPlayerLeftRoom : {0}", otherPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadArena();
            }
        }

        #endregion

        #region MonoBehaviour callbacks

        void Start()
        {
            sphere = GameObject.Find("Sphere");

            if (playerPrefab == null)
            {
                Debug.LogError("null player prefab for ", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
                    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

            //if(!GameObject.Find("Sphere").activeInHierarchy)
            if(PhotonNetwork.IsMasterClient)
            {
                    PhotonNetwork.Instantiate(sphere.name, new Vector3(0f, 2f, 10f), Quaternion.identity, 0);
            }
        }

        #endregion
    }
}
