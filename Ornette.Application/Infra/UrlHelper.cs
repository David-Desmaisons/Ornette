using System;

namespace Ornette.Application.Infra
{
    public static class UrlHelper
    {
        public static string BuildFromByteArray(string mineType, byte[] byteArray)
        {
            var b64String = Convert.ToBase64String(byteArray);
            return $"data:{mineType};base64,{b64String}";
        }
    }
}
