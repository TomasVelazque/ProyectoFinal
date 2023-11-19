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

namespace CapaPresentacion
{
    public partial class FormArticulos : Form
    {
        public FormArticulos()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            txbIndice.Text = "-1";
            txbID.Text = "0";
            txbDetalle.Text = "";
            txbPresentacion.Text = "";
            txbCodigo.Text = "";
            cboEstado.SelectedIndex = 0;
            txbDetalle.Select();
        }

        private void FormArticulos_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;



            foreach (DataGridViewColumn columna in dgwData.Columns)
            {
                if (columna.Visible == true && columna.Name != "BtnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            //MOSTRAR LOS ARTICULOS DE LA BASE DE DATOS 
            List<Articulos> listarticulos = new CN_Articulos().Listar();

            foreach (Articulos item in listarticulos)
            {

                dgwData.Rows.Add(new object[] {"",item.ArticulosID,item.Detalle,item.Presentacion,item.Codigo,item.Stock,item.PrecioCompra,item.PrecioVenta,
                item.Estado == true? 1 : 0,
                item.Estado == true? "Activo" : "Inactivo"
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Articulos objarticulo = new Articulos()
            {
                ArticulosID = Convert.ToInt32(txbID.Text),
                Detalle = txbDetalle.Text,
                Presentacion = txbPresentacion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false,
                Codigo = txbCodigo.Text

            };

            if (objarticulo.ArticulosID == 0)
            {

                int idarticulogenerado = new CN_Articulos().Registrar(objarticulo, out mensaje);

                if (idarticulogenerado != 0)
                {
                    dgwData.Rows.Add(new object[] {"",idarticulogenerado, txbDetalle.Text, txbPresentacion.Text, txbCodigo.Text, "0","0.00","0.00",
                    ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
                });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Articulos().Editar(objarticulo, out mensaje);

                if (resultado == true)
                {
                    DataGridViewRow row = dgwData.Rows[Convert.ToInt32(txbIndice.Text)];
                    row.Cells["id"].Value = txbID.Text;
                    row.Cells["Detalle"].Value = txbDetalle.Text;
                    row.Cells["Presentacion"].Value = txbPresentacion.Text;
                    row.Cells["Codigo"].Value = txbCodigo.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void dgwData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_marca_de_verificación_16.Width;
                var h = Properties.Resources.icons8_marca_de_verificación_16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.icons8_marca_de_verificación_16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgwData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwData.Columns[e.ColumnIndex].Name == "BtnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txbIndice.Text = indice.ToString();
                    txbID.Text = dgwData.Rows[indice].Cells["id"].Value.ToString();
                    txbDetalle.Text = dgwData.Rows[indice].Cells["Detalle"].Value.ToString();
                    txbPresentacion.Text = dgwData.Rows[indice].Cells["Presentacion"].Value.ToString();
                    txbCodigo.Text = dgwData.Rows[indice].Cells["Codigo"].Value.ToString();

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgwData.Rows[indice].Cells["EstadoValor"].Value.ToString()))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;

                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txbID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea ELIMINAR el ARTICULO? :0 ", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Articulos objarticulo = new Articulos()
                    {
                        ArticulosID = Convert.ToInt32(txbID.Text),
                    };

                    bool respuesta = new CN_Articulos().Eliminar(objarticulo, out mensaje);

                    if (respuesta)
                    {
                        dgwData.Rows.RemoveAt(Convert.ToInt32(txbIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

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

        private void btnLimpiarTextBox_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txbBusqueda.Text = "";

            foreach (DataGridViewRow row in dgwData.Rows)
            {
                row.Visible = true;
            }
        }

        // PARA EL CODIGO SOLAMENTE PERMITIMOS NUMEROS Y GIONES
        private void txbCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir solo números y el guión "-"
            if (!char.IsControl(teclaPresionada) && !char.IsDigit(teclaPresionada) && teclaPresionada != '-')
            {
                // Si no es un número ni el guión "-", cancelar la entrada del carácter
                e.Handled = true;
            }

            // Permitir borrar y pegar (Ctrl + X, Ctrl + C, Ctrl + V)
            if (char.IsControl(teclaPresionada) && teclaPresionada != '\b' && teclaPresionada != 22 && teclaPresionada != 3 && teclaPresionada != 24)
            {
                e.Handled = false;
            }
        }

        //PARA LA PRESENTACION PERMITIMOS LETRAS, NUMEROS Y ESPACIOS
        private void txbPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir letras, números y espacios
            if (!char.IsControl(teclaPresionada) && !char.IsLetterOrDigit(teclaPresionada) && teclaPresionada != ' ')
            {
                // Si no es una letra, número ni espacio, cancelar la entrada del carácter
                e.Handled = true;
            }

            // Permitir borrar y pegar (Ctrl + X, Ctrl + C, Ctrl + V)
            if (char.IsControl(teclaPresionada) && teclaPresionada != '\b' && teclaPresionada != 22 && teclaPresionada != 3 && teclaPresionada != 24)
            {
                e.Handled = false;
            }
        }

        //PARA EL NOMBRE DEL PRODUCTO SOLO DEJAMOS QUE SE PUEDAN PONER LETRAS Y ESPACIOS
        private void txbDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir letras y espacios
            if (!char.IsControl(teclaPresionada) && !char.IsLetter(teclaPresionada) && teclaPresionada != ' ')
            {
                // Si no es una letra ni un espacio, cancelar la entrada del carácter
                e.Handled = true;
            }

            // Permitir borrar y pegar (Ctrl + X, Ctrl + C, Ctrl + V)
            if (char.IsControl(teclaPresionada) && teclaPresionada != '\b' && teclaPresionada != 22 && teclaPresionada != 3 && teclaPresionada != 24)
            {
                e.Handled = false;
            }
        }
    }
}
