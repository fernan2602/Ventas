﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        VentasDataContext ventas = new VentasDataContext();

        //Crear procedimiento para listar la tabla 



        private void Form2_Load(object sender, EventArgs e)
        {


        }

        private void btnConsulta1_Click(object sender, EventArgs e)
        {
            var  consulta = from C in ventas.Cliente
                            select C;   
            dgvClientes.DataSource= consulta;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var consulta = from C in ventas.Cliente
                           select new
                           {
                               C.CodCliente,
                               C.Apellidos,
                               C.Nombres,
                           };
            dgvClientes.DataSource = consulta;  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var consulta = from C in ventas.Producto
                           select new
                           {
                               C.CodProduto,
                           };
        }
    }
}










