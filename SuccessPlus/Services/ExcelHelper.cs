using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;

namespace SuccessPlus.Services
{
    internal class ExcelHelper : IDisposable
    {
        private Application _excel;
        private Workbook _workbook;

        public ExcelHelper()
        {
            _excel = new Excel.Application();
        }

        public void Dispose()
        {
            try
            {
                _workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal bool Open(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    _workbook = _excel.Workbooks.Open(filePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return false;
        }

        internal void Save()
        {
            _workbook.Save();
        }

        internal bool Set(string column, int row, string data)
        {
            try
            {
                ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column] = data;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }
}