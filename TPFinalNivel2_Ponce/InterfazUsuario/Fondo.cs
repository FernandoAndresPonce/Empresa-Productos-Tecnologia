using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazUsuario
{
    public partial class Fondo : Form
    {
        public Fondo()
        {
            InitializeComponent();
        }

        //Mostrar fecha y hora, usar el Timer y modificarlo , enable true, intervale 100
        private void horaFecha_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
        }
    }
}
