using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace MyGame
{
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputScript : MonoBehaviour
    {
        #region Private constants

        const string playerNamePrefKey = "PlayerName";

        #endregion

        #region Monobehaviour callbacks

        void Start()
        {
            string defaultName = string.Empty;

            InputField inputField = this.GetComponent<InputField>();

            if (inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    inputField.text = defaultName; 
                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        #endregion

        #region Public methods

        public void SetPlayerName (string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("null name");
                return;
            }

            PhotonNetwork.NickName = name;

            PlayerPrefs.SetString(playerNamePrefKey, name);
        }

        #endregion
    }
}