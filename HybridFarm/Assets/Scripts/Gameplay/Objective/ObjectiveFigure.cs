
// This scripts maintain increment/decrement of current collectables during gameplay

using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Collections;

public class ObjectiveFigure : MonoBehaviour
{
    Objective objective;
    public GameObject[] ObjectiveSelectedprefabs;   //this array will have sprites image of objectives 

    public int[] NumberinIndexPostioninObjectiveItems;  // amount at index position of objectives items in ITEMs intiated in obejctive scripts.
    public int[] inIndexPostioninObjectiveItems;  // index of three objective among item array.
    public bool?[] Green_Correct_Indicators;  //  three element with null.
    private int flag =0;

    private bool a =false;

  


    void Start()
    {

        objective = FindObjectOfType<Objective>();
        ObjectiveSelectedprefabs = new GameObject[] {null,null,null};


        NumberinIndexPostioninObjectiveItems= new int[] {0,0,0}; //value of item objective not index  ; see objective scripts.
        inIndexPostioninObjectiveItems= new int[] {13,13,13};    // index value, if index= 13 no objective assined to it. Since we have 12 type of objective.

        



        for (int i = 0; i < objective.objective_items.Length; i++)
        {
            
            if (objective.objective_items[i]>0)    //to check it is an objective or not?
            {
                inIndexPostioninObjectiveItems[flag]=i;
                NumberinIndexPostioninObjectiveItems[flag]=objective.objective_items[i];
                ObjectiveSelectedprefabs[flag]=objective.respectiveItemSprites[i];  // see objective scripts.
                flag++;
            }
        }


        //Debug.Log (inIndexPostioninObjectiveItems[2]);

        

            // Instantiate and position item 1
        Vector3 spawnPosition1 = new Vector3(6.05006402f, -3.85f, 16f);
        if (ObjectiveSelectedprefabs[0])
            {
                GameObject objectiveitem1 = Instantiate(ObjectiveSelectedprefabs[0], spawnPosition1, Quaternion.identity);
            }
                
                 
        // Instantiate and position item 2
        Vector3 spawnPosition2 = new Vector3(6.0516402f, -3.85f, 16f) + new Vector3(1.1f * 1, 0, 0);
        if (ObjectiveSelectedprefabs[1])
            {
                GameObject objectiveitem2 = Instantiate(ObjectiveSelectedprefabs[1], spawnPosition2, Quaternion.identity);
            }
                
            // Instantiate and position item 3
        Vector3 spawnPosition3 = new Vector3(6.116402f, -3.85f, 16f) + new Vector3(1.1f * 2, 0, 0);
        if (ObjectiveSelectedprefabs[2])
            {
                GameObject objectiveitem3 = Instantiate(ObjectiveSelectedprefabs[2], spawnPosition3, Quaternion.identity);
            }

        Green_Correct_Indicators = new bool?[] { null, null, null };

                
    }

    void Update()
    {
    
        StartCoroutine(GreenCorrectIndicatorBool()) ;

    }


    private IEnumerator GreenCorrectIndicatorBool()  //to display green correct mark when an objective is completed.
    {
        yield return new WaitForSeconds(0.02f); // Adjust the delay time as needed
        
        for (int i=0; i< Green_Correct_Indicators.Length; i++)
        {
            //Debug.Log (objectiveFigure.inIndexPostioninObjectiveItems[i]);
            
            if (inIndexPostioninObjectiveItems[i]<13 || Green_Correct_Indicators[i]== true )
            {
                if (objective.collected_itemsIncrements[inIndexPostioninObjectiveItems[i]]>= objective.objective_items[inIndexPostioninObjectiveItems[i]] )
                {
                    Green_Correct_Indicators[i]= true;
                    a=true;

                }
                else
                {
                    if (!a) {
                    Green_Correct_Indicators[i]= false;
                    }
                }

            }



        }

        //Debug.Log(objective.collected_items[inIndexPostioninObjectiveItems[0]]);
        //Debug.Log(objective.collected_items[inIndexPostioninObjectiveItems[1]]);
        //Debug.Log(objective.collected_items[inIndexPostioninObjectiveItems[2]]);

        //Debug.Log(objective.items[inIndexPostioninObjectiveItems[0]]);
        //Debug.Log(Green_Correct_Indicators[0]);
        //Debug.Log(Green_Correct_Indicators[1]);
       
    }
}
