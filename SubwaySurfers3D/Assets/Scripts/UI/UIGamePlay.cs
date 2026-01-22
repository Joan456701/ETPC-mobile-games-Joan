using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private Image normalCoin;
    [SerializeField] private Image doubleCoin;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI metersText;

    private PlayerController pController;

    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        pController = obj.GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateCoinCount();
        if (pController.doubleCoins == false)
        {
            normalCoin.enabled = true;
            doubleCoin.enabled = false;
        }
        else
        {
            normalCoin.enabled = false;
            doubleCoin.enabled = true;
        }

        metersText.text = pController.aroundDistance.ToString();
    }

    private void UpdateCoinCount()
    {
        coinsText.text = pController.coinCount.ToString();
    }
}
