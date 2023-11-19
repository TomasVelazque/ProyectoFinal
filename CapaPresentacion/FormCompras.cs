using CapaEntidades;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormCompras : Form
    {
        private Usuarios _Usuario;
        public FormCompras(Usuarios oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {
            txbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txbProveedorID.Text = "0";
            txbArtID.Text = "0";
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txbProveedorID.Text = modal._Proveedor.ProveedoresID.ToString();
                    txbCUIT.Text = modal._Proveedor.CUIT;
                    txbNombre.Text = modal._Proveedor.Nombre;
                }
                else
                {
                    txbCUIT.Select();
                }
            }
        }

        private void btnBuscarArt_Click(object sender, EventArgs e)
        {
            using (var modal = new mdArticulo())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txbArtID.Text = modal._Articulo.ArticulosID.ToString();
                    txbCodigo.Text = modal._Articulo.Codigo;
                    txbDetalle.Text = modal._Articulo.Detalle;
                    txbPrecioCompra.Select();
                }
                else
                {
                    txbCUIT.Select();
                }
            }
        }

        private void txbCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Articulos oArticulos = new CN_Articulos().Listar().Where(a => a.Codigo == txbCodigo.Text && a.Estado == true).FirstOrDefault();

                if (oArticulos != null)
                {
                    txbCodigo.BackColor = Color.Honeydew;
                    txbArtID.Text = oArticulos.ArticulosID.ToString();
                    txbDetalle.Text = oArticulos.Detalle;
                    txbPrecioCompra.Select();
                }
                else
                {
                    txbCodigo.BackColor = Color.MistyRose;
                    txbArtID.Text = "0";
                    txbDetalle.Text = "";
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existe = false;

            if (int.Parse(txbArtID.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar algun articulo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txbPrecioCompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio-Compra - Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txbPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txbPrecioVenta.Text, out precioventa))
            {
                MessageBox.Show("Precio-Venta - Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txbPrecioVenta.Select();
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["ArticuloID"].Value != null && fila.Cells["ArticuloID"].Value.ToString() == txbArtID.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {
                dgvData.Rows.Add(new object[]
                {
                    txbArtID.Text,
                    txbDetalle.Text,
                    preciocompra.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    nudCantidad.Value.ToString(),
                    (nudCantidad.Value * preciocompra).ToString("0.00")
                });

                CalcularTotal();
                LimpiarProducto();
                txbCodigo.Select();
            }
        }

        private void LimpiarProducto()
        {
            txbArtID.Text = "0";
            txbCodigo.Text = "";
            txbCodigo.BackColor = Color.White;
            txbDetalle.Text = "";
            txbPrecioCompra.Text = "";
            txbPrecioVenta.Text = "";
            nudCantidad.Value = 1;
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
                }
            }
            txbTotalAPagar.Text = total.ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_basura_llena_25.Width;
                var h = Properties.Resources.icons8_basura_llena_25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.icons8_basura_llena_25, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvData.Rows.RemoveAt(indice);
                    CalcularTotal();

                }
            }
        }

        private void txbPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txbPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txbPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txbPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txbProveedorID.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar articulos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable Detalle_Compra = new DataTable();

            Detalle_Compra.Columns.Add("ArticulosID", typeof(int));
            Detalle_Compra.Columns.Add("PrecioCompra", typeof(decimal));
            Detalle_Compra.Columns.Add("PrecioVenta", typeof(decimal));
            Detalle_Compra.Columns.Add("Cantidad", typeof(int));
            Detalle_Compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                Detalle_Compra.Rows.Add(

                    new object[]{
                        Convert.ToInt32(row.Cells["ArticuloID"].Value.ToString()),
                        row.Cells["PrecioCompra"].Value.ToString(),
                        row.Cells["PrecioVenta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString()
                });
            }

            int idcorrelativo = new CN_Compras().ObtenerCorrelativo();
            string numerofactura = string.Format("{0:00000}",idcorrelativo);


            Compras oCompra = new Compras()
            {
                oUsuarios = new Usuarios() { UsuariosID = _Usuario.UsuariosID},
                pProveedores = new Proveedores() { ProveedoresID = Convert.ToInt32(txbProveedorID.Text)},
                NumeroFactura = numerofactura,
                MontoTotal = Convert.ToDecimal(txbTotalAPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compras().Registrar(oCompra, Detalle_Compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de compra generado: \n"+ numerofactura + "\n\n ¿Desea Copiar al portapapeles?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numerofactura);
                }

                txbProveedorID.Text = "";
                txbCUIT.Text = "";
                txbNombre.Text = "";
                dgvData.Rows.Clear();
                CalcularTotal();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txbFecha_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txbProveedorID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txbCUIT_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txbPrecioVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbPrecioCompra_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txbArtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbDetalle_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txbCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txbTotalAPagar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
