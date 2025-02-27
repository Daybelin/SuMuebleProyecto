﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using SuMueble.Controller;
using System.Windows.Forms;
using SuMueble.Models;
using System.Linq;

namespace SuMueble.Views
{
    public partial class VentaView : UserControl
    {
        //controladores
        ProductoControlador productoControlador = new ProductoControlador();
        ClienteControlador clienteControlador = new ClienteControlador();
        ColaboradorControlador colaboradorControlador = new ColaboradorControlador();
        VentaController ventaController = new VentaController();
        List<Productos> productos;
        ProductoControlador pc = new ProductoControlador();

        //variables
        private float Total = 0;
        private List<DetallesVentas> _detallesVenta = new List<DetallesVentas>();
        private string _msg = "1. Seleccione un producto\n2. Indique la cantidad que se venderá\n3. Asegurese de No borrar el precio del producto de el cuadro de texto en la parte inferior";
        private Guid _IDVenta;

        // metodos
        public VentaView()
        {
            InitializeComponent();
            CargarDataGrid();
            dgv_productos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _IDVenta = Guid.NewGuid();
        }
        private void CargarDataGrid()
        {
            dgv_productos.AutoGenerateColumns = false;                        
            productos = pc.GetProductos().ToList();
            dgv_productos.DataSource = productos;
        }

        private void btn_terminarVenta_Click(object sender, EventArgs e)
        {
            Clientes c = new Clientes()
            {
                DNI = txt_dniCliente.Text,
                Nombre = txt_nombreCliente.Text,
                Tel = txt_clienteTelefono.Text
            };
            string msg = VentaIsAllReady();
            if (msg == string.Empty)
            {
                Ventas venta = new Ventas()
                {
                    ID = _IDVenta,
                    DetallesVenta = _detallesVenta,
                    Cliente = c,
                    IDTipoVenta = 1,
                    IDColaborador = txt_dniColaborador.Text,
                    FechaFin = DateTime.Now

                };
                bool ok = ventaController.SaveVenta(venta);
                if (ok)
                    MessageBox.Show($"Venta Terminada\nMonto: {Total}", "Venta Completada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Venta no Terminada\nMonto: {Total}", "Error al completar la Venta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                CargarDataGrid();
                ClearVenta();
                
            }
            else
                MessageBox.Show("Faltan los siguientes datos:\n"+msg, "Faltan datos de la venta", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void ClearVenta()
        {
            Total = 0;
            l_monto.Text = string.Empty;
            _IDVenta = Guid.NewGuid();
            txt_dniCliente.Text = string.Empty;
            ClearCliente();

            _detallesVenta = new List<DetallesVentas>();
            ActualizarListView();
        }

        private void dgv_productos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // cell 3 = precio
            txt_precio.Text = GetCell(3);
        }

        

        private void btn_agregarProducto_Click(object sender, EventArgs e)

        {

            if (GetCell(4) == "0")
            {
                MessageBox.Show("No hay existencia del producto", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 
                
            } else
            {

                if (txt_cantidadProducto.Text != string.Empty && txt_precio.Text != string.Empty)
                {
                    DetallesVentas dv = new DetallesVentas()
                    {
                        IDVenta = _IDVenta,
                        IDProducto = int.Parse(GetCell(0)),
                        Cantidad = int.Parse(txt_cantidadProducto.Text),
                        PrecioVenta = int.Parse(txt_precio.Text),
                        Producto = GetCell(2),

                    };

                    CargarListVew(dv);
                    ClearProducto();
                }
                else
                    MessageBox.Show(_msg, "Faltan datos de la venta", MessageBoxButtons.OK, MessageBoxIcon.Information);




            }







        }
        private void CargarListVew(DetallesVentas dv)
        {
            Total += dv.SubTotal;
            l_monto.Text = string.Format("{0:C2}", Total);
            _detallesVenta.Add(dv);
            // actualizar el listview
            ActualizarListView();

        }
        private void ActualizarListView()
        {
            lb_productosVenta.DataSource = null;
            lb_productosVenta.DataSource = _detallesVenta;
            lb_productosVenta.DisplayMember = "Info";
        }
        private string GetCell(int cell = 0)
        {
            // ID, Codigo, Producto, Precio, Existencias
            // 0 ,      1,        2,      3,          4
            return dgv_productos.Rows[dgv_productos.CurrentRow.Index].Cells[cell].Value.ToString();
        }

        private void ClearProducto()
        {
            txt_cantidadProducto.Text = string.Empty;
            txt_precio.Text = string.Empty;
        }

        private void txt_dniCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_dniCliente.Text.Length == 13)
            {
                ClearCliente();
                Clientes cliente = clienteControlador.GetCliente(txt_dniCliente.Text);
                if (cliente == null)
                {
                    HideShowLabels(true);
                }
                else
                {
                    HideShowLabels(false);
                    txt_nombreCliente.Text = cliente.Nombre;
                    txt_clienteTelefono.Text = cliente.Tel;
                }
            }
            if (txt_dniCliente.Text.Length == 0)
                ClearCliente();
        }

        private void HideShowLabels(bool visible)
        {
            labelClienteNuevo.Visible = visible;
            labelNombre.Visible = visible;
            labelTelefono.Visible = visible;
        }
        private void ClearCliente()
        {
            txt_nombreCliente.Text = string.Empty;
            txt_clienteTelefono.Text = string.Empty;
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
                    ShowHideColaboradorLabel(c.Nombre,true);

                }
            }
            if(txt_dniColaborador.Text.Length == 0)
                dniColaboradorLabelError.Visible = false;

        }

        private void ShowHideColaboradorLabel(string name="",bool flag=false)
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

        private string VentaIsAllReady()
        {
            string msg = txt_dniCliente.Text.Length == 13 ? string.Empty : "* DNI del Cliente\n";
            msg += txt_nombreCliente.Text != string.Empty ? "" : "* Nombre del Cliente\n";
            msg += txt_clienteTelefono.Text != string.Empty ? "" : "* Telefono del Cliente\n";
            msg += _detallesVenta.Count > 0 ? "" : "* Agregar Productos a la Venta\n";
            msg += txt_dniColaborador.Text.Length == 13 ? "" : "* DNI del Colaborador\n";
            msg += dniColaboradorLabelError.ForeColor == Color.Crimson ? "* Agregar un DNI de Colaborador Valido": "";
            // Color.FromName("Crimson")
            return msg;

        }

        private void VentaView_Load(object sender, EventArgs e)
        {

        }

        private void btn_quitarItem_Click(object sender, EventArgs e)
        {
            int i = lb_productosVenta.SelectedIndex;
            Total -= _detallesVenta[i].SubTotal; 
            _detallesVenta.RemoveAt(i);
            ActualizarListView();
            l_monto.Text = string.Format("{0:C2}", Total);

        }

        private void txt_buscarProducto_TextChanged(object sender, EventArgs e)
        {
            string buscar = txt_buscarProducto.Text.ToLower();

            List<Productos> filtrados = productos.Where<Productos>(x => {

                return x.Producto.ToLower().StartsWith(buscar) || x.Codigo.ToLower().StartsWith(buscar);


            }).ToList();

            dgv_productos.DataSource = null;
            dgv_productos.DataSource = filtrados;
        }
    }
}
