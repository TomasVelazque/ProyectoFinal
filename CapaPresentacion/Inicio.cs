using CapaEntidades;
using FontAwesome.Sharp;
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
using System.Management.Instrumentation;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        //PROPIEDADES DEL FORMULARIO

        private static Usuarios UsuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;
        public Inicio(Usuarios objusuario)
        {
            UsuarioActual = objusuario;

            InitializeComponent();
        }
        //INICIO DEL FORMULARIO Y CARGA DE NOMBRE DEL USUARIO QUE ESTA INGRESANDO AL SISTEMA
        private void Inicio_Load(object sender, EventArgs e)
        {

            List<Permisos> ListaPermisos = new CN_Permisos().Listar(UsuarioActual.UsuariosID);

            foreach (IconMenuItem iconmenu in menuStrip1.Items)
            {
                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }

            lblUsuario.Text = UsuarioActual.Nombre;
        }

        //METODO PARA ABRIR LOS FORMULARIOS
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
           // formulario.BackColor = Color.Aqua;
            contenedor.Controls.Add(formulario);
            formulario.Show();
                
        }

        //ABRIMOS LOS MENUS Y SUBMENUS
        private void MenuArticulo_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem) sender, new FormArticulos());
        }

        private void MenuProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormProveedores());
        }

        private void MenuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormUsuarios());
        }

        private void MenuCamaras_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormCamaras());
        }

        private void SubMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormCompras(UsuarioActual));
        }

        private void SubMenuDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormDetalleCompra());
        }

        private void SubMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormVentas(UsuarioActual));
        }

        private void SubMenuVerDetalleVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FormDetalleVentas());
        }

        //TIMER PARA MOSTRAR LA HORA
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
        }
    }
}
