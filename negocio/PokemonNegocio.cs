using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Collections;


namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS;database=POKEDEX_DB;integrated security=true";  //String de donde esta mi base de datos etc
                comando.CommandType = System.Data.CommandType.Text; //Aca digo lo que el comando es su tipo digamos
                comando.CommandText = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS Tipo, D.Descripcion AS Debilidad, P.IdTipo , P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1"; //le pso el comando que quiero ejecutar
                comando.Connection = conexion; //El Comando que le pase(string(SELECT...)) lo va a inyecctar en esa conexion

                conexion.Open(); //ABRO LA CONEXION
                lector = comando.ExecuteReader(); //EJECUTO ESE COMANDO Y SE LO ASIGNO A LECTOR

                while (lector.Read()) //EL LECTOR VA LEER 
                {
                    Pokemon aux = new Pokemon(); // CREAMOS UN POKEMON NUEVO AUXILIAR SOLO PARA ESTE WHILE..UNA VARIABLE DE TIPO POKEMON 

                    aux.Id = (int)lector["Id"];
                    aux.Numero = (int)lector["Numero"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];

                    // if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    lista.Add(aux);
                }

                conexion.Close();

                return lista; //EL METODO ME VA A DEVOLVER ESA LISTA
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> listarConSP()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Pokemon> listaPokemons = new List<Pokemon>();

            try
            {
                string consulta = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS Tipo, D.Descripcion AS Debilidad, P.IdTipo , P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1";


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read()) //EL LECTOR VA LEER 
                {
                    Pokemon aux = new Pokemon(); // CREAMOS UN POKEMON NUEVO AUXILIAR SOLO PARA ESTE WHILE..UNA VARIABLE DE TIPO POKEMON 

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    listaPokemons.Add(aux);
                }

                return listaPokemons;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO POKEMONS(Numero, Nombre, Descripcion, UrlImagen, Activo, IdTipo, IdDebilidad )VALUES(" + nuevo.Numero + ",'" + nuevo.Nombre + "','" + nuevo.Descripcion + "',@urlImagen, 1 ,@idTipo, @idDebilidad)");
                datos.setearParametro("@urlImagen", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                //datos.ejecutarLectura NO pUedo ejecutarlo porque es una insercion no una lectura

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerarConexion();
            }
        }

        public void modificar(Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("UPDATE POKEMONS SET Numero=@numero,Nombre=@nombre, Descripcion=@desc, UrlImagen=@img,IdTipo=@tipo, IdDebilidad=@debilidad WHERE Id=@id");
                datos.setearParametro("@numero", poke.Numero);
                datos.setearParametro("@nombre", poke.Nombre);
                datos.setearParametro("@desc", poke.Descripcion);
                datos.setearParametro("@img", poke.UrlImagen);
                datos.setearParametro("@tipo", poke.Tipo.Id);
                datos.setearParametro("@debilidad", poke.Debilidad.Id);
                datos.setearParametro("@id", poke.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM POKEMONS WHERE Id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE POKEMONS SET Activo = 0 WHERE id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Pokemon> listaPokemons = new List<Pokemon>();

            try
            {
                string consulta = "SELECT P.Id, Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion AS Tipo, D.Descripcion AS Debilidad, P.IdTipo , P.IdDebilidad FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1 AND ";
                if (campo == "Numero")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {

                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {

                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "P.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "P.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "P.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read()) //EL LECTOR VA LEER 
                {
                    Pokemon aux = new Pokemon(); // CREAMOS UN POKEMON NUEVO AUXILIAR SOLO PARA ESTE WHILE..UNA VARIABLE DE TIPO POKEMON 

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = (int)datos.Lector["Numero"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    listaPokemons.Add(aux);
                }

                return listaPokemons;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
