using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void Interact()
    {
        Debug.Log("book padhle gandu");
        // Add interaction logic here (e.g., open door, pick up item, etc.)
    }
}
