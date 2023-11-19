using CapaEntidades;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Modales
{
    public partial class mdProveedor : Form
    {

        public Proveedores _Proveedor { get; set; }
        public mdProveedor()
        {
            InitializeComponent();
        }

        private void mdProveedor_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgwData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            //MOSTRAR LOS Proveedores DE LA BASE DE DATOS 
            List<Proveedores> listaProveedores = new CN_Proveedores().Listar();

            foreach (Proveedores item in listaProveedores)
            {

                dgwData.Rows.Add(new object[] {item.ProveedoresID,item.Nombre,item.CUIT,

            });

            }

        }

        //SI SE DA DOBLE CLICK SE RELLENAN LOS CAMPOS CON LOS DATOS DEL PROVEEDOR
        private void dgwData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if (iRow >= 0 && iColum > 0)
            {
                _Proveedor = new Proveedores()
                {
                    ProveedoresID = Convert.ToInt32(dgwData.Rows[iRow].Cells["id"].Value.ToString()),
                    Nombre = dgwData.Rows[iRow].Cells["Nombre"].Value.ToString(),
                    CUIT = dgwData.Rows[iRow].Cells["CUIT"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // BOTON DE BUSQUEDA
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgwData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgwData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txbBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        // BOTON DE LIMPIEZA
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txbBusqueda.Text = "";

            foreach (DataGridViewRow row in dgwData.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
