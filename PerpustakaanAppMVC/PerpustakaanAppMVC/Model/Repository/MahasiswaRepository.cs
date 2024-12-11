using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using PerpustakaanAppMVC.Model.Entity;
using PerpustakaanAppMVC.Model.Context;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace PerpustakaanAppMVC.Model.Repository
{
    internal class MahasiswaRepository
    {
        private SQLiteConnection _conn;

        public MahasiswaRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public int Create(Mahasiswa mhs)
        {
            int result = 0;
            string sql = @"insert into mahasiswa (npm, nama, angkatan) values (@npm, @nama, @angkatan)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@npm", mhs.Npm);
                cmd.Parameters.AddWithValue("@nama", mhs.Nama);
                cmd.Parameters.AddWithValue("@angkatan", mhs.Angkatan);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        public int Update(Mahasiswa mhs)
        {
            int result = 0;
            string sql = @"update mahasiswa set nama = @nama, angkatan = @angkatan where npm = @npm";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@npm", mhs.Npm);
                cmd.Parameters.AddWithValue("@nama", mhs.Nama);
                cmd.Parameters.AddWithValue("@angkatan", mhs.Angkatan);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }
            return result;
        }


        public int Delete(Mahasiswa mhs)
        {
            int result = 0;
            string sql = @"delete from mahasiswa where npm = @npm";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@npm", mhs.Npm);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }
            return result;

        }

        public List<Mahasiswa> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                // deklarasi perintah SQL
                string sql = @"select npm, nama, angkatan from mahasiswa order by nama";
                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT) 
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set 
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object

                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            // tambahkan objek mahasiswa ke dalam collection

                            list.Add(mhs);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }

        public List<Mahasiswa> ReadByNama(string nama)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                // deklarasi perintah SQL
                string sql = @"select npm, nama, angkatan from mahasiswa where nama like @nama order by nama";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%",
                    nama));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object

                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            // tambahkan objek mahasiswa ke dalam collection

                            list.Add(mhs);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}",
                ex.Message);
            }
            return list;
        }
    }
}
