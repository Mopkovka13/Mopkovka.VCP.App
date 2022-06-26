using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertExcel
{
    public class Excel
    {
        private string _path;
        FileInfo file;
        public Excel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            file = new FileInfo(path);
        }

        public void Read()
        {
            using var package = new ExcelPackage(file);
            package.LoadAsync(file);
            var ws = package.Workbook.Worksheets[0];

            int row = 0;
            int col = 0;
            Console.WriteLine(ws.Cells[row, col].Value.ToString());
        }     
    }
}
