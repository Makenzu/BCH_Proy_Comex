using System;

namespace BCH.Comex.Core.BL.XGSV
{
    public static class ExtensionMethods
    {
        public static string FormatAsCD(this decimal d, int totalWidth)
        {
            return d.ToString("#.#0").Replace(",", String.Empty).PadLeft(totalWidth, '0');
        }
    }
}
