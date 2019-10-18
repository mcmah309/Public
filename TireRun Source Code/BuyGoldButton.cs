using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGoldButton : MonoBehaviour
{
    MYIAPManager IAPManager;
    public enum ItemType
    {
        Gold1000,
        Gold10000,
        Gold100000
    }
    public ItemType itemType;
    //public Text priceText;
   // private string defaultText;

    private void Start()
    {
        IAPManager = FindObjectOfType<MYIAPManager>();
       // defaultText = priceText.text;
        //StartCoroutine(LoadPriceRoutine());
    }

    public void ClickBuy()
    {
        switch (itemType)
        {
            case ItemType.Gold1000:
                IAPManager.Buy1000Gold();
                break;
            case ItemType.Gold10000:
                IAPManager.Buy10000Gold();
                break;
            case ItemType.Gold100000:
                IAPManager.Buy100000Gold();
                break;
        }
    }
   /* private IEnumerator LoadPriceRoutine()
    {
        while (!IAPManager.IsInitialized())
        {
            yield return null;
        }
        string loadedPrice = "";
        switch (itemType)
        {
            case ItemType.Gold1000:
                loadedPrice = IAPManager.GetProductPriceFromStore(IAPManager.Gold1000);
                break;
            case ItemType.Gold10000:
                loadedPrice = IAPManager.GetProductPriceFromStore(IAPManager.Gold10000);

                break;
            case ItemType.Gold100000:
                loadedPrice = IAPManager.GetProductPriceFromStore(IAPManager.Gold100000);

                break;
        }
        //priceText.text = defaultText + " " + loadedPrice;
    }*/
}
