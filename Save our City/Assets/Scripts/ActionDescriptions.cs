using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDescriptions : MonoBehaviour
{
    public int[][] AP_Costs = {
    	new int[] {3, 2, 3, 2, 3, 5},
    	new int[] {1, 5, 2, 4, 4, 8},
    	new int[] {2, 2, 2, 3, 0, 4},//
    	new int[] {5, 2, 3, 3, 0, 4},//
    	new int[] {2, 3, 3, 2, 2, 5},
    	new int[] {3, 5, 1, 1, 2, 4},
    	new int[] {2, 3, 3, 2, 3, 4},
    	new int[] {2, 4, 3, 3, 3, 5}
    };
    //0 = reveal(g); 2 = reduce; 4 = cap; 6 = empower; 8 = reveal(m)
    //even = level 1; odd = level 2
    public int[][] Action_Categories = {
    	new int[] {0, 2, 2, 2, 2, 4},
    	new int[] {0, 1, 8, 8, 9, 40},
    	new int[] {0, 2, 2, 2, 6, 4},
    	new int[] {19, 2, 2, 6, 0/**/, 4},//
    	new int[] {0, 9, 2, 6, 6, 4},
    	new int[] {1, 9, 2, 2, 2, 4},
    	new int[] {8, 8, 2, 2, 6, 4},
    	new int[] {0, 9, 2, 2, 6, 4}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
