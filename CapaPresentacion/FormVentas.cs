using CapaEntidades;
using CapaNegocio;
using CapaPresentacion.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormVentas : Form
    {
        private Usuarios _Usuario;
        public FormVentas(Usuarios oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        //CUANDO CARGA LIMPIAMOS LOS CAMPOS Y CARGAMOS LA FECHA
        private void FormVentas_Load(object sender, EventArgs e)
        {
            txbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txbArtID.Text = "0";
            txbPago.Text = "";
            txbCambio.Text = "";
            txbTotalAPagar.Text = "0";
        }

        //ESTO ES PARA BUSCAR UN ARTICULO MEDIANTE LOS MODALES, SI SE ENCUENTRA SE LLENAN LOS CAMPOS DE TEXTO
        private void btnBuscarArt_Click(object sender, EventArgs e)
        {
            using (var modal = new mdArticulo())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txbArtID.Text = modal._Articulo.ArticulosID.ToString();
                    txbCodigoArticulo.Text = modal._Articulo.Codigo;
                    txbDetalleArt.Text = modal._Articulo.Detalle;
                    txbPrecio.Text = modal._Articulo.PrecioVenta.ToString("0.00");
                    txbStock.Text = modal._Articulo.Stock.ToString();
                    nudCantidad.Select();
                }
                else
                {
                    txbCodigoArticulo.Select();
                }
            }
        }

        private void txbCodigoArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Articulos oArticulos = new CN_Articulos().Listar().Where(a => a.Codigo == txbCodigoArticulo.Text && a.Estado == true).FirstOrDefault();

                if (oArticulos != null)
                {
                    txbCodigoArticulo.BackColor = Color.Honeydew;
                    txbArtID.Text = oArticulos.ArticulosID.ToString();
                    txbDetalleArt.Text = oArticulos.Detalle;
                    txbPrecio.Text = oArticulos.PrecioVenta.ToString("0.00");
                    txbStock.Text = oArticulos.Stock.ToString();
                    nudCantidad.Select();
                }
                else
                {
                    txbCodigoArticulo.BackColor = Color.MistyRose;
                    txbArtID.Text = "0";
                    txbDetalleArt.Text = "";
                    txbPrecio.Text = "";
                    txbStock.Text = "";
                    nudCantidad.Value = 1;
                }
            }
        }

        //ANTES DE AGREGAR NOS FIJAMOS QUE LOS CAMPOS NO ESTEN VACIOS Y QUE LOS FORMATOS SEAN CORRECTOS
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool art_existe = false;

            if (int.Parse(txbArtID.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar algun articulo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txbPrecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txbPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txbStock.Text) < Convert.ToInt32(nudCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["ArticuloID"].Value != null && fila.Cells["ArticuloID"].Value.ToString() == txbArtID.Text)
                {
                    art_existe = true;
                    break;
                }
            }

            if (!art_existe)
            {
                bool respuesta = new CN_Venta().RestarStock(
                   Convert.ToInt32(txbArtID.Text),
                   Convert.ToInt32(nudCantidad.Value.ToString())        
                    );

                if (respuesta)
                {
                    dgvData.Rows.Add(new object[]
                {
                    txbArtID.Text,
                    txbDetalleArt.Text,
                    precio.ToString("0.00"),
                    nudCantidad.Value.ToString(),
                    (nudCantidad.Value * precio).ToString("0.00")
                });

                    CalcularTotal();
                    LimpiarProducto();
                    txbCodigoArticulo.Select();
                }
            }
        }

        //METODO PARA CALCULAR EL TOTAL DE UNA VENTA
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

        //METODO DE LIMPIEZA DE LOS CAMPOS
        private void LimpiarProducto()
        {
            txbArtID.Text = "0";
            txbCodigoArticulo.Text = "";
            txbCodigoArticulo.BackColor = Color.White;
            txbDetalleArt.Text = "";
            txbPrecio.Text = "";
            txbStock.Text = "";
            nudCantidad.Value = 1;
        }

        // LOGOS DEL DATA GRIDVIEW
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 5)
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

        //SI SE PRECIONA ELBOTON DE ELIMINAR DEL DATAGRID SE ELIMINA EL ARTICULO Y CARACTERISTICAS
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(

                        Convert.ToInt32(dgvData.Rows[indice].Cells["ArticuloID"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString())
                        );

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        CalcularTotal();
                    }
                }
            }
        }

        // VALIDACION DE QUE EL PRECIO SE SOLAMENTE NUMERICO
        private void txbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txbPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        // VALIDACION DE QUE EL PAGO SEA SOLAMENTE NUMERICO
        private void txbPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txbPago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        //METODO PARA CALCULAR CAMBIO
        private void CalcularCambio()
        {
            if (txbTotalAPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagacon;
            decimal total = Convert.ToDecimal(txbTotalAPagar.Text);

            if (txbPago.Text.Trim() == "")
            {
                txbPago.Text = "0";
            }

            if (decimal.TryParse(txbPago.Text.Trim(), out pagacon))
            {
                if (pagacon < total)
                {
                    txbCambio.Text = "0";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txbCambio.Text = cambio.ToString("0.00");
                }
            }

        }

        //SI SE TOCA ENTER EN PAGO AUTOMATICAMENTE NOS CALCULA EL CAMBIO
        private void txbPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        //CREAMOS UNA VENTA
        private void btnCrearVenta_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar productos para realizar la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //UTILIZAMOS UN DATATABLE

            DataTable Detalle_Venta = new DataTable();

            Detalle_Venta.Columns.Add("ArticulosID", typeof(int));
            Detalle_Venta.Columns.Add("PrecioVenta", typeof(decimal));
            Detalle_Venta.Columns.Add("Cantidad", typeof(int));
            Detalle_Venta.Columns.Add("SubTotal", typeof(decimal));


            foreach (DataGridViewRow row in dgvData.Rows)
            {
                Detalle_Venta.Rows.Add(

                    new object[]{
                        Convert.ToInt32(row.Cells["ArticuloID"].Value.ToString()),
                        row.Cells["Precio"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString()
                });
            }

            //GENERAMOS EL NUMERO DE LA FACTURA

            int idcorrelativo = new CN_Venta().ObtenerCorrelativo();
            string NumeroFactura = string.Format("{0:00000}", idcorrelativo);
            CalcularCambio();


            Ventas oVentas = new Ventas()
            {
                oUsuarios = new Usuarios() { UsuariosID = _Usuario.UsuariosID },
                NumeroFactura = NumeroFactura,
                MontoPago = Convert.ToDecimal(txbPago.Text),
                NombreUsuario = _Usuario.Nombre, 
                MontoCambio = Convert.ToDecimal(txbCambio.Text),
                MontoTotal = Convert.ToDecimal(txbTotalAPagar.Text),
            };


            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVentas, Detalle_Venta, out mensaje);

            // SI ESTA TODO OK GENERAMOS EL NUMERO DE FACTURA 

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generado: \n" + NumeroFactura + "\n\n ¿Desea Copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(NumeroFactura);
                }

                dgvData.Rows.Clear();
                CalcularTotal();
                txbPago.Text = "";
                txbCambio.Text = "";
            }
            else
            {
                //MENSAJE DE ERROR SI PASO ALGUN ERROR
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
    }
}
