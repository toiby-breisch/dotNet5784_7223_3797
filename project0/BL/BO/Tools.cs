using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography;
using static System.Formats.Asn1.AsnWriter;
using System.Reflection.Metadata.Ecma335;

namespace BO;

static class Tools
 {
    [AttributeUsage(AttributeTargets.Property)]
    class PropertyDisplayAttribute : Attribute
    {
        public String DisplayValue { get; set; }
        public PropertyDisplayAttribute(string displayName)
        {
            DisplayValue = displayName;
        }
    }
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        PropertyInfo[] T_properties = t!.GetType().GetProperties();
        foreach (PropertyInfo item in T_properties)
        {
            Type myAttributeType = typeof(PropertyDisplayAttribute);
            object[] itemDisplayAtt = item.GetCustomAttributes(myAttributeType, false);
            if (itemDisplayAtt.Length == 1)
            {
                PropertyDisplayAttribute att = (PropertyDisplayAttribute)itemDisplayAtt[0];
                str += string.Format("\n {0,-15}  {1,-15}",
                att.DisplayValue,
                item.GetValue(t, null));
            }
            else
            {
                str += string.Format("\n {0,-15}  {1,-15}",
                item.Name,
                item.GetValue(t, null));
            }
        }
        return str;
    }
}
