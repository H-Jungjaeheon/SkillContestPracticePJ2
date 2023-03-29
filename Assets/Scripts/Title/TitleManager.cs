using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private int curIndex = 0;

    private Text[] texts; 

    void Update()
    {
        Choose();
    }

    private void Choose()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && curIndex != 0)
        {
            curIndex--;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && curIndex != 2)
        {
            curIndex++;
            //texts[curIndex].fontSize.
        }
    }
}
