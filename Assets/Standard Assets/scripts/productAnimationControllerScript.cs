using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.UI;

public class productAnimationControllerScript : MonoBehaviour {
    public float maxRayDistance;
    public Animator anim;
    public Animator animSortPrice;
    public Animator animChangeColor;
    public GameObject POC_GO;

    public string selectAnimationButton;
    public string moreInfoButton;
    public string subQuantityButton;
    public string AddQuantityButton;
    public string AddToCartButton;
    public string showCartButton;
    public GameObject cartGameObject;

    public GameObject[] ratingStars;
    public Sprite[] starTextureSprites;
    productProperties producePropertiesScript;
    public GameObject priceText_GO;
    public GameObject quantityText_GO;
    public GameObject rightInfoSprite_GO;
    public GameObject moreInfoSprite_GO;


    public TextMesh cartText;
    public TextMesh cartPriceText;
    private float cartPrice;

    public MyCartScript CartScript;
    private string selectedProductName;
    private float selectedProductPrice;

    public Transform laserOrigin;
    private LineRenderer lineRend;

    bool isSelectKeyDown;
    bool isProductSelected;
    private Color startcolor;
    private TextMesh priceText;
    private TextMesh quantityText;
    int productQuantity;

    private bool cartIsActive = false;


    // Use this for initialization
    void Start()
    {
        
        isSelectKeyDown = false;
        isProductSelected = false;
        priceText = priceText_GO.GetComponent<TextMesh>();
        productQuantity = 1;
        quantityText = quantityText_GO.GetComponent<TextMesh>();


        lineRend = gameObject.GetComponent<LineRenderer>();

        cartText.text = "Quantity - Price - ProductName";

        cartPrice = 0;
        cartPriceText.text = cartPrice.ToString("C");
    }


    // Update is called once per frame
    void Update()
    {


        #region selecting product
        // user select button is down , hilight the user selected objects
        if (CrossPlatformInputManager.GetButton(selectAnimationButton))
        {
            isSelectKeyDown = true;


            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;


            Debug.DrawLine(transform.position, transform.position + transform.forward * maxRayDistance, Color.red);


            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {

                // draw the laser
                lineRend.SetPosition(0, laserOrigin.position);
                lineRend.SetPosition(1, hit.point);


                // we hit the product, hilight the product
                if (hit.transform.gameObject.tag == "product")
                {
                    Debug.DrawLine(hit.point, hit.point + hit.transform.up * 5, Color.blue);
                }

                // we hit the product, hilight the product
                if (hit.transform.gameObject.tag == "sortAnim")
                {
                    animSortPrice.SetTrigger("sortPriceSET");



                }

                if (hit.transform.gameObject.tag == "colorChange")
                {
                    animChangeColor.SetInteger("intchangeColor", 1);



                }
            }
        }

        // open menu as user selected the object
        if (CrossPlatformInputManager.GetButtonUp(selectAnimationButton))
        {
            lineRend.SetPosition(0, laserOrigin.position);
            lineRend.SetPosition(1, laserOrigin.position);
            isSelectKeyDown = true;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            Debug.DrawLine(transform.position, transform.position + transform.forward * maxRayDistance, Color.red);


            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {

                if (hit.transform.gameObject.tag == "product")
                {
                    Debug.DrawLine(hit.point, hit.point + hit.transform.up * 5, Color.blue);


                    // on select update Product properties
                    producePropertiesScript = hit.transform.GetComponent<productProperties>();
                    selectedProductName = producePropertiesScript.name;
                    selectedProductPrice = producePropertiesScript.price;
                    productQuantity = 0;
                    quantityText.text = productQuantity.ToString();


                    // Update rating
                    for (int i = 0; i < producePropertiesScript.rating; i++)
                        ratingStars[i].GetComponent<SpriteRenderer>().sprite = starTextureSprites[0];
                    for (int i = producePropertiesScript.rating; i < 5; i++)
                        ratingStars[i].GetComponent<SpriteRenderer>().sprite = starTextureSprites[1];

                    // Update Price
                    priceText.text = producePropertiesScript.price.ToString("c2");

                    // update right info sprite and more info sprite
                    rightInfoSprite_GO.GetComponent<SpriteRenderer>().sprite = producePropertiesScript.rightImgSprite;
                    moreInfoSprite_GO.GetComponent<SpriteRenderer>().sprite = producePropertiesScript.moreInfoSprite;


                    // we hit product play, shjow properties and move POC
                    POC_GO.transform.position = hit.point;
                    anim.SetTrigger("selectAnimHash");
                    //rotate POC to User face

                    Vector3 directionToLook = POC_GO.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(directionToLook);
                    POC_GO.transform.rotation = rotation;


                    POC_GO.SetActive(true);

                    isProductSelected = true;
                    // Debug.Log("product selected");


                }
            }
        }
        #endregion

        #region if product selected
        if (isProductSelected)
        {
            // deselect product if out of range
            if ((Vector3.Magnitude((gameObject.transform.position - POC_GO.transform.position))) > maxRayDistance)
            {
                // Debug.Log("product selected for deletiojn = " + ((Vector3.Magnitude((gameObject.transform.position - POC_GO.transform.position)))).ToString());
                isProductSelected = false;
                POC_GO.SetActive(false);

                productQuantity = 1;
            }

            // read fore more button
            if (CrossPlatformInputManager.GetButtonDown(moreInfoButton))
            {
                anim.SetTrigger("extraInfoAnimHash");
            }

            // quantity subtract button
            if (CrossPlatformInputManager.GetButtonDown(subQuantityButton))
            {

                if (productQuantity > 1)
                {
                    productQuantity--;
                    quantityText.text = productQuantity.ToString();
                }


                anim.SetTrigger("SubQuantityAnimHash");
            }
            // quantity add button
            if (CrossPlatformInputManager.GetButtonDown(AddQuantityButton))
            {
                productQuantity++;
                quantityText.text = productQuantity.ToString();

                anim.SetTrigger("AddQuantityAnimHash");
            }

            // add to cart button
            if (CrossPlatformInputManager.GetButtonDown(AddToCartButton))
            {
                anim.SetTrigger("AddToCardAnimHash");




                // updaTE CART
                cartText.text += productQuantity.ToString() + " - " + (selectedProductPrice * productQuantity).ToString("C") +  " - " +selectedProductName +  "\n";
                

                cartPrice += selectedProductPrice * productQuantity;
                cartPriceText.text = cartPrice.ToString("C");

                CartScript.addProductToCart(selectedProductName, selectedProductPrice, productQuantity);
            }

        }
        #endregion







        // cart button 
        if (CrossPlatformInputManager.GetButtonDown(showCartButton))
        {
            if(cartIsActive)
            {
                cartGameObject.SetActive(false);
                cartIsActive = false;
            }
            else
            {
                cartGameObject.SetActive(true);
                cartIsActive = true;
            }
            
        }
    }
}
