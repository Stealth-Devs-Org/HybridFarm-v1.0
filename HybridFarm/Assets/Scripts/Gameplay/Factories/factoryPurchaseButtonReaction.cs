using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPurchaseButtonReaction : MonoBehaviour
{
    // Reference to MoneyScript
    private MoneyScript moneyScript;

    private FactoryPriceHandler factoryPriceHandler;

    private Objective objective;

    [SerializeField] string nameOfFactory;

    // Reference to the GameObject that represents the button
    public GameObject buttonGameObject;

    public int currentFactoryLevel=0;


    //private string[] choosenFacotryArray;

    //public string nameOfFactory;

    // Cost of the factory
    public int indexOfFactoryAssigned;
    public int CostOfFactory;
    public bool isElectricFactory =  false;


    void Start()
    {
        
        moneyScript = FindObjectOfType<MoneyScript>();  //to find money current value
        factoryPriceHandler = FindObjectOfType<FactoryPriceHandler>();
        objective = FindObjectOfType<Objective>();

        
        if (buttonGameObject != null)
        {
            buttonGameObject.SetActive(false);
        }

        
    }

    void Update()
    {


        string[] factoryNames= new string [] {"EggPowderFactoryFuel","EggPowderFactoryElectric","CakeFactoryFuel","CakeFactoryElectric","MeatCutterFactoryFuel","MeatCutterFactoryElectric","SausagesFactoryFuel","SausagesFactoryElectric","CurdFactory","CurdFactoryFuel","CheeseFactoryFuel","CheeseFactoryElectric"};

        
        // to find which factory was assigned to this script.....
        //indexOfFactoryAssigned =0;
        for (int i = 0; i<=9 ;i++)
        {
            if (nameOfFactory == factoryNames[i])
            {
                indexOfFactoryAssigned = i; // i has index of factory script assigned...
            }
        }
        

        currentFactoryLevel = objective.factoryNamesLevels[indexOfFactoryAssigned]; // get corrosponding current factory level from objective script.



        if (indexOfFactoryAssigned ==0)
        {
            CostOfFactory = factoryPriceHandler.eggPowderfactoryLevelsCost[currentFactoryLevel]; //display factory price on upgrade board based on selected factory...
        }

        else if (indexOfFactoryAssigned ==1)
        {
            CostOfFactory = factoryPriceHandler.eggPowderfactoryLevelsCost[currentFactoryLevel+1];
        }
        
        else if (indexOfFactoryAssigned ==2)
        {
            CostOfFactory = factoryPriceHandler.cakefactoryLevelsCost[currentFactoryLevel];
        }
        
        else if (indexOfFactoryAssigned ==3)
        {
            CostOfFactory = factoryPriceHandler.cakefactoryLevelsCost[currentFactoryLevel+1];
        }
        
        else if (indexOfFactoryAssigned ==4)
        {
            CostOfFactory = factoryPriceHandler.curdfactoryLevelsCost[currentFactoryLevel];
        }

        else if (indexOfFactoryAssigned ==5)
        {
            CostOfFactory = factoryPriceHandler.cheesefactoryLevelsCost[currentFactoryLevel+1];
        }

        else 
        {
            Debug.Log("Not Assigned Factory");
        }




        
        // Check if money value is sufficient to enable the button
        if (moneyScript != null && buttonGameObject != null)
        {
            if (moneyScript.moneyValue >= CostOfFactory)
            {
                buttonGameObject.SetActive(true);
            }
            else
            {
                buttonGameObject.SetActive(false);
            }
        }
    }
}
