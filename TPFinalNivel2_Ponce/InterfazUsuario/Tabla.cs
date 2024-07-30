using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using LogicaNegocio;

namespace InterfazUsuario
{
    public partial class Tabla : Form
    {
        private List<Articulo> listaArticulos; //Ojo
        public Tabla()
        {
            InitializeComponent();
            this.listaArticulos = new List<Articulo>();
        }

        private void Tabla_Load(object sender, EventArgs e)
        {
            cargarTabla();
            eliminarColumna();

            cbCategoria.Items.Add("Audio");
            cbCategoria.Items.Add("Celulares");
            cbCategoria.Items.Add("Media");
            cbCategoria.Items.Add("Televisores");
        }
        private void dgvTabla_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTabla.CurrentRow != null)
            {
                Articulo seleccionArticulo = (Articulo)dgvTabla.CurrentRow.DataBoundItem;
                cargarImagen(seleccionArticulo.ImagenUrl);
            }
        }


        // Metodos reutilizables.
        private void cargarTabla()
        {
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            listaArticulos = negocioArticulo.MostrarLista();
            dgvTabla.DataSource = listaArticulos;
        }

        private void eliminarColumna()
        {
            dgvTabla.Columns["Id"].Visible = false;
            dgvTabla.Columns["ImagenUrl"].Visible = false;
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbArticulo.Load("https://live.staticflickr.com/6031/6367268285_6801f2fcbc_z.jpg");

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo mostrar = new frmAltaArticulo();
            mostrar.ShowDialog();
            cargarTabla();
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            Articulo seleccionArticulo = (Articulo)dgvTabla.CurrentRow.DataBoundItem;
            frmAltaArticulo modificarFrom = new frmAltaArticulo(seleccionArticulo);

            modificarFrom.ShowDialog();
            
            cargarTabla();
        }

        private void eliminarRegistros(bool logic = false)
        {
            Articulo seleccionarArticulo = (Articulo)dgvTabla.CurrentRow.DataBoundItem;
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            try
            {
                if (!logic)
                {
                    DialogResult respuesta = MessageBox.Show("Desea Eliminar el ARTICULO de la Base de Datos ?", "Eliminando Registro...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);
                    if (respuesta == DialogResult.Yes)
                    {
                        negocioArticulo.EliminacionFisica(seleccionarArticulo);
                        cargarTabla();
                    }
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show("Desea Eliminar el ARTICULO ?", "Eliminando Registro...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);
                    if (respuesta == DialogResult.Yes)
                    {
                        negocioArticulo.Eliminar(seleccionarArticulo);
                        cargarTabla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private void btnEliminacionFisica_Click(object sender, EventArgs e)
        {
            eliminarRegistros();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text;
            List<Articulo> listaFiltro = new List<Articulo>();



            if (filtro != string.Empty)
                listaFiltro = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            else
                listaFiltro = listaArticulos;

            dgvTabla.DataSource = null;
            dgvTabla.DataSource = listaFiltro;
            eliminarColumna();
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionCategoria = cbCategoria.SelectedItem.ToString();

            try
            {
                switch (seleccionCategoria)
                {
                    // He puesto Marcas demas, por si acaso.
                    case "Audio":
                        cbMarca.Items.Clear();
                        cbMarca.Items.Add("Phillips");
                        cbMarca.Items.Add("Samsung");
                        cbMarca.Items.Add("Sony");
                        break;
                    case "Celulares":
                        cbMarca.Items.Clear();
                        cbMarca.Items.Add("Apple");
                        cbMarca.Items.Add("Huawei");
                        cbMarca.Items.Add("Motorola");
                        cbMarca.Items.Add("Samsung");
                        cbMarca.Items.Add("Sony");
                        break;
                    case "Media":
                        cbMarca.Items.Clear();
                        cbMarca.Items.Add("Apple");
                        cbMarca.Items.Add("Sony");
                        break;
                    case "Televisores":
                        cbMarca.Items.Clear();
                        cbMarca.Items.Add("Hisense");
                        cbMarca.Items.Add("Phillips");
                        cbMarca.Items.Add("Samsung");
                        cbMarca.Items.Add("Sony");
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoria = cbCategoria.SelectedItem.ToString();
            string marca = cbMarca.SelectedItem.ToString();

            List<Articulo> listaFiltro = new List<Articulo>();

            

            switch (categoria)
            {
                case "Celulares":
                    switch (marca)
                    {
                        case "Motorola":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Celulares") && x.Marca.Descripcion.Contains("Motorola"));
                            break;
                        case "Samsung":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Celulares") && x.Marca.Descripcion.Contains("Samsung"));
                            break;
                        default:
                            MessageBox.Show("Articulo NO encontrado", "Busqueda...", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            listaFiltro = listaArticulos;

                            break;

                    }
                    break;
                case "Televisores":
                    switch (marca)
                    {
                        case "Sony":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Televisores") && x.Marca.Descripcion.Contains("Sony"));
                            break;

                        default:
                            MessageBox.Show("Articulo NO encontrado", "Busqueda...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listaFiltro = listaArticulos;
                            break;
                    }
                    break;
                case "Media":
                    switch (marca)
                    {
                        case "Apple":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Media") && x.Marca.Descripcion.Contains("Apple"));
                            break;
                        case "Sony":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Media") && x.Marca.Descripcion.Contains("Sony"));
                            break;
                        default:
                            MessageBox.Show("Articulo NO encontrado", "Busqueda...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listaFiltro = listaArticulos;
                            break;
                    }
                    break;
                case "Audio":
                    switch (marca)
                    {
                        case "Sony":
                            listaFiltro = listaArticulos.FindAll(x => x.Categoria.Descripcion.Contains("Audio") && x.Marca.Descripcion.Contains("Sony"));
                            break;
                        default:
                            MessageBox.Show("Articulo NO encontrado", "Busqueda...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listaFiltro = listaArticulos;
                            break;
                    }
                    break;
                default:
                    listaFiltro = listaArticulos;
                    break;



            }

            dgvTabla.DataSource = null;
            dgvTabla.DataSource = listaFiltro;
            eliminarColumna();

        }




















        //  **CORREGIR VALIDACION CODIGO 1 LETRA Y LOS SIGUIENTES QUE SEAN NUMEROS.
        // **CORRERGIR PRECIO , YA QUE DESPUES DE LA COMA TIENE 4 DIGITOS DEMAS.

    }

}
