using CapaEntidades;
using CapaNegocio;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormDetalleVentas : Form
    {
        public FormDetalleVentas()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Ventas oVenta = new CN_Venta().ObtenerVenta(txbBusqueda.Text);

            if(oVenta.VentasID != 0)
            {
                txbNumeroFactura.Text = oVenta.NumeroFactura;
                txbFecha.Text = oVenta.FechaCreacion;
                txbUsuarioCompra.Text = oVenta.NombreUsuario;

                dgvData.Rows.Clear();

                foreach(DetalleVenta dv in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] { dv.oArticulos.Detalle, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                }


                txbMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                txbMontoPago.Text = oVenta.MontoPago.ToString("0.00");
                txbMontoCambio.Text = oVenta.MontoCambio.ToString("0.00");
                
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txbFecha.Text = "";
            txbUsuarioCompra.Text = "";
            dgvData.Rows.Clear();
            txbMontoCambio.Text = "0.00";
            txbMontoTotal.Text = "0.00";
            txbMontoPago.Text = "0.00";

        }

        private void FormDetalleVentas_Load(object sender, EventArgs e)
        {
            txbBusqueda.Select();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (txbUsuarioCompra.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_HTML = Properties.Resources.VentaTicket.ToString();

            Texto_HTML = Texto_HTML.Replace("@fecharegistro", txbFecha.Text.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@usuarioregistro", txbUsuarioCompra.Text.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@numerofactura", txbNumeroFactura.Text.ToUpper());


            string filas = string.Empty;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Articulo"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            Texto_HTML = Texto_HTML.Replace("@filas", filas);
            Texto_HTML = Texto_HTML.Replace("@montototal", txbMontoTotal.Text);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txbNumeroFactura.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);


                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();


                    using (StringReader sr = new StringReader(Texto_HTML))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    //stream.Close();
                    MessageBox.Show("SIII DOCUMENTO GENERADO :DDD", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
    }
}
