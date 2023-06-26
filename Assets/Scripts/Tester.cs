using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private StripsChanger stripsChanger;
    string test = "http://127.0.0.1:5500/img/StripeBottom.png";

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stripsChanger.ReceiveTopStripeImage(test);
            stripsChanger.ReceiveBotStripeImage(test);
        }
    }
}
