using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{

    private Animator anim;
    public float ColliderRadius;
    public bool IsOpened;

    public List<Food> Items = new List<Food>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayer();
    }

    void GetPlayer()
    {
        if(!IsOpened)
        {  
                foreach(Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
            {
                if(c.gameObject.CompareTag("Player"))
                {
                    if(Input.GetMouseButtonDown(0))
                    OpenChest();
                }
            }

        }
       
    }

    void OpenChest()
    {
        foreach(Food i in Items)
        {
           i.GetAction();
        }
        anim.SetTrigger("open");
        IsOpened = true;
        
    }
}
