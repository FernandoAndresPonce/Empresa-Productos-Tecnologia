﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; //importante para mover el formulario

namespace InterfazUsuario
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        // linea de codigo para permitir que se pueda mover el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panelMenuIzquierdo.Width == 250)
                panelMenuIzquierdo.Width = 70;
            else
                panelMenuIzquierdo.Width = 250;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("Esta seguro de Cerrar Sesion?", "Cerrando aplicacion...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);
            if (respuesta == DialogResult.Yes)
                Application.Exit();
        } 
            
            
        //para que quede centrado o el mismo lugar que lo he dejado cuando se restaura la pantalla
        int LX, LY;
        private void pbMaximizar_Click(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Maximized;
            //ajustar formulario para que no ocupe toda la pantalla.
            LX = this.Location.X;
            LY = this.Location.Y;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            pbMaximizar.Visible = false;
            pbRestaurar.Visible = true;
        }

        private void pbRestaurar_Click(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Normal;
            //ajustar formulario para que no ocupe toda la pantalla.
            this.Size = new Size(1300, 650);
            this.Location = new Point(LX, LY);
            pbMaximizar.Visible = true;
            pbRestaurar.Visible = false;
        }

        private void pbMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // mover formulario
        private void panelSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
        //Para abrir en el panel contenedor que posee , este formulario, los demas formularios (hijos)
        //IMPORTANTISIMO ...
        private void AbrirFormularioPanelContenedor(object formularioH)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formularioH as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            ProductosTabla frmTabla = new ProductosTabla();
            frmTabla.FormClosed += new FormClosedEventHandler(mostrarLogoCerrarForm);
            AbrirFormularioPanelContenedor(frmTabla);
            
        }
        // Posibilidad de expandir el formulario.

        protected override void WndProc(ref Message msj)
        {
            const int CoordenadaWFP = 0x84; // parte inferior derecha
            const int DesIzquierda = 16;
            const int DesDerecha = 17;
            if (msj.Msg == CoordenadaWFP)
            {
                int x = (int)(msj.LParam.ToInt64() & 0xFFFF);
                int y = (int)((msj.LParam.ToInt64() & 0XFFFF0000) >> 16);
                Point CoordenadaArea = PointToClient(new Point(x, y));
                Size TamanoAreaForm = ClientSize;
                if (CoordenadaArea.X >= TamanoAreaForm.Width - 16 && CoordenadaArea.Y >= TamanoAreaForm.Height - 16 );// aca me falta algo .
                {
                    msj.Result = (IntPtr)(IsMirrored ? DesIzquierda : DesDerecha);
                    return;
                }
            }
            base.WndProc(ref msj);
        }


        //Metodo para ver el logo cuando cerremos los formularios
        private void mostrarLogo()
        {
            AbrirFormularioPanelContenedor(new Fondo());
        }
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            mostrarLogo();
        }

        // Metodo al cerrar Formulario.
        private void mostrarLogoCerrarForm (object sender, FormClosedEventArgs e)
        {
            mostrarLogo(); /// y de aca vamos al formproducto
        }

       
        

    }
}
