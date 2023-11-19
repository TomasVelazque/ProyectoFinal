using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // CUANDO SE CANCELA SIMPLEMENTE SE CIERRA EL PROGRAMA
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //CUANDO SE INGRESA VERIFICAMOS SI EL USUARIO SE ENCUENTRA EN LA LISTA DE USUARIOS SACADOS DE LA BASE DE DATOS
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            List<Usuarios> TEST = new CN_Usuarios().Listar();

            Usuarios ousarios = new CN_Usuarios().Listar().Where(u => u.Nombre == txbUsuario.Text && u.Clave == txbContraseña.Text).FirstOrDefault();


            if (ousarios != null)
            {
                Inicio form = new Inicio(ousarios);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("NO SE ENCONTRO EL USUARIO >:0 ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //CUANDO EL FORMULARIO SE CIERRA SE LIMPIAN LOS CAMPOS DE TEXTO
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txbContraseña.Text = "";
            txbUsuario.Text = "";
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
