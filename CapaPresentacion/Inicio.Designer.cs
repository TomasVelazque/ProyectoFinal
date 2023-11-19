namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuArticulo = new FontAwesome.Sharp.IconMenuItem();
            this.MenuProveedor = new FontAwesome.Sharp.IconMenuItem();
            this.MenuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.MenuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuRegistrarVenta = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuVerDetalleVentas = new FontAwesome.Sharp.IconMenuItem();
            this.MenuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuRegistrarCompra = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.MenuCamaras = new FontAwesome.Sharp.IconMenuItem();
            this.MenuTitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuArticulo,
            this.MenuProveedor,
            this.MenuUsuario,
            this.MenuVentas,
            this.MenuCompras,
            this.MenuCamaras});
            this.menuStrip1.Location = new System.Drawing.Point(0, 48);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 63);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuArticulo
            // 
            this.MenuArticulo.AutoSize = false;
            this.MenuArticulo.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.MenuArticulo.IconColor = System.Drawing.Color.Black;
            this.MenuArticulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuArticulo.IconSize = 40;
            this.MenuArticulo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuArticulo.Name = "MenuArticulo";
            this.MenuArticulo.Size = new System.Drawing.Size(66, 59);
            this.MenuArticulo.Text = "Articulo";
            this.MenuArticulo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuArticulo.Click += new System.EventHandler(this.MenuArticulo_Click);
            // 
            // MenuProveedor
            // 
            this.MenuProveedor.AutoSize = false;
            this.MenuProveedor.IconChar = FontAwesome.Sharp.IconChar.Shop;
            this.MenuProveedor.IconColor = System.Drawing.Color.Black;
            this.MenuProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuProveedor.IconSize = 40;
            this.MenuProveedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuProveedor.Name = "MenuProveedor";
            this.MenuProveedor.Size = new System.Drawing.Size(66, 59);
            this.MenuProveedor.Text = "Proveedor";
            this.MenuProveedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuProveedor.Click += new System.EventHandler(this.MenuProveedor_Click);
            // 
            // MenuUsuario
            // 
            this.MenuUsuario.AutoSize = false;
            this.MenuUsuario.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.MenuUsuario.IconColor = System.Drawing.Color.Black;
            this.MenuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuUsuario.IconSize = 40;
            this.MenuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuUsuario.Name = "MenuUsuario";
            this.MenuUsuario.Size = new System.Drawing.Size(66, 59);
            this.MenuUsuario.Text = "Usuario";
            this.MenuUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuUsuario.Click += new System.EventHandler(this.MenuUsuario_Click);
            // 
            // MenuVentas
            // 
            this.MenuVentas.AutoSize = false;
            this.MenuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRegistrarVenta,
            this.SubMenuVerDetalleVentas});
            this.MenuVentas.IconChar = FontAwesome.Sharp.IconChar.ShoppingCart;
            this.MenuVentas.IconColor = System.Drawing.Color.Black;
            this.MenuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuVentas.IconSize = 40;
            this.MenuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(66, 59);
            this.MenuVentas.Text = "Ventas";
            this.MenuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // SubMenuRegistrarVenta
            // 
            this.SubMenuRegistrarVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuRegistrarVenta.IconColor = System.Drawing.Color.Black;
            this.SubMenuRegistrarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuRegistrarVenta.Name = "SubMenuRegistrarVenta";
            this.SubMenuRegistrarVenta.Size = new System.Drawing.Size(129, 22);
            this.SubMenuRegistrarVenta.Text = "Registrar";
            this.SubMenuRegistrarVenta.Click += new System.EventHandler(this.SubMenuRegistrarVenta_Click);
            // 
            // SubMenuVerDetalleVentas
            // 
            this.SubMenuVerDetalleVentas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuVerDetalleVentas.IconColor = System.Drawing.Color.Black;
            this.SubMenuVerDetalleVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuVerDetalleVentas.Name = "SubMenuVerDetalleVentas";
            this.SubMenuVerDetalleVentas.Size = new System.Drawing.Size(129, 22);
            this.SubMenuVerDetalleVentas.Text = "Ver Detalle";
            this.SubMenuVerDetalleVentas.Click += new System.EventHandler(this.SubMenuVerDetalleVentas_Click);
            // 
            // MenuCompras
            // 
            this.MenuCompras.AutoSize = false;
            this.MenuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRegistrarCompra,
            this.SubMenuDetalleCompra});
            this.MenuCompras.IconChar = FontAwesome.Sharp.IconChar.ShoppingBag;
            this.MenuCompras.IconColor = System.Drawing.Color.Black;
            this.MenuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuCompras.IconSize = 40;
            this.MenuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuCompras.Name = "MenuCompras";
            this.MenuCompras.Size = new System.Drawing.Size(66, 59);
            this.MenuCompras.Text = "Compras";
            this.MenuCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // SubMenuRegistrarCompra
            // 
            this.SubMenuRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuRegistrarCompra.IconColor = System.Drawing.Color.Black;
            this.SubMenuRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuRegistrarCompra.Name = "SubMenuRegistrarCompra";
            this.SubMenuRegistrarCompra.Size = new System.Drawing.Size(129, 22);
            this.SubMenuRegistrarCompra.Text = "Registrar";
            this.SubMenuRegistrarCompra.Click += new System.EventHandler(this.SubMenuRegistrarCompra_Click);
            // 
            // SubMenuDetalleCompra
            // 
            this.SubMenuDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.SubMenuDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuDetalleCompra.Name = "SubMenuDetalleCompra";
            this.SubMenuDetalleCompra.Size = new System.Drawing.Size(129, 22);
            this.SubMenuDetalleCompra.Text = "Ver Detalle";
            this.SubMenuDetalleCompra.Click += new System.EventHandler(this.SubMenuDetalleCompra_Click);
            // 
            // MenuCamaras
            // 
            this.MenuCamaras.AutoSize = false;
            this.MenuCamaras.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.MenuCamaras.IconColor = System.Drawing.Color.Black;
            this.MenuCamaras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuCamaras.IconSize = 40;
            this.MenuCamaras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuCamaras.Name = "MenuCamaras";
            this.MenuCamaras.Size = new System.Drawing.Size(66, 59);
            this.MenuCamaras.Text = "Camaras";
            this.MenuCamaras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MenuCamaras.Click += new System.EventHandler(this.MenuCamaras_Click);
            // 
            // MenuTitulo
            // 
            this.MenuTitulo.AutoSize = false;
            this.MenuTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MenuTitulo.Location = new System.Drawing.Point(0, 0);
            this.MenuTitulo.Name = "MenuTitulo";
            this.MenuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MenuTitulo.Size = new System.Drawing.Size(941, 48);
            this.MenuTitulo.TabIndex = 1;
            this.MenuTitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Cooper Black", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = ">SISTEMA: LOS DOS CHINOS<";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.White;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 111);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(941, 353);
            this.contenedor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(461, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "USUARIO: ";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblUsuario.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblUsuario.Location = new System.Drawing.Point(550, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(97, 19);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "lblUsuario";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(575, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(107, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblHora.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.Black;
            this.lblHora.Location = new System.Drawing.Point(714, 18);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(70, 19);
            this.lblHora.TabIndex = 7;
            this.lblHora.Text = "lblhora";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label4.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(653, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "HORA: ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 464);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.MenuTitulo);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip MenuTitulo;
        private FontAwesome.Sharp.IconMenuItem MenuArticulo;
        private FontAwesome.Sharp.IconMenuItem MenuProveedor;
        private FontAwesome.Sharp.IconMenuItem MenuUsuario;
        private FontAwesome.Sharp.IconMenuItem MenuVentas;
        private FontAwesome.Sharp.IconMenuItem MenuCompras;
        private FontAwesome.Sharp.IconMenuItem MenuCamaras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUsuario;
        private FontAwesome.Sharp.IconMenuItem SubMenuRegistrarCompra;
        private FontAwesome.Sharp.IconMenuItem SubMenuDetalleCompra;
        private FontAwesome.Sharp.IconMenuItem SubMenuRegistrarVenta;
        private FontAwesome.Sharp.IconMenuItem SubMenuVerDetalleVentas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}

