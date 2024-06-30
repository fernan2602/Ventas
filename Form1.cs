using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Llamar al mapeado objeto relacional a travez de un objeto 

        VentasDataContext ventas = new VentasDataContext();

        //Crear procedimiento para listar la tabla 

         public void Listar()
        {
            var consulta = from V in ventas.Vendedor
                           select V;
            dgvVendedor.DataSource = consulta;

        }
        //Agregar


        private void Form1_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Vendedor vendedorAgregar = new Vendedor();
            vendedorAgregar.Apellidos = txtApellido.Text;
            vendedorAgregar.Nombres = txtNombre.Text;
            vendedorAgregar.CodVendedor = txtCodVendedor.Text;
            ventas.Vendedor.InsertOnSubmit(vendedorAgregar);
            try
            {
                ventas.SubmitChanges(); //Confirma la transaccion
                txtCodVendedor.Clear(); txtNombre.Clear(); txtApellido.Clear();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            //Obtener el codigo del vendedor a eliminar
            var CodVendedorEliminar = (from V in ventas.Vendedor
                                       where V.CodVendedor.Contains(txtCodVendedor.Text)
                                       select V).First();

            ventas.Vendedor.DeleteOnSubmit(CodVendedorEliminar);
            try
            {
                ventas.SubmitChanges(); //Confirma la eliminacion
                txtCodVendedor.Clear(); txtNombre.Clear(); txtApellido.Clear();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            // Obtener el código del vendedor a actualizar

            // Buscar el vendedor a actualizar
            var vendedorActualizar = (from V in ventas.Vendedor
                                      where V.CodVendedor.Contains(txtCodVendedor.Text)
                                      select V).First();

            if (vendedorActualizar != null)
            {
                // Actualizar los campos del vendedor
                vendedorActualizar.Nombres = txtNombre.Text.Trim();
                vendedorActualizar.Apellidos = txtApellido.Text.Trim();

                try
                {
                    ventas.SubmitChanges(); // Confirmar los cambios

                    MessageBox.Show("Vendedor actualizado correctamente.");
                    txtCodVendedor.Clear(); txtNombre.Clear(); txtApellido.Clear();
                    Listar(); // Actualizar la lista de vendedores en la interfaz
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }


        }
    }
}
