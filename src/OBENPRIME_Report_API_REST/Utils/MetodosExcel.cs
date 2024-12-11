using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace OBENPRIME_Netsuite_API_REST.Utils
{
    public class MetodosExcel
    {
        public void ColocarBordes(ExcelWorksheet hoja1, string rango)
        {
            hoja1.Cells[rango].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            hoja1.Cells[rango].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            hoja1.Cells[rango].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            hoja1.Cells[rango].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        }

        public void AjustarTexto(ExcelWorksheet hoja1, string rango)
        {
            hoja1.Cells[rango].Style.WrapText = true;
        }

        public void CombinarCentrar(ExcelWorksheet hoja1, string rango)
        {
            hoja1.Cells[rango].Merge = true;
            hoja1.Cells[rango].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            hoja1.Cells[rango].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        public void CorlorRango(ExcelWorksheet hoja1, string rango, string colorHex)
        {
            Color color = ColorTranslator.FromHtml(colorHex);
            hoja1.Cells[rango].Style.Fill.PatternType = ExcelFillStyle.Solid;
            hoja1.Cells[rango].Style.Fill.BackgroundColor.SetColor(color);
        }
    }
}
