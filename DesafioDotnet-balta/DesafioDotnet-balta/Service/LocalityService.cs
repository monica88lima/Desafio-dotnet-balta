using Models;
using System.Globalization;

namespace DesafioDotnet_balta.Service
{
    public static class LocalityService
    {
        public static LocalityModel FormatFields(LocalityModel local)
        {
            local.State = local.State.ToUpper();

            string[] nameCity = local.City.Split(' ');
            if(nameCity.Length > 1 ) 
            {
               local.City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(local.City);          
            }
            //local.City = char.ToUpper(local.City[0]) + local.City.Substring(1);

            return local;
        }
    }
}
