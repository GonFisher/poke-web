using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pokemon
    {
        public int Id { get; set; }

        [DisplayName("Número")]//Anoteicion!!!!!! El datgreedview toma el nombre del atributo si necesitamos poner tilde o mas de una palabra usamos este
        public int Numero { get; set; }
        public string Nombre { get; set; }
        public string  Descripcion { get; set; }

        public string UrlImagen { get; set; }

        public Elemento Tipo { get; set; }

        public Elemento Debilidad { get; set; }

        //EL FRMPOKEMONS SALE CON ESE ORDEN POR UNA TECNICA DE SISTEMA QUE SE LLAMA REFLECXION Y VE EL OBJETO 
        //COPIA LA ESTRUCTURA DE LA CLASE POKEMON....
    }
}
