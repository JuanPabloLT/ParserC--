using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManejoDeErrores;
using System.Text.RegularExpressions;

namespace Tsimbolos
{
  public  class TS
    {
      public List<tabla_de_simbolos> TSimbolos = new List<tabla_de_simbolos>();
      TE tabla_errorres = new TE();


      public TS()
      {
           
      }
      public List<tabla_de_simbolos> TablaSimbolos
      {
          get { return TSimbolos; }
          set { TSimbolos = value; }
      }

      public void reinicialista()
      {
          TSimbolos.Clear();
      }

        #region Tabla de Simbolos , 51
        public void inicialista()
        {
            //string simb,string val,int nunlin,int tam,int ambit,int id_,string tip,string descrip
            tabla_de_simbolos ts = new tabla_de_simbolos("--//", -0,   0, "comentario", "inicio de un comentario de mas de una linea");
            TSimbolos.Add(ts);
            tabla_de_simbolos ts1 = new tabla_de_simbolos("//--", -0,   1, "comentario", "final de un comentario de mas de una linea");
            TSimbolos.Add(ts1);
            tabla_de_simbolos ts2 = new tabla_de_simbolos("//", -0,   2, "comentario", "comentario de una linea");
            TSimbolos.Add(ts2);
            tabla_de_simbolos ts4 = new tabla_de_simbolos("{", -0,   3, "bloque", "inicio de un bloque");
            TSimbolos.Add(ts4);
            tabla_de_simbolos ts5 = new tabla_de_simbolos("}", -0,   4, "bloque", "final de un bloque");
            TSimbolos.Add(ts5);
            tabla_de_simbolos ts6 = new tabla_de_simbolos("=", -0,  5, "asignacion", "simbolo de asignacion");
            TSimbolos.Add(ts6);
            tabla_de_simbolos ts51 = new tabla_de_simbolos("==", -0,  6, "comparador", "simbolo de comparacion igual que");
            TSimbolos.Add(ts51);
            tabla_de_simbolos ts52 = new tabla_de_simbolos("<", -0,   7, "comparador", "simbolo de comparacion menor que");
            TSimbolos.Add(ts52);
            tabla_de_simbolos ts53 = new tabla_de_simbolos(">", -0,  8, "comparador", "simbolo de comparacion mayor que");
            TSimbolos.Add(ts53);
            tabla_de_simbolos ts54 = new tabla_de_simbolos("<=", -0, 9, "comparador", "simbolo de comparacion menor igual que");
            TSimbolos.Add(ts54);
            tabla_de_simbolos ts55 = new tabla_de_simbolos(">=",   -0, 10, "comparador", "simbolo de comparacion mayor igual que");
            TSimbolos.Add(ts55);
            tabla_de_simbolos ts56 = new tabla_de_simbolos("<>",   -0, 11, "comparador", "simbolo de comparacion diferente");
            TSimbolos.Add(ts56);
            tabla_de_simbolos ts57 = new tabla_de_simbolos("!=",    -0, 12, "comparador", "simbolo de comparacion no es igual que");
            TSimbolos.Add(ts57);
            tabla_de_simbolos ts7 = new tabla_de_simbolos("Entero",   -0, 13, "palabra reservada", "numero entero");
            TSimbolos.Add(ts7);
            tabla_de_simbolos ts8 = new tabla_de_simbolos("Real",   -0, 14, "palabra reservada", "numero con decimales");
            TSimbolos.Add(ts8);
            tabla_de_simbolos ts9 = new tabla_de_simbolos("Cadena",    -0, 15, "palabra reservada", "cadena de caracteres");
            TSimbolos.Add(ts9);
            tabla_de_simbolos ts10 = new tabla_de_simbolos("Vacio",   -0, 16, "palabra reservada", "metodo vacio");
            TSimbolos.Add(ts10);
            tabla_de_simbolos ts11 = new tabla_de_simbolos("Booleano",    -0, 17, "palabra reservada", "booleano true o false");
            TSimbolos.Add(ts11);
            tabla_de_simbolos ts12 = new tabla_de_simbolos(":",  -0,   18, "asignacion", "simbolo de asignacion");
            TSimbolos.Add(ts12);
            tabla_de_simbolos ts13 = new tabla_de_simbolos(";",  -0,   19, "posicionador", "final de linea");
            TSimbolos.Add(ts13);
            tabla_de_simbolos ts14 = new tabla_de_simbolos("'",   -0, 20, "indicador de texto", "inicio y final de un texto");
            TSimbolos.Add(ts14);
            tabla_de_simbolos ts16 = new tabla_de_simbolos("[",    -0, 21, "arreglo", "inicio de asignacion de un arreglo");
            TSimbolos.Add(ts16);
            tabla_de_simbolos ts17 = new tabla_de_simbolos("]",    -0, 22, "arreglo", "final de asignacion de un arreglo");
            TSimbolos.Add(ts17);
            tabla_de_simbolos ts18 = new tabla_de_simbolos("+",   -0, 23, "operador", "suma");
            TSimbolos.Add(ts18);
            //tabla_de_simbolos ts19 = new tabla_de_simbolos("+", "", -0, -0, -0, 19, "concatenador", "concatenador de elementos");
            //TSimbolos.Add(ts19);
            tabla_de_simbolos ts20 = new tabla_de_simbolos("-",   -0, 24, "operador", "resta");
            TSimbolos.Add(ts20);
            tabla_de_simbolos ts21 = new tabla_de_simbolos("*",    -0, 25, "operador", "multiplicacion");
            TSimbolos.Add(ts21);
            tabla_de_simbolos ts32 = new tabla_de_simbolos("Si",   -0, 26, "palabra reservada", "si tal condicion se cumple");
            TSimbolos.Add(ts32);
            tabla_de_simbolos ts33 = new tabla_de_simbolos("Sino",  -0, 27, "palabra reservada", "y si tal condicion se cumple en vez de la anterior");
            TSimbolos.Add(ts33);
            tabla_de_simbolos ts34 = new tabla_de_simbolos("Probar",   -0, 28, "palabra reservada", "probar lineas");
            TSimbolos.Add(ts34);
            tabla_de_simbolos ts35 = new tabla_de_simbolos("Mientras",   -0, 29, "palabra reservada", "ejecuta ciclo mientras se cumple condicion");
            TSimbolos.Add(ts35);
            tabla_de_simbolos ts36 = new tabla_de_simbolos("Verdadero",   -0, 30, "palabra reservada", "resultado afirmativo");
            TSimbolos.Add(ts36);
            tabla_de_simbolos ts37 = new tabla_de_simbolos("Consola.EscribirLinea",    -0, 31, "metodo reservado", "imprime los valores");
            TSimbolos.Add(ts37);
            tabla_de_simbolos ts38 = new tabla_de_simbolos("Real.Analizar",   -0, 32, "metodo reservado", "convertir a numero real");
            TSimbolos.Add(ts38);
            tabla_de_simbolos ts39 = new tabla_de_simbolos("Consola.LeerLinea",   -0, 33, "metodo reservado", "lee los valores ingresados");
            TSimbolos.Add(ts39);
            tabla_de_simbolos ts40 = new tabla_de_simbolos("Capturar",  -0,   34, "palabra reservada", "condicion error de prueba");
            TSimbolos.Add(ts40);
            tabla_de_simbolos ts41 = new tabla_de_simbolos("FormatoExcepcion",   -0, 35, "palabra reservada", "tipo de excepcion de error");
            TSimbolos.Add(ts41);
            tabla_de_simbolos ts42 = new tabla_de_simbolos("Consola.Limpiar",   -0, 36, "metodo reservada", "limpia la consola de ingreso de datos");
            TSimbolos.Add(ts42);
            tabla_de_simbolos ts43 = new tabla_de_simbolos("Y",  -0,  37, "comparador", "comparador de concatenacion");
            TSimbolos.Add(ts43);
            tabla_de_simbolos ts45 = new tabla_de_simbolos("Principal",   -0, 38, "palabra reservada", "metodo principal");
            TSimbolos.Add(ts45);
            tabla_de_simbolos ts46 = new tabla_de_simbolos("Cadena[]",   -0, 39, "palabra reservada", "arreglo de tipo cadena");
            TSimbolos.Add(ts46);
            tabla_de_simbolos ts47 = new tabla_de_simbolos("Clase",    -0, 40, "palabra reservada", "clase");
            TSimbolos.Add(ts47);
            tabla_de_simbolos ts48 = new tabla_de_simbolos("(", -0,   41, "parametro", "inicia peticion de parametro");
            TSimbolos.Add(ts48);
            tabla_de_simbolos ts49 = new tabla_de_simbolos(")",    -0, 42, "parametro", "termina peticion de parametro");
            TSimbolos.Add(ts49);
            tabla_de_simbolos ts50 = new tabla_de_simbolos(",",    -0, 43, "concatenador", "concatena variables");
            TSimbolos.Add(ts50);
            tabla_de_simbolos ts58 = new tabla_de_simbolos("Estatico",    -0, 44, "palabra reservada", "estatico");
            TSimbolos.Add(ts58);

            //---------------------------------------------asi deberian entrar los datos nuevos encontrados-------------------------------------------------
            //tabla_de_simbolos ts51 = new tabla_de_simbolos("", "", -0, -0, -0, 51, "numero", "especifica un numero");
            //TSimbolos.Add(ts51);
            //tabla_de_simbolos ts52 = new tabla_de_simbolos("", "", -0, -0, -0, 52, "identificador", "especifica una palabra que identifica una variable");
            //TSimbolos.Add(ts52);
            //----------------------------------------------------------------------------------------------------------------------------------------------

        }
      #endregion


  

