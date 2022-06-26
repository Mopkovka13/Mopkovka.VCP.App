using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConvertExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Product> list = new List<Product>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(@"D:\Учеба\Mopkovka.VCP.App\ConvertExcel\PrivetMir.xlsx")))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Реестр"];
                int row = worksheet.Dimension.Rows;
                int column = worksheet.Dimension.Columns;
                worksheet.Cells[1, 3].Calculate();
                string? tempName;
                string? tempActivity;
                string? tempInstitute;
                string? tempDateOfActivity;
                for (int i = 3; i < row; i++)
                {
                    tempName = worksheet.Cells[i, 13].Value?.ToString();
                    tempInstitute = worksheet.Cells[i, 14].Value?.ToString();
                    tempActivity = worksheet.Cells[i, 2].GetVal();
                    tempDateOfActivity = worksheet.Cells[i, 4].GetVal();
                    DateTime start = new DateTime(1900, 1, 1);
                    double z = 0;
                    bool successConvert = Double.TryParse(tempDateOfActivity, out double ValidDate);
                    if(successConvert)
                    {
                        tempDateOfActivity = start.AddDays(ValidDate - 2).ToString();
                        tempDateOfActivity = tempDateOfActivity.Substring(0, tempDateOfActivity.Length - 8);
                        list.Add(new Product(tempName, tempInstitute, tempActivity, tempDateOfActivity));
                    }
                    else
                    {
                        list.Add(new Product(tempName, tempInstitute, tempActivity, tempDateOfActivity));
                    }
                }
            }
            foreach (Product product in list)
                Console.WriteLine($"{product.Name} {product.Activity} {product.DateOfActivity:T}") ;

            /*using(Product product1 = new Product())
            {

            }*/


        }

    }
    public static class MyExtension
    {
        public static string GetVal(this ExcelRange @this) // Получать для объединённых ячеек
        {
            if (@this.Merge)
            {
                var idx = @this.Worksheet.GetMergeCellId(@this.Start.Row, @this.Start.Column);
                string mergedCellAddress = @this.Worksheet.MergedCells[idx - 1];
                string firstCellAddress = @this.Worksheet.Cells[mergedCellAddress].Start.Address;
                return @this.Worksheet.Cells[firstCellAddress].Value?.ToString()?.Trim() ?? "";
            }
            else
            {
                return @this.Value?.ToString()?.Trim() ?? "";
            }
        }
    }
}