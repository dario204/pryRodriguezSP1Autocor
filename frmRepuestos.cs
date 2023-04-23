using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryRodriguezSP1Autocor
{
    public partial class frmMain : Form
    {
        private const string PATH_ARCHIVO = "Repuestos.txt";
        public frmMain()
        {
            InitializeComponent();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {
            txtCodigo.Text = ""; // limpiar los textBox
            txtNombre.Text = "";
            txtPrecio.Text = "";
            // cargar el comboBox
            cboMarca.Items.Clear();
            cboMarca.Items.Add("Marca A");
            cboMarca.Items.Add("Marca B");
            cboMarca.Items.Add("Marca C");
            cboMarca.SelectedIndex = 0;
            // marcar la opción de origen Nacional
            optNacional.Checked = true;
        }



        private bool ValidarDatos()
        {
            // devuelve falso si no se cumplen todas las condiciones
            bool resultado = false;
            if (txtCodigo.Text != "") // controla el valor del código
            {
                if (txtNombre.Text != "") // controla el nombre
                {
                    if (txtPrecio.Text != "") // controla el precio
                    {
                        clsArchivo Repuestos = new clsArchivo();
                        Repuestos.NombreArchivo = PATH_ARCHIVO;
                        // controla que no se repita el código del repuesto
                        if (Repuestos.BuscarCodigoRepuesto(txtCodigo.Text) ==
                        false)
                        {
                           
                        resultado = true; // devuelve verdadero sólo si todas
                                          // las condiciones se cumplieron
                        }
                    }
                }
            }
            return resultado;
        }



        private clsRepuesto CrearRepuesto()
        {
            clsRepuesto nuevoRep = new clsRepuesto();
            nuevoRep.Codigo = txtCodigo.Text;
            nuevoRep.Nombre = txtNombre.Text;
            nuevoRep.Marca = cboMarca.SelectedItem.ToString();
            nuevoRep.Precio = decimal.Parse(txtPrecio.Text);
            if (optNacional.Checked == true)
            {
                nuevoRep.Origen = "Nacional";
            }
            else
            {
                nuevoRep.Origen = "Importado";
            }
            return nuevoRep;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && txtPrecio.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (ValidarDatos()) // si los datos son correctos
            {
                // crear un nuevo repuesto
                clsRepuesto nuevoRep = CrearRepuesto();
                // grabar en el archivo
                clsArchivo Repuestos = new clsArchivo();
                Repuestos.NombreArchivo = PATH_ARCHIVO;
                Repuestos.GrabarRepuesto(nuevoRep);
                // restaurar la interfaz al estado inicial
                Inicializar();

            }
            else
            {
                MessageBox.Show("Error", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            frmConsulta frm = new frmConsulta(PATH_ARCHIVO);
            frm.ShowDialog();
        }
}   }
