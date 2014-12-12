using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class DictionaryHelpers
{
    public static int[] GetIntKeyArray(int size, int baseValue = 0)
    {
        int[] keys = new int[size];

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = ((i * 3) % size) + baseValue;
        }

        return keys;
    }

    public static string[] GetStringKeyArray(int size, int baseValue = 0)
    {
        string[] keys = new string[size];

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = (((i * 3) % size) + baseValue).ToString("D4", CultureInfo.InvariantCulture);
        }

        return keys;
    }
}
