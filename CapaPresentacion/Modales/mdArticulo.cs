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
    public partial class mdArticulo : Form
    {
        public Articulos _Articulo { get; set; }
        public mdArticulo()
        {
            InitializeComponent();
        }

        private void mdArticulo_Load(object sender, EventArgs e)
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

            List<Articulos> listarticulos = new CN_Articulos().Listar();

            foreach (Articulos item in listarticulos)
            {

                dgwData.Rows.Add(new object[] {item.ArticulosID,item.Detalle, item.Codigo,item.Stock,item.PrecioCompra,item.PrecioVenta,
               
                 });

            }
        }

        // SI LE DAMOS DOBLE CLICK SE NOS LLENAN LOS CAMPOS CON LAS COSAS DE LOS ARTICULOS
        private void dgwData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if (iRow >= 0 && iColum > 0)
            {
                _Articulo = new Articulos()
                {
                    ArticulosID = Convert.ToInt32(dgwData.Rows[iRow].Cells["id"].Value.ToString()),
                    Detalle = dgwData.Rows[iRow].Cells["Detalle"].Value.ToString(),
                    Codigo = dgwData.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    Stock = Convert.ToInt32(dgwData.Rows[iRow].Cells["Stock"].Value.ToString()),
                    PrecioCompra = Convert.ToDecimal(dgwData.Rows[iRow].Cells["PrecioCompra"].Value.ToString()),
                    PrecioVenta = Convert.ToDecimal(dgwData.Rows[iRow].Cells["PrecioVenta"].Value.ToString()),

                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        // SI DAMOS A BUSCAR NOS BUSCA LOS ARTICULOS
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

        //BOTON DE LIMPIAR
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
