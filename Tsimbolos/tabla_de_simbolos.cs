using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsimbolos
{
    public class tabla_de_simbolos
    {

        public string simbolo;
        public int NumLinea;
        public int id;      
        public string tipo;
        public string descripcion;

        public tabla_de_simbolos(string simb,int nunlin,int id_,string tip,string descrip)
        {

            simbolo = simb;          
            NumLinea = nunlin;
            id = id_;
            tipo = tip;
            descripcion = descrip;
            

        }

        
        public tabla_de_simbolos()
        {
                
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Simbolo
        {
            get { return simbolo; }
            set { simbolo = value; }
        }
       
        

        public int Numero_de_linea
        {
            get { return NumLinea; }
            set { NumLinea = value; }
        }
        

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
      
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


    }
}
