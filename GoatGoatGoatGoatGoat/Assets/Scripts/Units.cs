using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour {
    public static string format = "F1";

	public static string readableText(float weight)
    {

        if(weight < .5f)
        {
            return (weight * 1000).ToString(format) + "g";
        }
        else if(weight < 700)
        {
            return weight.ToString(format) + "kg";
        }
        else if(weight < 700*1000)
        {
            return (weight / 1000/1000).ToString(format) + "kt";
        }
        return "...";
    }
}
