using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionComparer : IComparer
{
    public int Compare(object x, object y)
    {
        Potion potionX = x as Potion;
        Potion potionY = y as Potion;

        if (potionX == null || potionY == null)
        {
            return 1;
        }

        return (potionX.value > potionY.value) ? 1 : -1;
    }
}
