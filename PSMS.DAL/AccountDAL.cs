﻿using MySql.Data.MySqlClient;
using PMSM.DAL;
using PSM;
using PSMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSMS.DAL
{
    class AccountDAL
    {
        SqlDbHelper db = new SqlDbHelper();
        UserDAL userdal = new UserDAL();
         //用户注册账号，需要传入密码和账号，用户Id会自己生成
        public int AddAccount(int number,String password)
        {
            //sql语句
            String sql = "INSERT INTO t_account(user_account,user_password,Is_del) VALUES(@user_id,@user_account,@user_password,@Isdel)";
            //参数列表
            MySqlParameter[] param = {
                                       new MySqlParameter("@user_account",SqlDbType.Int),
                                       new MySqlParameter("@user_password",SqlDbType.VarChar),
                                       new MySqlParameter("@Is_del",SqlDbType.Int),
                                     
                                   };

            //参数赋值
            param[0].Value = number;
            param[1].Value = password;
            param[2].Value = 0;
            return db.ExecuteNonQuery(sql, param);
        }
        //实现登陆,只返回Account的对象，如果数据库中没有与输入的账号密码匹配，则返回为空
        public Account GetUserByLoginNameAndPassword( int number, string password)//number--用户账号，//password--用户密码//isdel=0--用户存在，isdel=1用户不存在
        {
    
            string sql = "SELECT * FROM t_account WHERE user_account=@user_account AND user_password=@user_password AND Is_del=0";
            MySqlParameter[] @params = {
                                        new MySqlParameter("@name",MySqlDbType.VarChar),
                                        new MySqlParameter("@password",MySqlDbType.VarChar),
                                        new MySqlParameter("@Is_del",MySqlDbType.VarChar),
                                   };
            @params[0].Value = number;
            @params[1].Value = password;
            DataTable dt = db.ExecuteDataTable(sql, @params);
            Account account = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0]; // 第1行记录
                account= DataRowToAccount(dr);
            }
            return account;
        }
        //需要传入要修改密码的账号，并且传入新的密码

        public int UpdatePwd(Account account, string newPwd)
        {
            //1 sql语句
            string sql = "UPDATE t_account SET password=@newPwd WHERE id=@id AND password=@oldPwd";
            MySqlParameter[] param = {
                new MySqlParameter("@id",MySqlDbType.Int32),
                new MySqlParameter("@oldPwd",MySqlDbType.Int32),
                new MySqlParameter("@newPwd",MySqlDbType.VarChar)
            };
            param[0].Value = account.id;
            param[1].Value = account;
            param[2].Value = newPwd;
            return db.ExecuteNonQuery(sql, param);
        }
        //软删除，改变Is_del的值
        public int SoftDelete(int id)
        {
            string sql = "UPDATE t_account SET Is_del=1 WHERE user_id="+id; // 存在SQL注入的风险

            return db.ExecuteNonQuery(sql);
        }
        //使用SQL语句直接删除由id指定的用户数据
        public int RealDelete(int id)
        {
            string sql = "DELETE FROM t_account WHERE No =" + id;

            return db.ExecuteNonQuery(sql);
        }
        //显示所有的账号信息,以List的形式
        public List<Account> GetAllAccount()
        {
         
            string sql = "SELECT * FROM t_account" ;
            DataTable dt = db.ExecuteDataTable(sql);
            List<Account> accounts = new List<Account>();
            foreach (DataRow dr in dt.Rows)
            {
                //行转化成对象
                Account account = DataRowToAccount(dr);
                accounts.Add(account);
            }
            return accounts;
        }
        //根据id查找账号信息

        public Account GetAccountById(int id)
        {
            //1 sql语句
            string sql = "SELECT * FROM t_account WHERE user_id=@id";
            MySqlParameter[] param = {
                                        new MySqlParameter("@id",MySqlDbType.Int32)
                                   };
            param[0].Value = id;
            DataTable dt = db.ExecuteDataTable(sql, param);
            Account account = null;
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                account= DataRowToAccount(dr);
            }
            return account;
        }
        private Account DataRowToAccount(DataRow dr)
        {
            Account account= new Account();
            account.id = Convert.ToInt32(dr["user_id"]);   
            account.ac_number = Convert.ToInt32(dr["user_account"]);
            account.password = Convert.ToString(dr["user_password"]);
            return account;
        }
    }
}
