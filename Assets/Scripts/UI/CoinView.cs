using UnityEngine;
using UnityEngine.UI;

public class CoinView : MonoBehaviour
{
    public Text coinText;
    
    public int count;

    private void Update()
    {
        coinText.text = "" + count;
    }
}
