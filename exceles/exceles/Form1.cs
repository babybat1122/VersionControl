﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Drawing.Text;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using Microsoft.Office.Core;

namespace exceles
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errmsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errmsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[] 
            {
            "Kód",
            "Eladó",
            "Oldal",
            "Kerület",
            "Lift",
            "Szobák száma",
            "Alapterület (m2)",
            "Ár (mFt)",
            "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (var item in Flats)
            {
                string lift = "";
                if (item.Elevator == true)
                {
                    lift = "Van";
                }
                else
                {
                    lift = "Nincs";
                }
                values[counter, 0] = item.Code;
                values[counter, 1] = item.Vendor;
                values[counter, 2] = item.Side;
                values[counter, 3] = item.District;
                values[counter, 4] = lift;
                values[counter, 5] = item.NumberOfRooms;
                values[counter, 6] = item.FloorArea;
                values[counter, 7] = item.Price / item.FloorArea;
                values[counter, 8] = xlSheet.get_Range(
                                                       GetCell(2, 1),
                                                       GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
                counter++;
            }
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

    }
}
