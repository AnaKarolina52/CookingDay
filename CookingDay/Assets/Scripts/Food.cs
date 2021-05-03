using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Item", menuName = "Inverntory/Item" )]
public class Food : ScriptableObject
{
   public Sprite icon;
   public string Name;
   public float Value;

   [System.Serializable] 
   public enum Type 
   {
      Vegetable,
      Fruits,
      Protein
   }

   public Type FoodType;

   public void GetAction()
   {
       switch(FoodType)
       {
           case Type.Vegetable:
           Debug.Log("Heath + " + Value);
           break;

           case Type.Fruits:
           Debug.Log("Heath + " + Value);
           break;

           case Type.Protein:
           Debug.Log("Heath + " + Value);
           break;
       }
       
       GameController.instance.GameCompleted();
       FindObjectOfType<AudioManager>().Play("VictoryMusic");

   }
}
