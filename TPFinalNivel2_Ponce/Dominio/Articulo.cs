﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        /*
         Código de artículo.
        Nombre.
        Descripción.
        Marca (seleccionable de una lista desplegable).
        Categoría (seleccionable de una lista desplegable.
        Imagen.
        Precio.
         */

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

    }
}
