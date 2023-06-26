using System.Collections.Generic;
using UnityEngine;

public class CustomList<T>
{
    public List<T> list;
    private int currentIndex;

    public CustomList()
    {
        list = new List<T>();
        currentIndex = -1;
    }

    public CustomList(List<T> from)
    {
        list = new List<T>();
        foreach(T item in from)
        {
            Add(item);
        }
    }

    public void Add(T item)
    {
        list.Add(item);
        currentIndex = list.Count-1;
    }

    public T Next()
    {
        if (list.Count == 0)
        {
            Debug.Log("List is empty");
        }

        currentIndex++;
        if (currentIndex >= list.Count)
        {
            currentIndex = 0;
        }

        return list[currentIndex];
    }

    public void SetCurrentItemById(int itemId)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == itemId)
            {
                currentIndex = i;
                return;
            }
        }
        Debug.Log("Item with ID " + itemId + " not found in the list.");
    }

    public T GetCurrentItem()
    {
        return list[currentIndex];
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
