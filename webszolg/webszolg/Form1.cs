﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using webszolg.Entities;
using webszolg.MnbServiceReference;

namespace webszolg
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GetExchangeRates();
            DataMining();

            BindingList<RateData> Rates;
            dataGridView1.DataSource = Rates;
        }

        public void GetExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
        }

        public void DataMining()
        {
            XmlDocument xml;
            xml.LoadXml(result);
        }
    }
}