      public List<tabla_de_simbolos> llamatabla()
      {
        return TSimbolos;
      }

       public void añadir_obj(tabla_de_simbolos Ts )
       {
           TSimbolos.Add(Ts);
       }

       public string compararAL(string argsplit)
       {
           string bandera = "";
           foreach (var palabra in TSimbolos)
           {

               if (palabra.simbolo == argsplit)
               {
                   bandera = palabra.tipo;
                   break;
               }
               else
               {
                   bandera = "";
               }

           }
           return bandera;
       }

        public string compararALDesc(string argsplit)
        {
            string desc = "";
            foreach (var palabra in TSimbolos)
            {

                if (palabra.simbolo == argsplit)
                {
                    desc = palabra.Descripcion;
                    break;
                }
                else
                {
                    desc = "";
                }

            }
            return desc;
        }
        //----------------------------------------------------------------
        public bool revisar_duplicados()
       {
           bool flag = false;
           //int cont = 0;
           foreach (var sent1 in TSimbolos)
           {
               foreach (var sent2 in TSimbolos)
               {
                   if (sent1.ID == sent2.ID)//&& sent1.TipoVar == sent2.TipoVar
                   {

                       flag = true;    
                       
                       
                       //cont += 1;
                   }
               }
           }
           return flag;
       }
      //----------------------------------------------------------------
       

      //----------------------------------------------------------------

       public int compararALRef(string argsplit)
       {
           int id = 0 ;
           foreach (var palabra in TSimbolos)
           {

               if (palabra.simbolo == argsplit)
               {
                   id = palabra.id;
                   break;
               }
               else
               {
                   id = 0;
               }

           }
           return id;
       }

       //--------------------------------------------------------------
        public int contlineas ()
        {
            int numid = 0;
            foreach (var nlinea in TSimbolos)
            {
                numid = numid + 1;
            }
            return numid-1;
        }




    }
}
