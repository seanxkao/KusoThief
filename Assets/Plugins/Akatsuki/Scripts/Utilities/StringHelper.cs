﻿using UnityEngine;

namespace Akatsuki
{
    public class StringHelper
    {
        public static string GetMinuteSecondTime(float time)
        {
            return (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        }
    }
}
