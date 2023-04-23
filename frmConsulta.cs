using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace pryRodriguezSP1Autocor
{
    public partial class frmConsulta : Form
    {
        public frmConsulta()
        {
            InitializeComponent();
        }
        private string PATH_ARCHIVO;
        public frmConsulta(string Path) // el constructor recibe el nombre del archivo
        {
            InitializeComponent();
            PATH_ARCHIVO = Path;
        }
        private void frmConsulta_Load(object sender, EventArgs e)
        {
            Inicializar();

        }
        private void Inicializar()
        {
            // carga de los items en el comboBox de Marcas
            cboMarca.Items.Clear();
            cboMarca.Items.Add("Marca A");
            cboMarca.Items.Add("Marca B");
            cboMarca.Items.Add("Marca C");
            cboMarca.SelectedIndex = 0;
            // opción de Origen inicial
            optNacional.Checked = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            {
                // controlar si el archivo existe
                if (!File.Exists(Application.StartupPath + "\\" + PATH_ARCHIVO))
                {
                    MessageBox.Show("No hay datos para mostrar", "Consulta",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                // leer el contenido del archivo
                clsArchivo Repuestos = new clsArchivo();
                Repuestos.NombreArchivo = PATH_ARCHIVO;
                List<clsRepuesto> repuestos = Repuestos.ObtenerRepuestosOrdenados();
                // limpiar la grilla
                dgvRepuestos.Rows.Clear();
                // recorrer la lista de repuestos
                foreach (clsRepuesto repuesto in repuestos)
                {
                    // controlar el valor de la Marca
                    if (repuesto.Marca == cboMarca.SelectedItem.ToString())
                    {
                        // controlar el tipo de Origen y los botones de opción activos
                        if (optImportado.Checked && repuesto.Origen == "Importado")
                        {
                            // agregar el repuesto a la grilla
                            dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                            repuesto.Marca, repuesto.Origen,
                            repuesto.Precio.ToString());
                        }
                        else
                        {
                            if (optNacional.Checked && repuesto.Origen == "Nacional")
                            {
                                // agregar el repuesto a la grilla
                                dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                                repuesto.Marca, repuesto.Origen,
                                repuesto.Precio.ToString());
                            }
                            else
                            {
                                if (optAmbos.Checked)
                                {
                                    // agregar el repuesto a la grilla
                                    dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                                    repuesto.Marca, repuesto.Origen,
                                    repuesto.Precio.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
