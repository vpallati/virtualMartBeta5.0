using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyCartScript : MonoBehaviour {

    public TextMesh cartIndicatorTextMesh;
    private List<cartProduct> myCartItems;
    int totalQuantity;

    // Use this for initialization

    void Start() {
        myCartItems = new List<cartProduct>();
        cartIndicatorTextMesh.text = "myCartItems(0)";
        totalQuantity = 0;
    }


    public void addProductToCart(string name, float price, int quantity)
    {
        myCartItems.Add(new cartProduct(name, quantity, price));
        totalQuantity += quantity;
        cartIndicatorTextMesh.text = "myCartItems(" + totalQuantity.ToString() + ")";
    }

    // Update is called once per frame
    void Update() {

    }

    private class cartProduct
    {
        public string name;
        public int quantity;
        public float unitPrice;

        public cartProduct(string Name, int Quantity, float UnitPrice)
        {
            name = Name;
            quantity = Quantity;
            unitPrice = UnitPrice;
        }

    }

}
