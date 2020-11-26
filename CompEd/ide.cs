using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Tsimbolos;
using System.Text.RegularExpressions;//using necesario , llama ala referencia de la libreria de expresiones regulares 
using Microsoft.Office.Interop.Excel;
using ManejoDeErrores;



namespace CompEd
{
    public partial class Ide : Form
    {
        int cantLineas =0 ;
        string nomarchivox;
        TS tabla_simbolos = new TS();
        TE tabla_errorres = new TE();
       
    

        public Ide()
        {
            InitializeComponent();
            
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //AnlzdrSntctc();
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Ide_Load(object sender, EventArgs e)//---------------
        {
            tabla_errorres.inicialestaE();
            tabla_simbolos.inicialista();
            tabControl1.Visible = false;
            PagCodigo.Select();
            PagCodigo.DetectUrls = true;
            
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;

            this.Activate();
        }

        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void Ide_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogo = MessageBox.Show("¿ Desea cerrar el compilador?","Cerrar Compilador", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogo == DialogResult.OK) {
                System.Windows.Forms.Application.Exit();
            }
            else { 
                e.Cancel = true;
            }
            
        }

        private void acercaDeCompEdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirarchivo();          
        }


        //-----------------------------METODOS DE ARCHIVOS -----------------------------------------

