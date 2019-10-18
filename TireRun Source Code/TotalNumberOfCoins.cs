using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotalNumberOfCoins : MonoBehaviour
{
    public TMP_Text coinNumber;
    void Update()
    {
        coinNumber.text = PlayerPrefs.GetInt("NumberOfCoins").ToString();
    }

}
