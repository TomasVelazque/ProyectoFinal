using CapaEntidades;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class FormDetalleCompra : Form
    {
        public FormDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compras oCompras = new CN_Compras().ObtenerCompra(txbBusqueda.Text);

            if(oCompras.ComprasID != 0)
            {
                txbNumerodeFactura.Text = oCompras.NumeroFactura;
                txbFecha.Text = oCompras.FechaCreacion;
                txbUsuarioCompra.Text = oCompras.oUsuarios.Nombre;
                // txbNombreProveedor.Text = oCompras.pProveedores.Nombre;
                txbCUITProveedor.Text = oCompras.pProveedores.CUIT;
            }


            dgvData.Rows.Clear();
            foreach(DetalleCompra dc in oCompras.oDetalleCompra)
            {
                dgvData.Rows.Add(new object[] { dc.oArticulos.Detalle, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal });
            }
            txbMontoTotal.Text = oCompras.MontoTotal.ToString("0.00");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txbFecha.Text = "";
            txbUsuarioCompra.Text = "";
            txbCUITProveedor.Text = "";
            dgvData.Rows.Clear();
            txbMontoTotal.Text = "0.00";
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (txbUsuarioCompra.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_HTML = Properties.Resources.CompraTicket.ToString();

            Texto_HTML = Texto_HTML.Replace("@fecharegistro",txbFecha.Text.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@usuarioregistro", txbUsuarioCompra.Text.ToUpper());
            Texto_HTML = Texto_HTML.Replace("@numerofactura", txbNumerodeFactura.Text.ToUpper());


            string filas = string.Empty;

            foreach(DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Articulo"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            Texto_HTML = Texto_HTML.Replace("@filas",filas);
            Texto_HTML = Texto_HTML.Replace("@montototal", txbMontoTotal.Text);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txbNumerodeFactura.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if(savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4,25,25,25,25);


                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc,stream);
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

        private void FormDetalleCompra_Load(object sender, EventArgs e)
        {

        }
    }
}
