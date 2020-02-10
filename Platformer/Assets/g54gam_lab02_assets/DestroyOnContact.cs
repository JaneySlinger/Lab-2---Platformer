using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        if(playerObject == null){
            Debug.Log("Cannot find the 'Player' object");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject == playerObject){
            Application.LoadLevel(Application.loadedLevel);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
