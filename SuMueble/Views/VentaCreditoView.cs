﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using SuMueble.Views.Prompts;
using System.Windows.Forms;
using SuMueble.Models;
using SuMueble.Controller;

namespace SuMueble.Views
{

    public partial class VentaCreditoView : UserControl
    {   //Controladores 
        ClienteControlador clienteControlador = new ClienteControlador();
        ColaboradorControlador colaboradorControlador = new ColaboradorControlador();

        //Variables
        public static Guid _IDVenta;
        public static Ventas _venta;  
        public static List<DetallesVentas> listaProductos;
        public static List<Referencias> listaReferencias;
        private static float _Total = 0;
        public static void listaProducto(DetallesVentas dv)
        {
            dv.IDVenta = _IDVenta;
            listaProductos.Clear();
            listaProductos.Add(dv);
            _Total = dv.SubTotal;
           
        }

        //metodos
        private void btn_agregarProducto_Click(object sender, EventArgs e)
        {
            VentaAgregarProducto ventaAgregarProducto = new VentaAgregarProducto();
            ventaAgregarProducto.ShowDialog();
            CargarListView();
        }
        private void CargarListView()
        {
            l_monto.Text = string.Format("{0:C2}", _Total);
            lb_productosVenta.DataSource = null;
            lb_productosVenta.DataSource = listaProductos;
            lb_productosVenta.DisplayMember = "Info";
        } 
        private void btn_agregarReferencia_Click(object sender, EventArgs e)
        {
            var agregarReferencia = new AgregarReferencia();
            agregarReferencia.ShowDialog();
            CargarReferencias();
        }

        private void CargarReferencias()
        {

            lb_referencias.DataSource = null;
            lb_referencias.DataSource = listaReferencias;
            lb_referencias.DisplayMember = "Nombre";
        }

        private void txt_dniCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_dniCliente.Text.Length == 13)
            {
                ClearCliente();
                Clientes cliente = clienteControlador.GetCliente(txt_dniCliente.Text);
                if (cliente == null)
                {
                    ActivarIndicadores();
                    labelClienteNuevo.Visible = true;
                    
                }
                else
                {
                   
                    txt_nombreCliente.Text = cliente.Nombre;
                    txtTelefonoCliente.Text = cliente.Tel;
                    txt_dirCliente.Text = cliente.Direccion;
                    txt_rtnCliente.Text = cliente.RTN;
                    ActivarIndicadores();
                    
                 }
            }
            if (txt_dniCliente.Text.Length == 0)
                ClearCliente();
        }
        private void limpiarventa()
        {
            txt_dniCliente.Text = string.Empty;
            _IDVenta = Guid.NewGuid();
            listaProductos.Clear();
            listaReferencias.Clear();
            _Total = 0;
            _venta = new Ventas();
        }
        private void ClearCliente()
        {
            txt_nombreCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;
            txt_dirCliente.Text = string.Empty;
            txt_rtnCliente.Text = string.Empty;
           
            ActivarIndicadores();
        }
        private void ActivarIndicadores()
        {

            l_dir.Visible = txt_dirCliente.Text == "";
            l_NombreCliente.Visible = txt_nombreCliente.Text == "";
            l_TelefonoCliente.Visible = txtTelefonoCliente.Text == "";
            l_RTNCliente.Visible = txt_rtnCliente.Text == "";
        }

        private void txt_dniColaborador_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (txt_dniColaborador.Text.Length == 13)
            {
                Colaboradores c = colaboradorControlador.GetColaborador(txt_dniColaborador.Text);
                if (c == null)
                {
                    ShowHideColaboradorLabel();
                }
                else
                {
                    ShowHideColaboradorLabel(c.Nombre, true);

                }
            }
            if (txt_dniColaborador.Text.Length == 0)
                dniColaboradorLabelError.Visible = false;

        }

        private void ShowHideColaboradorLabel(string name = "", bool flag = false)
        {
            dniColaboradorLabelError.Visible = true;
            if (flag)
            {
                dniColaboradorLabelError.Text = name;
                dniColaboradorLabelError.ForeColor = Color.FromName("Dodgerblue");


            }
            else
            {
                dniColaboradorLabelError.Text = "Escribió mal su DNI";
                dniColaboradorLabelError.ForeColor = Color.FromName("Crimson");

            }

        }
        private void btn_terminarVenta_Click(object sender, EventArgs e)
        {
            string msg = IsAllReady() ;
            if (msg == "")
            {
                
                Clientes cliente = new Clientes()
                {
                    DNI       = txt_dniCliente.Text,
                    Direccion = txt_dirCliente.Text,
                    Nombre    = txt_nombreCliente.Text,
                    RTN       = txt_rtnCliente.Text,
                    Tel       = txtTelefonoCliente.Text

                };
                _venta = new Ventas()
                {
                    ID            = _IDVenta,
                    Cliente       = cliente,
                    DetallesVenta = listaProductos,
                    IDColaborador = txt_dniColaborador.Text,
                    Referencias   = listaReferencias,
                    IDTipoVenta   = 2,
                    //Cuotas  = 0,
                    //Prima = 0,
                    //FechaFin = DateTime.Now,


                };
                var terminar = new TerminarVentaCredito();
                terminar.ShowDialog();
                limpiarventa();
                ClearCliente();
            }
            else
            {
                MessageBox.Show(msg, "Campos incompletos",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            
        }
        private string IsAllReady()
        {
            string res = txt_dniCliente.Text.Length != 13 ? "* DNI Cliente" : "";
            res += txt_rtnCliente.Text.Length != 14 ? "\n* RTN de Cliente" : "";
            res += txt_nombreCliente.Text.Length < 3 ? "\n* Nombre de Cliente" : "";
            res += txtTelefonoCliente.Text.Length != 8 ? "\n* Telefono de Cliente" : "";
            res += txt_dirCliente.Text.Length < 10 ? "\n* Direccion de Cliente" : "";
            res += txt_dniColaborador.Text == "" ? "\n* DNI de Colaborador" : "";
            
            res += listaReferencias.Count < 2 ? "\n* Faltan Referencias" : "";

            res += listaProductos.Count != 1 ? "\n* Falta Agregar Productos a la venta" : "";


            return res;
        }
        //constructor
        public VentaCreditoView()
        {
            InitializeComponent();
            listaProductos = new List<DetallesVentas>();
            listaReferencias = new List<Referencias>();
            _IDVenta = Guid.NewGuid();
        }
    }
}
