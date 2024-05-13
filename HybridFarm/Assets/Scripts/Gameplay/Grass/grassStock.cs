using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class grassStock : MonoBehaviour
{
    
    grassSpawnDestroy grassSpawnDestroy;
    bool canrestock =true;
    

    void Start()
    {
        // Find the grassSpawnDestroy object in the scene
        grassSpawnDestroy = FindObjectOfType<grassSpawnDestroy>();
        

        if (grassSpawnDestroy == null)
        {
            Debug.LogError("grassSpawnDestroy not found in the scene!");
        }
    }

    public void TaskOnClick()
    {
        // This function will be called when the button is clicked
        //Debug.Log("Button clicked!");
        if (canrestock)
        {
        StartCoroutine(makenumberofGrassplanttoZero());
        canrestock=false;
        }

        else 
        {
            StopCoroutine(makenumberofGrassplanttoZero());
        }

        StartCoroutine(makerestockCooldown());
        
        // Add your code here that you want to execute when the button is pressed
    }

    private IEnumerator makenumberofGrassplanttoZero()
    {
        
        yield return new WaitForSeconds(5);
        grassSpawnDestroy.numberOfGrassplant = 0 ;

    }


    IEnumerator makerestockCooldown()
    {
        canrestock = false;
        yield return new WaitForSeconds(5);
        canrestock = true;
    }
}
