using Apiders2.Models;
using apiegitim1.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace apiegitim1.Controllers
{
    [EnableCors("*", "*", "*")] // naber
    public class KategoriController : ApiController
    {
        public List<Kategoriler> listele()
        {
            POSDBEntities context = new POSDBEntities();
            var list = context.Kategoriler.ToList();
            return list;
        }

        [HttpPost] // istek post türünde gelsin
        public bool kaydet(Kategoriler kategori)
        {
            try
            {
                POSDBEntities context = new POSDBEntities();
                context.Kategoriler.Add(kategori);
                context.SaveChanges(); // değişikliği uygula demek
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpPost] // istek post türünde gelsin
        public bool sil(int kategoriId)
        {
            try
            {
                Kategoriler kategoriler=new Kategoriler();
                kategoriler.id = kategoriId;

                POSDBEntities context = new POSDBEntities();
                context.Kategoriler.Attach(kategoriler); // bunu yapmadan olmaz
                context.Kategoriler.Remove(kategoriler);
                context.SaveChanges();// değişikliği uygula demek
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpPost] // istek post türünde gelsin
        public bool guncelle(Kategoriler kategori)
        {
            try
            {
                using (var dbContext = new POSDBEntities()) // using demek belleği çıkınca temizlesin demek
                {
                    dbContext.Kategoriler.Attach(kategori);
                    dbContext.Entry(kategori).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public object listeleJoin()
        {
            POSDBEntities context = new POSDBEntities();
            string query = @"select kategoriId,Urunler.id as urunId,Urunler.ad as urunAd,Kategoriler.ad as kategoriAd from Urunler 
left join Kategoriler  on Urunler.kategoriId=Kategoriler.id";
            var list = getQueryToDataTableNew(query,context);
            return list;
        }


        [ApiExplorerSettings(IgnoreApi = true)] // yazılan metot swaggerde gözükmesin demek
        public DataTable getQueryToDataTableNew(string query, DbContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = 0;//sınırsız demek
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }




        public object getKategorilerString()
        {
            List<String> list = new List<String>() { "Yiyecekler", "İçecekler" };
            return list;
        }

        [Route("getKategoriler12")]
        public object getKategoriler1()
        {
            List<Kategoriler> list = new List<Kategoriler>();
            Kategoriler kategoriler = new Kategoriler();
            kategoriler.id = 1;
            kategoriler.ad = "Yiyecekler";
            list.Add(kategoriler);

            kategoriler = new Kategoriler();
            kategoriler.id = 2;
            kategoriler.ad = "İçecekler";
            list.Add(kategoriler);

            return list;
        }


        public void datatableToObject()
        {

            using (var dbContext = new POSDBEntities()) // using demek belleği çıkınca temizlesin demek
            {
                string query = @"select Kategoriler.id,Kategoriler.ad,kategoriId,tarih,fiyat,aktif from Urunler
left join Kategoriler on Kategoriler.id=Urunler.kategoriId";

                DataTable dataTable = getQueryToDataTableNew(query, dbContext);
                string json = JsonConvert.SerializeObject(dataTable);
                List<KatUrunModel> modelim = JsonConvert.DeserializeObject<List<KatUrunModel>>(json);
            }

           
        }



    }
}