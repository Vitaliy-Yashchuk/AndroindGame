using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyItemMoney : MonoBehaviour
{
    public enum Types
    {
        REMOVE_ADS,OPEN_CITY, OPEN_MEGAPOLIS
    }
    public Types type;
    public IAPManager manager;

    public void BuyItem()
    {
        switch (type)
        {
            case Types.REMOVE_ADS:
                manager.BuyAds();
                break;
            case Types.OPEN_CITY:
                manager.BuyOpenCity();
                break;
            case Types.OPEN_MEGAPOLIS:
                manager.BuyOpenMegapolis();
                break;
        }
    }
}
