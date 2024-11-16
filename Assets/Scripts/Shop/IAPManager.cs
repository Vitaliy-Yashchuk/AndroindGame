using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController;

    // ID ваших товаров
    public const string REMOVE_ADS = "remove_ads";
    public const string OPEN_CITY = "open_city";
    public const string OPEN_MEGAPOLIS = "open_megapolis";
    public BuyMapCoins shopController;
    public GameObject adsObject;
    public string enviroment = "testing";
    
    async void Start()
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(enviroment);
            await UnityServices.InitializeAsync(options);
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(REMOVE_ADS, ProductType.NonConsumable);
            builder.AddProduct(OPEN_CITY, ProductType.NonConsumable);
            builder.AddProduct(OPEN_MEGAPOLIS, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);  
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Это метод для покупки чего-либо
    public void BuyAds()
    {
        m_StoreController.InitiatePurchase(REMOVE_ADS);
    }
    public void BuyOpenCity()
    {
        m_StoreController.InitiatePurchase(OPEN_CITY);
    }
    public void BuyOpenMegapolis()
    {
        m_StoreController.InitiatePurchase(OPEN_MEGAPOLIS);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Ініціализація завершена");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Ініціализація помилка. Причина: {error}.";

        if (message != null)
        {
            errorMessage += $" Больше деталей: {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Получаем купленный товар
        var product = args.purchasedProduct;

        // Проверяем id купленного товара
        if (product.definition.id == REMOVE_ADS)
        {
            PlayerPrefs.SetString("NoAds","Yes");
            Destroy(adsObject);
        }
        else if (product.definition.id == OPEN_CITY)
        {
            PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins")+1000);
            shopController.BuyNewMap(1000);
        }
        else if (product.definition.id == OPEN_MEGAPOLIS)
        {
            PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins")+5000);
            shopController.BuyNewMap(5000);
        }

        Debug.Log($"Купівля завершена. Товар: {product.definition.id}");
        
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Купівля помилка - Товар: '{product.definition.id}', Причина: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Купівля помилка - Товар: '{product.definition.id}'," +
            $" Причина: {failureDescription.reason}," +
            $" Більше інформації: {failureDescription.message}");
    }
}
