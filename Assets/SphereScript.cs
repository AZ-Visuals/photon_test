using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class SphereScript : MonoBehaviour
{
    [PunRPC]
    void ActivateSphere(bool activate)
    {
        transform.position += new Vector3(0.5f, 0, 0);
    }

    void Awake()
    {
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
}
