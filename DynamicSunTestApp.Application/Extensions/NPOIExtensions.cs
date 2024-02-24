using System.Globalization;
using NPOI.SS.UserModel;

namespace DynamicSunTestApp.Application.Extensions;

public static class NPOIExtensions
{
    public static string ToCellValueString(this ICell? cell)
    {
        if (cell == null) return string.Empty;
        
        return cell.CellType switch
        {
            CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
            CellType.String => cell.StringCellValue,
            _ => string.Empty
        };
    }
}