        public void exportaraexcel(DataGridView tabla)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);

            int ColumnIndex = 0;

            foreach (DataGridViewColumn col in tabla.Columns)
            {

                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = col.Name;

            }

            int rowIndex = 0;

            foreach (DataGridViewRow row in tabla.Rows)
            {

                rowIndex++;

                ColumnIndex = 0;

                foreach (DataGridViewColumn col in tabla.Columns)
                {

                    ColumnIndex++;

                    excel.Cells[rowIndex + 1, ColumnIndex] = row.Cells[col.Name].Value;

                }

            }

            excel.Visible = true;

            Worksheet worksheet = (Worksheet)excel.ActiveSheet;

            worksheet.Activate();



        }

        public void abrirarchivo()
        {

             try{
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "                                                                     Abrir Archivo                                                                       ";
            ofd.ShowDialog();
           // ofd.Filter = "Archivos ed#(*.ed)|*.ed";
            if (File.Exists(ofd.FileName))
            {
                using (Stream stream = ofd.OpenFile())
                {
                    //MessageBox.Show("archivo encontrado:  "+ofd.FileName);
                    leerarchivo(ofd.FileName);
                    nomarchivox = ofd.FileName;
                    
                    
                    tabControl1.Visible = true;
                }
                
            }
            }catch(Exception){

                MessageBox.Show("El archivo no se abrio correctamente");

                tabla_errorres.addliste(2);
            }

        }

        public void leerarchivo(string nomarchivo)
        {
            StreamReader reader = new StreamReader(nomarchivo, System.Text.Encoding.Default);
            //string read = reader.ReadLine();
            string texto;
           // while (read != null)
            //{
                texto = reader.ReadToEnd();
               // read = read + "\n";
               
                reader.Close();

                PagCodigo.Text = texto;
               // read =reader.ReadLine();
               
            //}
            
            
        }

        public bool revisasiarchivoexiste(string nomarchivo)
        {

            bool existe;

            if (File.Exists(nomarchivo))
            {
                // el archivo existe
                existe = true;
            }
            else
            {
                // el archivo no extiste
                existe = false;
            }
            return existe;
        }

        public void guardaArchivo()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Archivos ed|*.ed";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                if (File.Exists(saveFile.FileName))
                {
                   
                    
                    //------------------ para sobrescribir el texto ...................
                    StreamWriter codigonuevo = File.CreateText(saveFile.FileName);
                    codigonuevo.Write(PagCodigo.Text);
                   codigonuevo.Flush();
                    codigonuevo.Close();
                    nomarchivox = saveFile.FileName;
                    

                }
                else
                {
                    // el archivo no extiste
                   
                        StreamWriter codigonuevo = File.CreateText(saveFile.FileName);
                        codigonuevo.Write(PagCodigo.Text);
                        codigonuevo.Write("\n \n <</ Archivo creado el: " + DateTime.Now.ToString() + " />> \n ");   
                        codigonuevo.Flush();
                        codigonuevo.Close();
                        nomarchivox = saveFile.FileName;
                        
                }
              

            }

        }

        public void guardaArchivo2(string nomarchivo)
        {
            try
            {
                if (nomarchivo == null)
                {
                    guardaArchivo();

                }
                else
                {
                    // el archivo nuevo
                    StreamWriter codigonuevo = File.CreateText(nomarchivo);
                    codigonuevo.Write(PagCodigo.Text);
                    codigonuevo.Flush();
                    codigonuevo.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("error al guardar");

            }
                                   

        }

        public void leer_archivo_al(string nomarchivo)
        {

            int contador_Ambitoi =0;
            int contador_Ambitf = 0;
            int ambito = 0;
            try 
            {
                StreamReader reader = new StreamReader(nomarchivo);
                string[] Palabras_Separadas;
                string read;
                int numero_de_lineas = 0;
                PagCodigo.Select(0, PagCodigo.SelectionStart);

                while (reader != null)
                {
                    numero_de_lineas = numero_de_lineas + 1;
                    read = reader.ReadLine();
                 

                    if (reader.EndOfStream)
                    {
                        //MessageBox.Show("ultima linea");

                        break;
                    }
                    else
                    {


                        Palabras_Separadas = read.Split(' ');
                        foreach (var palabra in Palabras_Separadas)
                        {
                            #region Medicion del ambito

                            if (palabra == "{")
                            {
                                contador_Ambitoi = contador_Ambitoi + 1;
                            }
                            if (palabra == "}")
                            {
                                contador_Ambitf = contador_Ambitf + 1;
                            }
                            ambito = contador_Ambitoi;


                            #endregion
                         

                           //-----------------------------------------------------------------------
                            

                            if (tabla_simbolos.compararAL(palabra.ToString())!="" && palabra != null)// se manda a comparar la palabra con la tabla de simbolos
                            {                             
                                //                                                    simb  ,val, nunlin           ,tam,ambit,                 id_,           tipo,       descrip
                                //uneSentencias();
                               
                                tabla_de_simbolos objnuevo = new tabla_de_simbolos(palabra,  numero_de_lineas, tabla_simbolos.compararALRef(palabra.ToString()), tabla_simbolos.compararAL(palabra.ToString()), tabla_simbolos.compararALDesc(palabra.ToString()));
                                tabla_simbolos.añadir_obj(objnuevo);

                                PagCodigo.SelectionStart = PagCodigo.Find(palabra);
                                PagCodigo.SelectionColor = Color.DodgerBlue;
                                
                            }
                            else//de no estar en la tabla de simbolos se agrega a un campo nuevo
                            {
                                if (Regex.IsMatch(palabra, @"[a-zA-Z]") && palabra != null)//sentencia que revisa los dos texbox 
                                {
                                    // System.Windows.Forms.MessageBox.Show("esto es una palabra");
                                    tabla_de_simbolos objnuevo = new tabla_de_simbolos(palabra, numero_de_lineas,  tabla_simbolos.contlineas() + 1, "Cadena nueva", "cadena de caracteres");
                                    tabla_simbolos.añadir_obj(objnuevo);
                                }
                                else if (Regex.IsMatch(palabra, @"\d{1}|\d{2}|\d{3}|\d{4}|\d{5}") && palabra != null)
                                {
                                    //System.Windows.Forms.MessageBox.Show("esto es un numero");
                                    tabla_de_simbolos objnuevo = new tabla_de_simbolos(palabra,  numero_de_lineas,  tabla_simbolos.contlineas() + 1, "numero nuevo", "numero");
                                    tabla_simbolos.añadir_obj(objnuevo);

                                    PagCodigo.SelectionStart = PagCodigo.Find(palabra);
                                    PagCodigo.SelectionColor = Color.Aquamarine;

                                }
                                else
                                {
                                    // System.Windows.Forms.MessageBox.Show("Error en la expresion \n no cumple con un formato correcto ");

                                }
                              }
                        }//fin del analisis lexico
                   
                    }
                    Palabras_Separadas = null;
                    cantLineas = numero_de_lineas;
                  
                }

                if (contador_Ambitf != contador_Ambitoi)
                {
                    //MessageBox.Show("error de ambito");
                    tabla_errorres.addliste(8);
                   

                }
             

                reader.Close();
            }
            catch (ArgumentNullException)
            {

                MessageBox.Show("El archivo no se abrio correctamente");
                
                tabla_errorres.addliste(2);
            }
            catch (Exception)
            {
                MessageBox.Show("error");
            }
          
            
        }

        public string[] uneSentencias (){
            string sentencia = null;
            string[] sentencias = new string[cantLineas];

            for (int i = 1; i < cantLineas; i++) //une los token de cada linea
            {
                foreach (var token in tabla_simbolos.llamatabla())
                {
                    if (token.NumLinea == i && token != null)
                    {
                            sentencia =  sentencia + " " + token.simbolo.ToString();
                    }
                }
                sentencias[i] = sentencia;
                sentencia = null;
            }
            

            return sentencias;
        }
        #region analizador sintactico
        public void AnlzdrSntctc(string[] sentencias)
        {
            
           for (int i = 1; i < sentencias.Length; i++)
           {
               

                #region  Expresiones regulares
                if (sentencias[i] != null)
               {
                    if (Regex.IsMatch(sentencias[i], @"Entero*"))
                   {

                        //System.Windows.Forms.MessageBox.Show("esto es una sentencia int");
                        #region parte semantica
                       if (Regex.IsMatch(sentencias[i], @"Entero\s+[a-zA-Z]{1,10};"))
                            {
                                MessageBox.Show("Sentencia correcta Entero: "+ sentencias[i]);
                            }

                        else{

                           tabla_errorres.addliste(12, i);
                            MessageBox.Show("Error de escritura Entero: " + sentencias[i]);

                        }
                        #endregion


                    }
                    else if (Regex.IsMatch(sentencias[i], @"Real*"))
                   {
                        //System.Windows.Forms.MessageBox.Show("esto es una sentencia double");
                        #region parte semantica
                        if (Regex.IsMatch(sentencias[i], @"Real\s+[a-zA-Z]{1,10};"))
                        {
                            MessageBox.Show("Sentencia correcta Real: " + sentencias[i]);
                        }

                        else
                        {

                            tabla_errorres.addliste(12, i);
                            MessageBox.Show("Error de escritura Real: " + sentencias[i]);

                        }
                        #endregion

                    }
                    else if (Regex.IsMatch(sentencias[i], @"Cadena*"))
                   {
                        #region parte semantica
                        if (Regex.IsMatch(sentencias[i], @"Cadena\s+[a-zA-Z]{1,10};"))
                        {
                            MessageBox.Show("Sentencia correcta Cadena: " + sentencias[i]);
                        }
                    else
                        {

                            tabla_errorres.addliste(12, i);
                            MessageBox.Show("Error de escritura Cadena: " + sentencias[i]);

                        }
                        #endregion

                    }
                    else if (Regex.IsMatch(sentencias[i], @"Booleano*"))
                   {
                        //System.Windows.Forms.MessageBox.Show("esto es una sentencia bool");

                        #region parte semantica
                        if (Regex.IsMatch(sentencias[i], @"Booleano\s+[a-zA-Z]{1,10};"))
                        {
                            MessageBox.Show("Sentencia correcta Booleano: " + sentencias[i]);
                        }

                        else
                        {

                            tabla_errorres.addliste(12, i);
                            MessageBox.Show("Error de escritura Booleano: " + sentencias[i]);

                        }
                        #endregion

                    }
                    else if (Regex.IsMatch(sentencias[i], @"//*"))
                   {
                       MessageBox.Show("Esto es un comentario correcto: " + sentencias[i]);
                   }
                    else if (Regex.IsMatch(sentencias[i], @"{$"))
                   {
                        MessageBox.Show("Inicio de ambito correcto: " + sentencias[i]);
                    }
                    else if (Regex.IsMatch(sentencias[i], @"}$"))
                   {
                        MessageBox.Show("Fin de ambito correcto: " + sentencias[i]);
                    }
                    else if (Regex.IsMatch(sentencias[i], @"Si*"))//--
                   {
                        MessageBox.Show("Sentencia Si correcta: " + sentencias[i]);
                    }
                    else if (Regex.IsMatch(sentencias[i], @"Sino*"))//--
                   {
                        MessageBox.Show("Sentencia Sino Si correcta: " + sentencias[i]);
                    }
                    else if (Regex.IsMatch(sentencias[i], @"Mientras*"))
                   {
                        MessageBox.Show("Sentencia Mientras correcta: " + sentencias[i]);
                    }
                }

               #endregion
               
               //System.Windows.Forms.MessageBox.Show("" + sentencias[i]);
           }
           



       
        }
        #endregion





        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardaArchivo();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            guardaArchivo2(nomarchivox);
          

        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            guardaArchivo2(nomarchivox);
            
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
            
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = tabla_simbolos.llamatabla();
            dataGridView2.DataSource = tabla_errorres.llamatablaE();
            
        }

        private void analizadorLexicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leer_archivo_al(nomarchivox);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
           tabControl1.Visible = true;
        
        }

        private void cerrarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false ;
            
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            abrirarchivo();
        }

        private void maximizarVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                this.WindowState = FormWindowState.Maximized;
        }

        private void minimizarVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                this.WindowState = FormWindowState.Normal;
        }

        private void minimizarVentanaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                 this.WindowState = FormWindowState.Minimized;
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void colorDeLaFuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var cl = colorDialog1.ShowDialog();
            if (cl == System.Windows.Forms.DialogResult.OK)
            {
                //PagCodigo.SelectionColor = colorDialog1.Color; <..... esto para una parte del texto
                PagCodigo.ForeColor = colorDialog1.Color;
            }



        }

        private void colorDeConsolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cl = colorDialog1.ShowDialog();
            if (cl == System.Windows.Forms.DialogResult.OK)
            {
                //PagCodigo.SelectionColor = colorDialog1.Color; <..... esto para una parte del texto
                PagCodigo.BackColor = colorDialog1.Color;
            }

        }

        private void formatoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fm = fontDialog1.ShowDialog();
            if (fm == DialogResult.OK)
            {
                //PagCodigo.SelectionColor = colorDialog1.Color; <..... esto para una parte del texto
                PagCodigo.Font = fontDialog1.Font;
            }


        }

        

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            guardaArchivo2(nomarchivox);
            tabla_simbolos.reinicialista();
            tabla_errorres.reinicialista();
            tabla_errorres.inicialestaE();
            tabla_simbolos.inicialista();
            leer_archivo_al(nomarchivox);
            string[] sent = uneSentencias();
           
            if (tabla_simbolos.revisar_duplicados())
            {
                tabla_errorres.addliste(11);
            }
         
            AnlzdrSntctc(sent);
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = tabla_simbolos.llamatabla();
            dataGridView2.DataSource = tabla_errorres.llamatablaE(); 
            System.Media.SystemSounds.Asterisk.Play();
                
            
           
            
        }

        private void PagCodigo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
