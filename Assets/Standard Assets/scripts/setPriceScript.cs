using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setPriceScript : MonoBehaviour {


    private Text priceText;

    public void setPriceTO(float price)
    {
        // Update Price
        priceText = gameObject.GetComponent<Text>();
        priceText.text = "$" + price.ToString();

    }
}
