using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Constants.Enums;

public static class EnumHelper
{
    private static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? value.ToString() : attribute.Description;
    }

    /// <summary>
    /// Get the list of Enum items as SelectListItem
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<SelectListItem> GetEnumSelectList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Select(e => new SelectListItem
        {
            Value = ((int)(object)e).ToString(),
            Text = GetEnumDescription(e)
        });
    }
}
