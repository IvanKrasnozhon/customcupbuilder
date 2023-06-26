using System.Collections.Generic;
using UnityEngine;

public class CupSizeChanger : MonoBehaviour
{
    private CustomList<Cup> cups;
    [SerializeField] private List<Cup> cupsTemp = new List<Cup>();

    private void Awake()
    {
        cups = new CustomList<Cup>(cupsTemp);
        cups.Next();
    }

    public void SetSize(int size)
    {
        cups.GetCurrentItem().gameObject.SetActive(false);
        cups.SetCurrentItemById(size);
        cups.GetCurrentItem().gameObject.SetActive(true);
    }
    
    public int GetCurrentCupId()
    {
        return cups.GetCurrentIndex();
    }
}
