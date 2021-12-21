using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Excel
{
    public class Program
    {
        public static void Main()
        {
            var personsList = new List<Person>
            {
                new() {FirstName = "Ivan", LastName = "Ivanov", Age = 25, Phone = "2125036"},
                new() {FirstName = "Anton", LastName = "Petrov", Age = 23, Phone = "2125569"},
                new() {FirstName = "Denis", LastName = "Sidorov", Age = 35, Phone = "2125255"}
            };

            using var excelPackage = new ExcelPackage();

            excelPackage.Workbook.Properties.Title = "Persons";
            excelPackage.Workbook.Properties.Created = DateTime.Now;

            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

            worksheet.Cells["A1"].Value = "FirstName";
            worksheet.Cells["B1"].Value = "LastName";
            worksheet.Cells["C1"].Value = "Age";
            worksheet.Cells["D1"].Value = "Phone";

            var tableHead = "A1:D1";

            worksheet.Cells[tableHead].Style.Font.Size = 16;
            worksheet.Cells[tableHead].Style.Font.Name = "Calibri";
            worksheet.Cells[tableHead].Style.Font.Bold = true;
            worksheet.Cells[tableHead].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[tableHead].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            worksheet.Cells[tableHead].AutoFitColumns();

            worksheet.Cells["A2"].LoadFromCollection(personsList);

            for (var i = 1; i <= worksheet.Cells[tableHead].Columns; i++)
            {
                worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }

            var table = worksheet.Cells[1, 1, personsList.Count + 1, 4];

            table.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            table.AutoFitColumns();

            var outputWorkBook = new FileInfo("file.xlsx");
            excelPackage.SaveAs(outputWorkBook);
        }
    }
}