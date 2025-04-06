using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coin = 0;

    public TextMeshProUGUI coinText;

    public static Coin instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            coin++;
            coinText.text = "coins: " + coin.ToString();
            Debug.Log(coin);
            Destroy(other.gameObject);
        }
    }

    public bool TryBuyHealth(int cost, float healAmount, Movement player)
    {
        if (coin >= cost)
        {
            coin -= cost;
            coinText.text = "coins: " + coin.ToString();

            player.Heal(healAmount); // Llamamos al método Heal en el jugador
            return true;
        }
        return false;
    }
}
