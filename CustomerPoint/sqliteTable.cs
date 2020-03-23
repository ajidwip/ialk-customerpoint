using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CustomerPoint
{
    class sqliteTable
    {
        //table master customer
        public static string T_MsCustomer = "T_MsCustomer";
        public static string MasterKey = "MasterKey";
        public static string CustName = "CustName";
        public static string NoHP = "NoHp";
        public static string NoKTP = "NoKTP";
        public static string Alamat = "Alamat";
        public static string NamaPemilik = "NamaPemilik";
        public static string NPWP = "NPWP";
        public static string Area = "Area";
        public static string kodepos = "kodepos";
        public static string jenisusaha = "jenisusaha";

        public static string CREATE_T_MsCustomer = "CREATE TABLE " + T_MsCustomer + "(" + MasterKey + " TEXT," + CustName + " TEXT," + NoHP + " TEXT," + NoKTP + " TEXT," + Alamat + " TEXT," + NamaPemilik + " TEXT," + NPWP + " TEXT,"+ Area + " TEXT,"+ kodepos + " TEXT,"+ jenisusaha + " TEXT);";


        //table master promo
        public static string T_MsPromo = "T_MsPromo";
        public static string NamaPromo = "NamaPromo";
        public static string validfrom = "validfrom";
        public static string validto = "validto";
        public static string GambarPromo = "GambarPromo";
        public static string SK = "SK";

        public static string CREATE_TableMasterpromo = "CREATE TABLE " + T_MsPromo + "(" + NamaPromo + " TEXT," + validfrom + " TEXT," + validto + " TEXT," + GambarPromo + " BLOB," + SK + " TEXT);";
    }
}