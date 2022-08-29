using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeerScript : MonoBehaviour
{
    [SerializeField] int _beerCount = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            Debug.Log("Test");
            GameObject.Destroy(gameObject);
        }
            
    }
}
