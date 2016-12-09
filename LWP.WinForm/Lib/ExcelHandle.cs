using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;  

namespace LWP.WinForm.Lib
{
    public class ExcelHandle
    {
        public static bool CreateExcelFile(string FileName)
        {
            try
            {
                //create  
                object Nothing = System.Reflection.Missing.Value;
                var app = new Excel.Application();
                app.Visible = false;
                Excel.Workbook workBook = app.Workbooks.Add(Nothing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workBook.Sheets[1];
                worksheet.Name = "Work";
                //headline  
                worksheet.Cells[1, 1] = "ProductName";
                worksheet.Cells[1, 2] = "CreateTime";
                worksheet.Cells[1, 3] = "Keywords";

                worksheet.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing);
                workBook.Close(false, Type.Missing, Type.Missing);
                app.Quit();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog(e);
                return false;
            }
        }


        public static bool WriteToExcel(string excelName, string productName, string createTime, string keywords)
        {
            try
            {
                //open  
                object Nothing = System.Reflection.Missing.Value;
                var app = new Excel.Application();
                app.Visible = false;
                Excel.Workbook mybook = app.Workbooks.Open(excelName, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
                Excel.Worksheet mysheet = (Excel.Worksheet)mybook.Worksheets[1];
                mysheet.Activate();
                //get activate sheet max row count  
                int maxrow = mysheet.UsedRange.Rows.Count + 1;
                mysheet.Cells[maxrow, 1] = productName;
                mysheet.Cells[maxrow, 2] = createTime;
                mysheet.Cells[maxrow, 3] = keywords;
                mybook.Save();
                mybook.Close(false, Type.Missing, Type.Missing);
                mybook = null;
                //quit excel app  
                app.Quit();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog(e);
                return false;
            }
        } 
    }
}
