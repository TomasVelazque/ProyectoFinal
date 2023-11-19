using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaPresentacion.Utilidades;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormUsuarios : Form
    {
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() {Valor = 1, Texto = "Activo"});
            cboEstado.Items.Add(new OpcionCombo() {Valor = 0, Texto = "Inactivo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


            List<Roles> listarol = new CN_Roles().Listar();

            foreach (Roles item in listarol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.RolesID, Texto = item.Descripcion });

            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

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


            //MOSTRAR LOS USUARIOS DE LA BASE DE DATOS 
            List<Usuarios> listausuarios = new CN_Usuarios().Listar();

            foreach (Usuarios item in listausuarios)
            {

                dgwData.Rows.Add(new object[] {"",item.UsuariosID,item.Email,item.Clave,item.Celular,item.Nombre,
                item.oRoles.RolesID,
                item.oRoles.Descripcion,
                item.Estado == true? 1 : 0,
                item.Estado == true? "Activo" : "Inactivo"
            }) ;

            }

            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;





        }

        // CREAMOS Y MODIFICAMOS UN USUARIO
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Usuarios objusuario = new Usuarios()
            {
                UsuariosID = Convert.ToInt32(txbID.Text),
                Nombre = txbNombre.Text,
                Email = txbEmail.Text,
                Clave = txbClave.Text,
                Celular = txbCelular.Text,
                oRoles = new Roles() { RolesID = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false  
            };

            if (objusuario.UsuariosID == 0)
            {

                int idusuariogenerado = new CN_Usuarios().Registrar(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {
                    dgwData.Rows.Add(new object[] {"",idusuariogenerado, txbEmail.Text, txbCelular.Text, txbClave.Text, txbNombre.Text,

                    ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
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
                bool resultado = new CN_Usuarios().Editar(objusuario, out mensaje);

                if (resultado == true)
                {
                    DataGridViewRow row = dgwData.Rows[Convert.ToInt32(txbIndice.Text)];
                    row.Cells["UsuarioID"].Value = txbID.Text;
                    row.Cells["Email"].Value = txbEmail.Text;
                    row.Cells["Clave"].Value = txbClave.Text;
                    row.Cells["Celular"].Value = txbCelular.Text;
                    row.Cells["Nombre"].Value = txbNombre.Text;
                    row.Cells["RolID"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
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

        private void Limpiar()
        {
            txbIndice.Text = "-1";
            txbID.Text = "0";
            txbNombre.Text = "";
            txbEmail.Text = "";
            txbCelular.Text = "";
            txbClave.Text = "";
            txbConfirmarClave.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

            txbNombre.Select();
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

                e.Graphics.DrawImage(Properties.Resources.icons8_marca_de_verificación_16, new Rectangle(x,y,w,h));
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
                    txbID.Text = dgwData.Rows[indice].Cells["UsuarioID"].Value.ToString();
                    txbNombre.Text = dgwData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txbEmail.Text = dgwData.Rows[indice].Cells["Email"].Value.ToString();
                    txbClave.Text = dgwData.Rows[indice].Cells["Clave"].Value.ToString();
                    txbConfirmarClave.Text = dgwData.Rows[indice].Cells["Clave"].Value.ToString();
                    txbCelular.Text = dgwData.Rows[indice].Cells["Celular"].Value.ToString();

                    foreach (OpcionCombo oc in cboRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgwData.Rows[indice].Cells["RolID"].Value.ToString()))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_combo;
                            break;

                        }
                    }


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
            if(Convert.ToInt32(txbID.Text) != 0)
            {
                if(MessageBox.Show("¿Desea ELIMINAR el USUARIO? :0 ","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuarios objusuario = new Usuarios()
                    {
                        UsuariosID = Convert.ToInt32(txbID.Text),
                    };

                    bool respuesta = new CN_Usuarios().Eliminar(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dgwData.Rows.RemoveAt(Convert.ToInt32(txbIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                }
            } 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgwData.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgwData.Rows)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txbBusqueda.Text = "";

            foreach (DataGridViewRow row in dgwData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiarTextBox_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        // PARA EL USUARIO SOLO PERMITIMOS LETRAS Y ESPACIOS
        private void txbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir solamente letras y espacios
            if (!char.IsControl(teclaPresionada) && !char.IsLetter(teclaPresionada) && teclaPresionada != ' ')
            {
                // Si no es una letra ni un espacio, cancelar la entrada del carácter
                e.Handled = true;
            }
        }

        // PERMITIMOS EN EMAIL INGRESAR SOLAMENTE NUMEROS, LETRAS, PUNTOS Y EL @
        private void txbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir números, letras, puntos y el símbolo "@"
            if (!char.IsControl(teclaPresionada) && !char.IsLetterOrDigit(teclaPresionada) && teclaPresionada != '.' && teclaPresionada != '@')
            {
                // Si no es un número, letra, punto ni el símbolo "@", cancelar la entrada del carácter
                e.Handled = true;
            }

            // Permitir borrar y pegar (Ctrl + X, Ctrl + C, Ctrl + V)
            if (char.IsControl(teclaPresionada) && teclaPresionada != '\b' && teclaPresionada != 22 && teclaPresionada != 3 && teclaPresionada != 24)
            {
                e.Handled = false;
            }

        }

        //PARA LA CONTRASEÑA SOLO PERMITIMOS LETRAS Y NUMEROS
        private void txbClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir letras y números
            if (!char.IsControl(teclaPresionada) && !char.IsLetterOrDigit(teclaPresionada))
            {
                // Si no es una letra ni un número, cancelar la entrada del carácter
                e.Handled = true;
            }
        }

        //PARA EL NUMERO PERMITIMOS SOLAMENTE NUMEROS (PODRIA HABER PUESTO + Y ESO PERO LO QUISE HACER SENCILLO)
        private void txbCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtener el carácter presionado
            char teclaPresionada = e.KeyChar;

            // Permitir solamente números y la tecla de control (borrar, copiar, pegar, etc.)
            if (!char.IsControl(teclaPresionada) && !char.IsDigit(teclaPresionada))
            {
                // Si no es un número ni una tecla de control, cancelar la entrada del carácter
                e.Handled = true;
            }
        }
    }
}
