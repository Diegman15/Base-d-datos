﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        OleDbConnection con; //Agregamos la conexion    
        DataTable dt; //Agregar la tabla
        public MainWindow()
        {
            InitializeComponent();
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\AlumnosDB.mdb";//Conectamos la base de datos a nuestro archivo Access
            MostrarDatos();

        }
        private void MostrarDatos()
        {
            OleDbCommand cmd = new OleDbCommand();//ole ayuda a correr comandos 
            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select*from Pogra";//esto indica que seleccione todos los campos que estan en la tabla progra
            OleDbDataAdapter da = new OleDbDataAdapter();
            dt = new DataTable();
            da.Fill(dt);
            gvDatos.ItemsSource = dt.AsDataView();
            if (dt.Rows.Count > 0)
            {
                lbContenido.Visibility = System.Windows.Visibility.Hidden;
                gvDatos.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lbContenido.Visibility = System.Windows.Visibility.Visible;
                gvDatos.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void LimpiarTodo()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            cbGenero.SelectedIndex = 0; 
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            btnNuevo.Content = "Nuevo";
            txtId.IsEnabled = true;
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open) con.Open();
            cmd.Connection = con;
            if(txtId.Text!="")
            {
                if (cbGenero.Text != "Selecciona Genero")
                {
                    cmd.CommandText = "insert into Progra(Id,Nombre,Genero,Telefono,Direccion)" + "Values(" + txtId.Text + ",'" + txtNombre.Text + ",'" + cbGenero.Text + ",'" + txtTelefono.Text + ",'" + txtDireccion.Text + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Alumnos agregado correctamente... ");
                    LimpiarTodo();

                }
            }
            else
            {
                cmd.CommandText = "update Progra set Nombre='" + txtNombre.Text + "'Genero='" + cbGenero.Text + "',Telefono=" + txtTelefono.Text + ",'Direccion='" + txtDireccion.Text + "'where Id=" + txtId.Text;
                cmd.ExecuteNonQuery();
                MostrarDatos();
                MessageBox.Show("Datos del alumno Actualizados...");
                LimpiarTodo(); 
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

    

