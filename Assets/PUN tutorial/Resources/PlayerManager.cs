using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

namespace MyGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {

        #region Public fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region MonoBehaviour callbacks

        void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this.gameObject;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}
