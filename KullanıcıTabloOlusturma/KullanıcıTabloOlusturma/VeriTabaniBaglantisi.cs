using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KullanıcıTabloOlusturma
{
    public class VeriTabaniBaglantisi
    {

        public static SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=Kullanici_Tablo_Olusturma;Integrated Security=True;");

        public static void BaglantiKontrolu()
        {

            if (baglanti.State == System.Data.ConnectionState.Closed)
            {
                baglanti.Open();
            }

            else { }
        }
    }

}