using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public  class AccesoDatos
    {
        private SqlConnection conexion;//Primero declaro las propiedades 
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get
            {
                return lector;
            }
        }
         
        public AccesoDatos() //Constructor dice que cuando nace la clase inicia con esta conexion. Cada vex que nace el objeto acceso a datos lo hace con una esta conexion..
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS;database=POKEDEX_DB;integrated security=true");
            comando = new SqlCommand(); //Lo mismo que necesito un comando....

        }
        //Hasta aca tenemos la conexion...ahora vamos a pasarle la consulta al comando con la funcion....
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
                comando.Connection = conexion; //El comando que le paso arriba de tipo text y encapsulo en una consulta. Aca el comando lo ejecuta
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerarConexion()
        {
            if(lector != null)
                lector.Close();


            conexion.Close();


        }

    }
}
