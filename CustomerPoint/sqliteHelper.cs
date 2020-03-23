﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using System.Runtime.CompilerServices;

namespace CustomerPoint
{
    public class sqliteHelper : SQLiteOpenHelper
    {
        private static sqliteHelper dbInstance = null;
        private const string _DatabaseName = "CustomerPoint.db";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public sqliteHelper(Context context) : base(context, _DatabaseName, null, 1)
        {

        }

        public static sqliteHelper getDbInstance(Context context)
        {

            if (dbInstance == null)

                dbInstance = new sqliteHelper(context.ApplicationContext);
            return dbInstance;
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(sqliteTable.CREATE_T_MsCustomer);
            db.ExecSQL(sqliteTable.CREATE_TableMasterpromo);
        }
        
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

        }
        public void initDatabase()
        {
            SQLiteDatabase db = this.WritableDatabase;
            if (db.IsOpen)
            {
                db.Close();
            }
        }
    }
}