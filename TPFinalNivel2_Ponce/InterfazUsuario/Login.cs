﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace InterfazUsuario
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwmd, int wmsg, int wparam, int lparam);


        private void button1_Click(object sender, EventArgs e)
        {
            // de esta manera escondemos el primer formulario, pero sigue consumiendo ram.
            MenuPrincipal irMenuPrincipal = new MenuPrincipal();
            irMenuPrincipal.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnaelSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }
    }
}
