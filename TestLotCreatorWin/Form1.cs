using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Contrib;
using WatiN.Core;
using WatiN.Core.Constraints;
using WatiN.Core.DialogHandlers;
using Button = WatiN.Core.Button;
using Form = System.Windows.Forms.Form;
using RadioButton = WatiN.Core.RadioButton;

namespace TestLotCreatorWin
{
    public partial class Form1 : Form
    {
        private IE _browser;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //_browser = new WatiN.Core.IE();
            //_browser.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FeelControls();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveLot();
        }




        private void LoadPage()
        {
            _browser.GoTo("http://24au.ru/new/");

            CheckIsLogged();

            //_browser.GoTo("http://www.avito.ru/additem");
        }


        private void CheckIsLogged()
        {
            Button enterButton = _browser.Button(Find.ByClass("button lined-button toolbar-enter-button"));

            if (enterButton.Exists)
            {
                _browser.TextField("UserName").Value = "poolsar";
                _browser.TextField("Password").Value = "rolton";
                enterButton.Click();
            }
        }

        private void FeelControls()
        {
            // название
            _browser.TextField("lotc-ctg-search-field").Value = "зайка попрыгайка";


            // категория
            _browser.SelectList(Find.ByName("Cat1")).Option(Find.ByValue("928")).Select();
            _browser.FireChangeByName("Cat1");
            _browser.SelectList(Find.ByName("Cat2")).SelectByValue("933");
            _browser.FireChangeByName("Cat2");
            _browser.SelectList(Find.ByName("Cat3")).SelectByValue("934");
            _browser.FireChangeByName("Cat3");


            // состояние новое
            _browser.RadioButtons.ToList().First(r => r.GetAttributeValue("Value") == "18791").Checked = true;




            // фотографии
            string filePath = "C:\\mytemp\\456.jpg";
            Frame frame = _browser.Frame("uploaderIframe");
            FileUpload fileUploadElement = frame.FileUpload(Find.ById("fileInput"));
            fileUploadElement.Set(filePath);


            // описание
            _browser.TextField("DescriptionText").Value = "зайка попрыгайка";

            // фиксированная цена
            _browser.RadioButton("TypeAuctionOnlyBlit").Click();

            // цена
            _browser.TextField("OnlyBlitzPrice").Value = 999.ToString();

            //продолжительность торгов
            _browser.SelectList("Days").SelectByValue("14");

            // продвижение лота
            _browser.CheckBox("IsAdvanceShow").Checked = false;


        }

        private void SaveLot()
        {

            _browser.Button("lotc-form-submit").Click();


        }



        private void button9_Click(object sender, EventArgs e)
        {
            _browser.GoTo("http://krsk.24au.ru/3730223/?jsAction=empty");


            Text = string.Format("Лот №{0}", _browser.Uri.LocalPath.Replace("/", ""));
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }


    }

    public static class BrawserExtensions
    {
        public static void FireChangeById(this IE browser, string elementId)
        {
            browser.Eval("$('#" + elementId + "').change();");
        }

        public static void FireChangeByName(this IE browser, string elementName)
        {
            browser.Eval("$('select[name=" + elementName + "]').change();");
        }
    }
}
